<template>
  <form id="mainForm" action="#" method="post">
    <Card>
      <template #title>
        <Toolbar>
          <template #left>
            <div class="p-card-title">[{{ mode }}] {{ $t("group") }}</div>
            <div
              style="margin-left: 10px"
              v-if="mode != this.$FORM_MODE_CREATE"
            >
              <Button
                type="button"
                icon="pi pi-info"
                @click="toggle"
                aria:haspopup="true"
                aria-controls="overlay_panel"
                class="p-button-rounded p-button-outlined p-button-sm"
              />

              <OverlayPanel
                ref="op"
                appendTo="body"
                :showCloseIcon="true"
                id="overlay_panel"
                style="width: 550px"
                :breakpoints="{ '960px': '80vw' }"
              >
                <div class="grid field">
                  <div class="row">
                    <i class="pi pi-plus-circle" />
                    {{ this.$formatDateTime(header.Create_Date) }}
                  </div>
                  <div class="row">&nbsp;by {{ header.Create_By }}</div>
                </div>
                <div class="grid field" v-if="header.Edit_Date">
                  <div class="row">
                    <i class="pi pi-pencil" />
                    {{ this.$formatDateTime(header.Edit_Date) }}
                  </div>
                  <div class="row">&nbsp;by {{ header.Edit_By }}</div>
                </div>
              </OverlayPanel>
            </div>
          </template>

          <template #right>
            <Button
              icon="pi pi-chevron-left"
              :label="$t('back')"
              class="p-button mr-2"
              @click="backToIndex"
            />
            <Button
              icon="pi pi-save"
              :label="$t('save')"
              class="p-button-success"
              @click="confirm($event)"
              v-if="mode != this.$FORM_MODE_VIEW"
            />
            <ConfirmPopup></ConfirmPopup>
          </template>
        </Toolbar>
      </template>
      <template #content>
        <div class="p-inputtext-sm p-fluid">
          <FormField
            :labelName="$t('Group')"
            fieldName="Group_Code"
            v-model="header"
          >
            <CustomSelect
              name="Group_Code"
              v-model="header.Group_Code"
              v-if="header"
              :url="$API_URL + 'list/jabatan'"
              :minLength="0"
              :filter="true"
              :disabled="mode == this.$FORM_MODE_VIEW"
              :placeholder="$t('code')"
            />
          </FormField>
          <FormField
            :labelName="$t('description')"
            fieldName="Group_Description"
            v-model="header"
          >
            <InputText
              name="Group_Description"
              :placeholder="$t('description')"
              v-model="header.Group_Description"
              type="text"
              v-if="header"
              :disabled="mode == this.$FORM_MODE_VIEW"
            />
          </FormField>

          <div class="col-12">
            <DataTable
              :loading="!header"
              :value="header ? header.Details : null"
              responsiveLayout="scroll"
              class="p-datatable-sm editable-cells-table"
              :rowClass="hideRow"
            >
              <Column>
                <template #header="">
                  <Button
                    label=""
                    icon="pi pi-plus"
                    class="p-button-primary p-mr-2 p-button-sm"
                    @click="addDetail"
                    :disabled="!canEdit"
                  />
                </template>
                <template #body="slotProps">
                  <Button
                    icon="pi pi-minus"
                    class="p-button-danger p-mr-2 p-button-sm"
                    @click="deleteDetail(slotProps)"
                    :disabled="!canEdit"
                  />
                </template>
              </Column>
              <Column
                field="Menu_Id"
                :header="$t('menu')"
                key="Menu_Id"
                class="w-12"
              >
                <template #body="slotProps">
                  <CustomSelect
                    v-model="slotProps.data[slotProps.column.props.field]"
                    :url="$API_URL + 'list/menu'"
                    :minLength="0"
                    :filter="true"
                    :disabled="!canEdit"
                    @change="onCellEditComplete(slotProps)"
                  />
                </template>
              </Column>
              <Column field="View" :header="$t('view')">
                <template #body="slotProps">
                  <Checkbox
                    v-model="slotProps.data.View"
                    :binary="true"
                    :disabled="!canEdit"
                    @change="onCellEditComplete(slotProps)"
                  />
                </template>
              </Column>
              <Column field="Edit" :header="$t('edit')">
                <template #body="slotProps">
                  <Checkbox
                    v-model="slotProps.data.Edit"
                    :binary="true"
                    :disabled="!canEdit"
                    @change="onCellEditComplete(slotProps)"
                  />
                </template>
              </Column>
              <Column field="Delete" :header="$t('delete')">
                <template #body="slotProps">
                  <Checkbox
                    v-model="slotProps.data.Delete"
                    :binary="true"
                    :disabled="!canEdit"
                    @change="onCellEditComplete(slotProps)"
                  />
                </template>
              </Column>
              <Column field="Create" :header="$t('create')">
                <template #body="slotProps">
                  <Checkbox
                    v-model="slotProps.data.Create"
                    :binary="true"
                    :disabled="!canEdit"
                    @change="onCellEditComplete(slotProps)"
                  />
                </template>
              </Column>
            </DataTable>
          </div>
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
      indexName: "GroupIndex",
      url: this.$API_URL + "group/",
      header: null,
      detail: null,
      mode: "",
      id: 0,
      formEditable: true,
      dataModel: false,
      group: null,
    };
  },
  methods: {
    hideRow(data) {
      return data.mode == this.$FORM_MODE_DELETE ? "row-hidden" : null;
    },
    getData() {
      var self = this;
      this.$axios
        .get(this.url + this.id + "/" + this.mode)
        .then((response) => {
          console.log(response);
          self.header = response.data.data.header;
          self.dataModel = response.data.data.detail;
          self.header.mode = self.mode;
        })
        .catch(function (error) {
          alert(error);
        });
    },
    saveData() {
      this.$emit("block-ui");
      this.$axios
        .post(this.url, this.header)
        .then((response) => {
          console.log(this.header);
          console.log(response);
          if (response.data.success) {
            this.$router.push({
              name: this.indexName,
              params: {
                showToast: true,
                severity: "success",
                summary: "Data Saved",
                detail: response.data.message,
                life: 120000,
              },
            });
          } else {
            this.showError("Error Saving Data", response.data.message);
          }
        })
        .catch(function (error) {
          this.showError("Error Saving Data", error);
        });
    },
    showError(summary, message) {
      this.$emit("unblock-ui");
      this.$toast.add({
        severity: "error",
        summary: summary,
        detail: message,
        life: 120000,
      });
    },
    backToIndex() {
      this.$router.push({ path: "/Group/Index" });
    },
    toggle(event) {
      this.$refs.op.toggle(event);
    },
    confirm(event) {
      this.$confirm.require({
        target: event.currentTarget,
        message: "Are you sure?",
        icon: "pi pi-exclamation-triangle",
        accept: () => {
          this.saveData();
        },
        reject: () => {
          //callback to execute when user rejects the action
        },
      });
    },
    addDetail() {
      var newDetail = this.dataModel;
      newDetail.mode = this.$FORM_MODE_CREATE;
      var det = { ...newDetail };

      this.header.Details.push(det);
    },
    deleteDetail(event) {
      if (event) {
        var detail = this.header.Details[event.index];

        if (detail.mode == this.$FORM_MODE_CREATE)
          this.header.Details.splice(this.header.Details.indexOf(detail), 1);
        else detail.mode = this.$FORM_MODE_DELETE;
      }
    },
    onCellEditComplete(event) {
      this.editDetail(event.index);
    },
    editDetail(index) {
      console.log(this.header);
      if (this.header.Details && this.header.Details.length > 0) {
        let m = this.header.Details[index].mode;

        if (m == this.$FORM_MODE_UNCHANGED) {
          this.header.Details[index].mode = this.$FORM_MODE_EDIT;
        }
      }
    },
  },
  created() {
    this.mode = this.$route.query.mode;
    this.id = this.$route.query.id ?? 0;

    this.getData();
  },
  computed: {
    canEdit() {
      return (
        this.mode == this.$FORM_MODE_CREATE || this.mode == this.$FORM_MODE_EDIT
      );
    },
  },
};
</script>
