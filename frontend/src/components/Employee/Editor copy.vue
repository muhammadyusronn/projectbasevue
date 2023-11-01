<template>
  <form id="mainForm" action="#" method="post">
    <Card>
      <template #title>
        <Toolbar>
          <template #left>
            <div class="p-card-title">[{{ mode }}] {{ $t("Employee") }}</div>
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
                    {{ this.$formatDateTime(header.Created_Date) }}
                  </div>
                  <div class="row">&nbsp;by {{ header.Created_By }}</div>
                </div>
                <div class="grid field" v-if="header.Edited_Date">
                  <div class="row">
                    <i class="pi pi-pencil" />
                    {{ this.$formatDateTime(header.Edited_Date) }}
                  </div>
                  <div class="row">&nbsp;by {{ header.Edited_By }}</div>
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
              v-if="
                mode != this.$FORM_MODE_DELETE && mode != this.$FORM_MODE_VIEW
              "
            />
            <Button
              icon="pi pi-trash"
              label="Delete"
              class="p-button-danger"
              @click="confirm($event)"
              v-if="mode == this.$FORM_MODE_DELETE"
            />
          </template>
        </Toolbar>
      </template>
      <template #content>
        <div class="p-fluid">
          <div class="p-fluid grid">
            <div class="col-4">
              <div class="flex flex-column gap-2 mb-2">
                <label for="email">Email</label>
                <InputText
                  id="email"
                  v-model="employeeData.email"
                  aria-describedby="email-help"
                  :disabled="
                    mode == $FORM_MODE_VIEW || mode == $FORM_MODE_DELETE
                  "
                />
                <small id="email-help"
                  >Enter your valid email (Ex :
                  muhammad.hartoyo@id.wilmar-intl.co.id).</small
                >
              </div>
              <div class="flex flex-column gap-2 mb-2">
                <label for="fullname">Fullname</label>
                <InputText
                  id="fullname"
                  v-model="employeeData.name"
                  :disabled="
                    mode == $FORM_MODE_VIEW || mode == $FORM_MODE_DELETE
                  "
                />
              </div>
              <div class="flex flex-column gap-2 mb-2">
                <label for="departement">Departement</label>
                <InputText
                  id="departement"
                  v-model="employeeData.departement"
                  :disabled="
                    mode == $FORM_MODE_VIEW || mode == $FORM_MODE_DELETE
                  "
                />
              </div>
              <div class="flex flex-column gap-2 mb-2">
                <label for="phone">Phone Number</label>
                <InputText
                  id="phone"
                  v-model="employeeData.phone"
                  :disabled="
                    mode == $FORM_MODE_VIEW || mode == $FORM_MODE_DELETE
                  "
                />
              </div>
              <div class="flex flex-column gap-2 mb-2">
                <label for="salary">salary Number</label>
                <InputNumber
                  v-model="employeeData.salary"
                  inputId="locale-user"
                  :disabled="
                    mode == $FORM_MODE_VIEW || mode == $FORM_MODE_DELETE
                  "
                  :minFractionDigits="0"
                />
              </div>
            </div>
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
      indexName: "EmployeeIndex",
      url: this.$API2_URL + "employee/",
      employeeData: {
        name: "",
        email: "",
        phone: "",
        departement: "",
        salary: 0,
      },
      mode: "",
      id: "",
      formEditable: true,
    };
  },
  methods: {
    confirm(event) {
      this.submitted = true;
      if (this.mode != "Delete") {
        if (
          !this.employeeData.email ||
          !this.employeeData.name ||
          !this.employeeData.phone ||
          !this.employeeData.departement ||
          !this.employeeData.salary
        ) {
          this.showError("ERROR", "Please complete the data!");
          return;
        } else {
          this.saveData();
        }
      } else {
        this.saveData();
      }
    },
    async getData(mode) {
      if (mode != "Create") {
        this.loading = true;

        var self = this;

        this.$axios
          .post(this.$API_URL + this.dataUrl, this.lazyParams)
          .then((response) => {
            if (response.data.success) {
              this.dataModel = response.data.data;
              this.totalRecords = response.data.totalRecords;
            } else {
              self.$toast.add({
                severity: "error",
                summary: "Error",
                detail: response.data.message,
                life: this.$DEFAULT_TIMER,
              });
            }

            this.loading = false;
          })
          .catch(function (err) {
            self.$toast.add({
              severity: "error",
              summary: "Error",
              detail: err.message,
              life: this.$DEFAULT_TIMER,
            });
            self.loading = false;
          });
      }
    },
    async saveData() {
      this.$emit("block-ui");
      if (this.mode == "Create") {
        await this.$axios
          .post(this.url, this.employeeData)
          .then((response) => {
            console.log(response);
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
          })
          .catch(function (error) {
            console.log("err : " + error);
          });
      } else if (this.mode == "Edit") {
        await this.$axios
          .put(this.url + this.$route.query.id, this.employeeData)
          .then((response) => {
            console.log(response);
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
          })
          .catch(function (error) {
            console.log("err : " + error);
          });
      } else if (this.mode == "Delete") {
        await this.$axios
          .delete(this.url + this.$route.query.id)
          .then((response) => {
            console.log(response);
            this.$router.push({
              name: this.indexName,
              params: {
                showToast: true,
                severity: "success",
                summary: "Successfully Delete Data",
                detail: response.data.message,
                life: 120000,
              },
            });
          })
          .catch(function (error) {
            console.log("err : " + error);
          });
      }
    },
    showError(summary, message) {
      this.$toast.add({
        severity: "error",
        summary: summary,
        detail: message,
        life: 120000,
      });
    },
    backToIndex() {
      this.$router.push({ path: "/Employee/Index" });
    },
  },
  created() {
    this.mode = this.$route.query.mode;
    this.id = this.$route.query.id ?? "00000000-0000-0000-0000-000000000000";
    console.log(this.$route.query.mode);
    this.getData(this.$route.query.mode);
  },
  computed: {
    noEdit() {
      return (
        this.mode == this.$FORM_MODE_DELETE || this.mode == this.$FORM_MODE_VIEW
      );
    },
    canEdit() {
      return (
        this.mode == this.$FORM_MODE_CREATE || this.mode == this.$FORM_MODE_EDIT
      );
    },
  },
};
</script>
