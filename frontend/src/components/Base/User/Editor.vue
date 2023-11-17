<template>
  <form id="mainForm" action="#" method="post">
    <Card>
      <template #title>
        <Toolbar>
          <template #left>
            <div class="p-card-title">[{{ mode }}] {{ $t("user") }}</div>
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
                <div class="p-grid p-field">
                  <div class="p-row">
                    <i class="pi pi-plus-circle" />
                    {{ this.$formatDateTime(header.Create_Date) }}
                  </div>
                  <div class="p-row">by {{ header.Create_By }}</div>
                </div>
                <div class="p-grid p-field" v-if="header.Edit_Date">
                  <div class="p-row">
                    <i class="pi pi-pencil" />
                    {{ this.$formatDateTime(header.Edit_Date) }}
                  </div>
                  <div class="p-row">by {{ header.Edit_By }}</div>
                </div>
              </OverlayPanel>
            </div>
          </template>

          <template #right>
            <Button
              icon="pi pi-chevron-left"
              label="Back"
              class="p-button mr-2"
              @click="backToIndex"
            />
            <Button
              icon="pi pi-save"
              label="Save"
              class="p-button-success"
              @click="confirm($event)"
              v-if="canSave"
            />
            <ConfirmPopup></ConfirmPopup>
          </template>
        </Toolbar>
      </template>
      <template #content>
        <div class="p-inputtext-sm p-fluid">
          <TabView>
            <TabPanel header="Header Groups">
              <div v-if="header && isOverNotAccess">
                <Message severity="warn" :closable="false">
                  {{ $t("Account Status :") }} ({{
                    Is_Active ? "Active" : "Not Active"
                  }})
                </Message>
              </div>
              <div class="grid">
                <div class="col-6">
                  <FormField
                    :labelName="$t('Username')"
                    fieldName="Username"
                    v-model="header"
                    labelClass="col-4"
                    fieldClass="col-6 md-6"
                  >
                    <InputText
                      name="Username"
                      v-model="header.Username"
                      type="text"
                      :placeholder="$t('Username')"
                      v-if="header"
                      :disabled="!canEdit"
                    />
                  </FormField>

                  <FormField
                    :labelName="$t('Full_Name')"
                    fieldName="Full_Name"
                    v-model="header"
                    labelClass="col-4"
                    fieldClass="col-6 md-6"
                  >
                    <InputText
                      name="Full_Name"
                      v-model="header.Full_Name"
                      type="text"
                      :placeholder="$t('Full_Name')"
                      v-if="header"
                      :disabled="!canEdit"
                    />
                  </FormField>

                  <FormField
                    :labelName="$t('email')"
                    fieldName="Email"
                    v-model="header"
                    labelClass="col-4"
                    fieldClass="col-6 md-6"
                  >
                    <InputText
                      name="Email"
                      v-model="header.Email"
                      type="text"
                      :placeholder="$t('email')"
                      v-if="header"
                      :disabled="!canEdit"
                    />
                  </FormField>

                  <FormField
                    :labelName="$t('location')"
                    fieldName="Location_Code"
                    v-model="header"
                    labelClass="col-4"
                    fieldClass="col-6 md-6"
                  >
                    <CustomSelect
                      class="p-inputtext-sm"
                      v-model="header.Location_Code"
                      v-if="header"
                      :url="this.$API_URL + 'list/location_code'"
                      :minLength="0"
                      :params="{}"
                      :disabled="!canEdit"
                    />
                  </FormField>

                  <FormField
                    :labelName="$t('department')"
                    fieldName="Department"
                    v-model="header"
                    labelClass="col-4"
                    fieldClass="col-6 md-6"
                  >
                    <CustomSelect
                      name="Department"
                      v-model="header.Department"
                      v-if="header"
                      :url="$API_URL + 'list/department_id'"
                      :minLength="0"
                      :filter="true"
                      :disabled="!canEdit"
                      :placeholder="$t('department')"
                    />
                  </FormField>

                  <FormField
                    :labelName="$t('Estate')"
                    fieldName="EstateCode"
                    v-model="header"
                    labelClass="col-4"
                    fieldClass="col-6 md-6"
                  >
                    <CustomSelect
                      name="Department"
                      v-model="header.EstateCode"
                      v-if="header"
                      :url="$API_URL + 'list/estate'"
                      :minLength="0"
                      :filter="true"
                      :disabled="!canEdit"
                      :placeholder="$t('Estate')"
                    />
                  </FormField>

                  <FormField
                    :labelName="$t('use_ad')"
                    fieldName="Use_AD"
                    v-model="header"
                    labelClass="col-4"
                    fieldClass="col-6 md-6"
                  >
                    <Checkbox
                      v-model="header.Use_AD"
                      :disabled="!canEdit"
                      v-if="header"
                      :binary="true"
                    />
                  </FormField>

                  <FormField
                    v-if="mode != this.$FORM_MODE_CREATE"
                    :labelName="$t('password')"
                    fieldName="Use_AD"
                    v-model="header"
                    labelClass="col-4"
                    fieldClass="col-6 md-6"
                  >
                    <!-- <InputText name="Email" v-model="header.Email" type="text" :placeholder="$t('email')"  v-if="header" :disabled="mode == this.$FORM_MODE_VIEW"  /> -->
                    <!-- <InputText type="password" v-model="header.Password" :disabled="!canEdit" v-if="header && header.Use_AD"/> -->
                    <Button
                      label="Reset Password"
                      class="p-button-success"
                      :disabled="!canEdit"
                      v-if="header && !header.Use_AD"
                      @click="confirmResetPass($event)"
                    />
                    <ConfirmPopup></ConfirmPopup>
                  </FormField>

                  <FormField
                    :labelName="$t('Is Active')"
                    fieldName="IsActive"
                    v-model="header"
                    labelClass="col-4"
                    fieldClass="col-6 md-6"
                  >
                    <Checkbox
                      v-model="header.IsActive"
                      :disabled="!canEdit"
                      v-if="header"
                      :binary="true"
                    />
                  </FormField>
                </div>
                <div class="col-6">
                  <Groups
                    :dataArray="header.Groups"
                    :dataModel="groupModel"
                    :mode="mode"
                    v-if="header"
                    :header="header"
                  />
                </div>
              </div>
            </TabPanel>
          </TabView>
        </div>
      </template>
    </Card>
  </form>
</template>

<script>
import Groups from "./_Groups.vue";

//import Locations from './_Locations.vue';

export default {
  name: "Editor",
  components: {
    Groups: Groups,
  },
  data() {
    return {
      indexName: "UserIndex",
      url: this.$API_URL + "user/",
      resetPassUrl: this.$API_URL + "user/reset_password",
      header: null,
      detail: null,
      mode: "",
      id: 0,
      formEditable: true,
      changePassword: false,
      deparmentmodel: null,
      username: null,
      Is_Active: false,
      displayDialog: false,
      inactive_msg: "",
    };
  },
  methods: {
    getData() {
      var self = this;
      this.$axios
        .get(this.url + this.id + "/" + this.mode)
        .then((response) => {
          console.log(response);
          response.data.data.header.Use_AD =
            response.data.data.header.Use_AD == "Y" ? true : false;
          response.data.data.header.IsActive =
            response.data.data.header.IsActive == "Y" ? true : false;
          self.header = response.data.data.header;
          self.groupModel = response.data.data.group;
          self.Is_Active = response.data.data.header.IsActive;
        })
        .catch(function (error) {
          console.log(error);
        });
    },
    saveData() {
      var self = this;
      this.$emit("block-ui");
      this.header.mode = this.mode;

      this.$axios
        .post(this.url, this.header)
        .then((response) => {
          if (response.data.success) {
            console.log(this.header);
            self.$router.push({
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
            self.$emit("unblock-ui");
            self.$toast.add({
              severity: "error",
              summary: "Error Saving Data",
              detail: response.data.message,
              life: 120000,
            });
          }
        })
        .catch(function (error) {
          self.$toast.add({
            severity: "error",
            summary: "Error Saving Data",
            detail: error,
            life: 3000,
          });
          self.$emit("unblock-ui");
        });
    },
    saveLockUser() {
      var self = this;
      var data = {
        user_id: this.header.Id,
        is_locked: this.Is_Locked,
      };

      this.$emit("block-ui");
      this.$axios
        .post(this.$API_URL + "util/user_lock_unlock/", data)
        .then((response) => {
          self.getData();
          self.$emit("unblock-ui");
          self.closeDialog();
          if (response.data.success) {
            self.$toast.add({
              severity: "success",
              summary: "Success Saving Data",
              detail: "User account has been saved successfully.",
              life: 50000,
            });
          } else {
            self.$toast.add({
              severity: "error",
              summary: "Error Saving Data",
              detail: error,
              life: 5000,
            });
          }
        })
        .catch(function (error) {
          self.$toast.add({
            severity: "error",
            summary: "Error Saving Data",
            detail: error,
            life: 5000,
          });
          self.$emit("unblock-ui");
        });
    },
    resetPassword() {
      var self = this;
      this.$emit("block-ui");
      this.header.mode = this.mode;

      this.$axios
        .post(this.resetPassUrl, this.header)
        .then((response) => {
          if (response.data.success) {
            // self.$router.push({ name: this.indexName, params: { showToast: true, severity: 'success', summary: 'Password Reset', detail: response.data.message, life: 120000} } );
            self.$toast.add({
              severity: "success",
              summary: "Password Reset",
              detail: response.data.message,
              life: 120000,
            });

            // self.changePassword = true;
            // this.username = this.header.Employee_Number;

            self.$emit("unblock-ui");
          } else {
            self.$emit("unblock-ui");
            self.$toast.add({
              severity: "error",
              summary: "Error Saving Data",
              detail: response.data.message,
              life: 120000,
            });
          }
        })
        .catch(function (error) {
          self.$toast.add({
            severity: "error",
            summary: "Error Saving Data",
            detail: error,
            life: 3000,
          });
          self.$emit("unblock-ui");
        });
    },
    // hideChangePasswordDialog(event){
    //     self = this;
    //     self.changePassword = false;
    //     // this.errormessage = "";

    //     if(event && event.success){
    //         // this.changePasswordSuccess = event.message;
    //         self.$toast.add({ severity: 'success', summary: 'Password Change', detail: event.message, life: 120000 });
    //     }
    // },
    backToIndex() {
      this.$router.push({ path: "/User/Index" });
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
    confirmResetPass(event) {
      this.$confirm.require({
        target: event.currentTarget,
        message: "Are you sure?",
        icon: "pi pi-exclamation-triangle",
        accept: () => {
          this.resetPassword();
        },
        reject: () => {
          //callback to execute when user rejects the action
        },
      });
    },
    hideRow(data) {
      return data.mode == this.$FORM_MODE_DELETE ? "row-hidden" : null;
    },
    openDialog() {
      this.inactiveMsg();
      this.displayDialog = true;
    },
    closeDialog() {
      this.displayDialog = false;
      this.Is_Locked = this.header.Is_Blocked;
    },
    inactiveMsg() {
      var userAccount = this.header;
      var lastAccessDate = new Date(
        this.$moment(userAccount.Last_Access_Date).format("L")
      );
      var currentDate = new Date(this.$moment(new Date()).format("L"));

      var lastDuration = currentDate.getTime() - lastAccessDate.getTime();
      var totalDays = lastDuration / (1000 * 3600 * 24);

      if (isNaN(totalDays)) {
        this.inactive_msg = "This account has never been active. ";
      } else {
        this.inactive_msg =
          "This account has been inactive for " + totalDays + " days. ";
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
    canSave() {
      return this.mode != this.$FORM_MODE_VIEW;
    },
    isOverNotAccess() {
      var duration = 90;
      var userAccount = this.header;
      var lastAccessDate = new Date(
        this.$moment(userAccount.Last_Access_Date).format("L")
      );
      var currentDate = new Date(this.$moment(new Date()).format("L"));

      if (userAccount.Last_Access_Date == null) {
        return true;
      } else {
        var lastDuration = currentDate.getTime() - lastAccessDate.getTime();
        var totalDays = lastDuration / (1000 * 3600 * 24);

        if (totalDays >= duration) {
          return true;
        }

        return false;
      }
    },
  },
};
</script>
