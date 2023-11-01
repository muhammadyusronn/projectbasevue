<template>
    <form id="mainForm" action="#" method="post">
        <Card>
            <template #title>
                <Toolbar>
                    <template #left>
                        <div class="p-card-title">
                            [{{mode}}] {{$t('lookup')}}
                        </div>
                        <div style="margin-left: 10px" v-if="mode != this.$FORM_MODE_CREATE">
                            <Button type="button" icon="pi pi-info" @click="toggle" aria:haspopup="true" aria-controls="overlay_panel" class="p-button-rounded p-button-outlined p-button-sm" />

                            <OverlayPanel ref="op" appendTo="body" :showCloseIcon="true" id="overlay_panel" style="width: 550px" :breakpoints="{'960px': '80vw'}">
                                <div class="grid field">
                                    <div class="row">
                                        <i class="pi pi-plus-circle" /> {{this.$formatDateTime(header.Created_Date)}}
                                    </div>
                                    <div class="row">
                                        &nbsp;by {{header.Created_By}}
                                    </div>
                                </div>
                                <div class="grid field" v-if="header.Edited_Date">
                                    <div class="row">
                                        <i class="pi pi-pencil" /> {{this.$formatDateTime(header.Edited_Date)}}
                                    </div>
                                    <div class="row">
                                        &nbsp;by {{header.Edited_By}}
                                    </div>
                                </div>
                            </OverlayPanel>
                        </div>
                    </template>

                    <template #right>
                        <Button icon="pi pi-chevron-left" :label="$t('back')" class="p-button mr-2" @click="backToIndex" />
                        <Button icon="pi pi-save" :label="$t('save')" class="p-button-success" @click="confirm($event)" v-if="mode != this.$FORM_MODE_VIEW"/>
                        <ConfirmPopup></ConfirmPopup>
                    </template>
                </Toolbar>
            </template>
            <template #content>
                <div class="p-inputtext-sm p-fluid">
                    <FormField :labelName="$t('lookup_group')" fieldName="Lookup_Group" v-model="header">
                        <InputText name="Lookup_Group" v-model="header.Lookup_Group" type="text" :placeholder="$t('lookup_group')" v-if="header" :disabled="mode == this.$FORM_MODE_VIEW"  />
                    </FormField>

                    <FormField :labelName="$t('lookup_key')" fieldName="Lookup_Key" v-model="header">
                        <InputText name="Lookup_Key" v-model="header.Lookup_Key" type="text" :placeholder="$t('lookup_key')" v-if="header" :disabled="mode == this.$FORM_MODE_VIEW"  />
                    </FormField>
                    
                    <FormField :labelName="$t('lookup_value')" fieldName="Lookup_Value" v-model="header">
                        <InputText name="Lookup_Value" v-model="header.Lookup_Value" type="text" :placeholder="$t('lookup_value')" v-if="header" :disabled="mode == this.$FORM_MODE_VIEW"  />
                    </FormField>

                    <FormField :labelName="$t('lookup_info1')" fieldName="Lookup_Info1" v-model="header">
                        <InputText name="Lookup_Info1" v-model="header.Lookup_Info1" type="text" :placeholder="$t('lookup_info1')" v-if="header" :disabled="mode == this.$FORM_MODE_VIEW"  />
                    </FormField>

                    <FormField :labelName="$t('lookup_info2')" fieldName="Lookup_Info2" v-model="header">
                        <InputText name="Lookup_Info2" v-model="header.Lookup_Info2" type="text" :placeholder="$t('lookup_info2')" v-if="header" :disabled="mode == this.$FORM_MODE_VIEW"  />
                    </FormField>
                </div>
            </template>
        </Card>
    </form>
</template>


<script>
    export default {
        name: "Editor",
        data() {
            return {
                indexName: "LookupIndex",
                url: this.$API_URL + "lookup/",
                header: null,
                detail: null,
                mode: "",
                id: 0,
                formEditable: true,
            }
        },
        methods: {
            getData() {
                var self = this;
                this.$axios.get(this.url + this.id + "/" + this.mode)
                    .then((response) => {
                        self.header = response.data.data.header;
                        self.header.mode = self.mode;
                    })
                    .catch(function (error) {
                        alert(error);
                    });
            },
            saveData() {
                this.$emit('block-ui');
                this.$axios.post(this.url, this.header)
                    .then((response) => {
                        if (response.data.success) {
                            this.$router.push({ name: this.indexName, params: { showToast: true, severity: 'success', summary: 'Data Saved', detail: response.data.message, life: 120000} } );
                        }
                        else {
                            this.showError('Error Saving Data', response.data.message);
                        }
                    })
                    .catch(function (error) {
                        this.showError('Error Saving Data', error);
                    });
            },
            showError(summary, message) {
                this.$emit('unblock-ui');
                this.$toast.add({ severity: 'error', summary: summary, detail: message, life: 120000});
            },
            backToIndex() {
                this.$router.push({ path: "/lookup/Index" });
            },
            toggle(event) {
                this.$refs.op.toggle(event);
            },
            confirm(event) {
                this.$confirm.require({
                    target: event.currentTarget,
                    message: 'Are you sure?',
                    icon: 'pi pi-exclamation-triangle',
                    accept: () => {
                        this.saveData();
                    },
                    reject: () => {
                        //callback to execute when user rejects the action
                    }
                });
            },
        },
        created() {
            this.mode = this.$route.query.mode;
            this.id = this.$route.query.id ?? 0;

            this.getData();
        }
    }
</script>