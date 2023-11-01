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
            <div class="col-6">
              <div class="field grid">
                <label for="name" class="col-4">Employee Name</label>
                <div class="col-8 md-8">
                  <InputText
                    name="name"
                    v-model="header.name"
                    type="text"
                    :placeholder="Name"
                    v-if="header"
                    :class="{ 'p-invalid': submitted && !header.name }"
                    :disabled="
                      mode != this.$FORM_MODE_CREATE &&
                      mode != this.$FORM_MODE_EDIT
                    "
                    :readonly="
                      mode != this.$FORM_MODE_CREATE &&
                      mode != this.$FORM_MODE_EDIT
                    "
                  />
                  <small
                    v-if="
                      header &&
                      !header.name &&
                      submitted &&
                      mode != this.$FORM_MODE_DELETE
                    "
                    class="p-error"
                    >{{ $t("is_required").replace("Value", "Employee Name") }}
                  </small>
                  <Skeleton v-else-if="!header" />
                </div>
              </div>
              <div class="field grid">
                <label for="email" class="col-4">Email</label>
                <div class="col-8 md-8">
                  <InputText
                    name="email"
                    v-model="header.email"
                    required="true"
                    type="text"
                    :placeholder="email"
                    v-if="header"
                    :class="{ 'p-invalid': submitted && !header.email }"
                    :readonly="
                      mode != this.$FORM_MODE_CREATE &&
                      mode != this.$FORM_MODE_EDIT
                    "
                    :disabled="
                      mode != this.$FORM_MODE_CREATE &&
                      mode != this.$FORM_MODE_EDIT
                    "
                  />
                  <small
                    v-if="
                      header &&
                      !header.email &&
                      submitted &&
                      mode != this.$FORM_MODE_DELETE
                    "
                    class="p-error"
                    >{{ $t("is_required").replace("Value", "Email Employee") }}
                  </small>
                  <Skeleton v-else-if="!header" />
                </div>
              </div>
              <div class="field grid">
                <label for="phone" class="col-4">Phone</label>
                <div class="col-8 md-8">
                  <InputNumber
                    name="phone"
                    inputId="withoutgrouping"
                    :useGrouping="false"
                    v-model="header.phone"
                    required="true"
                    type="text"
                    :placeholder="phone"
                    v-if="header"
                    :class="{ 'p-invalid': submitted && !header.phone }"
                    :readonly="
                      mode != this.$FORM_MODE_CREATE &&
                      mode != this.$FORM_MODE_EDIT
                    "
                    :disabled="
                      mode != this.$FORM_MODE_CREATE &&
                      mode != this.$FORM_MODE_EDIT
                    "
                  />
                  <small
                    v-if="
                      header &&
                      !header.phone &&
                      submitted &&
                      mode != this.$FORM_MODE_DELETE
                    "
                    class="p-error"
                    >{{ $t("is_required").replace("Value", "Phone Employee") }}
                  </small>
                  <Skeleton v-else-if="!header" />
                </div>
              </div>
              <div class="field grid">
                <label for="salary" class="col-4">salary</label>
                <div class="col-8 md-8">
                  <InputNumber
                    inputId="integeronly"
                    name="salary"
                    v-model="header.salary"
                    required="true"
                    type="text"
                    :placeholder="salary"
                    v-if="header"
                    :class="{ 'p-invalid': submitted && !header.salary }"
                    :readonly="
                      mode != this.$FORM_MODE_CREATE &&
                      mode != this.$FORM_MODE_EDIT
                    "
                    :disabled="
                      mode != this.$FORM_MODE_CREATE &&
                      mode != this.$FORM_MODE_EDIT
                    "
                  />
                  <small
                    v-if="
                      header &&
                      !header.salary &&
                      submitted &&
                      mode != this.$FORM_MODE_DELETE
                    "
                    class="p-error"
                    >{{ $t("is_required").replace("Value", "salary Employee") }}
                  </small>
                  <Skeleton v-else-if="!header" />
                </div>
              </div>
              <div class="field grid">
                <label for="departement" class="col-4">Departement</label>
                <div class="col-8 md-8">
                  <InputText
                    name="departement"
                    v-model="header.departement"
                    required="true"
                    type="text"
                    :placeholder="departement"
                    v-if="header"
                    :class="{ 'p-invalid': submitted && !header.departement }"
                    :readonly="
                      mode != this.$FORM_MODE_CREATE &&
                      mode != this.$FORM_MODE_EDIT
                    "
                    :disabled="
                      mode != this.$FORM_MODE_CREATE &&
                      mode != this.$FORM_MODE_EDIT
                    "
                  />
                  <small
                    v-if="
                      header &&
                      !header.departement &&
                      submitted &&
                      mode != this.$FORM_MODE_DELETE
                    "
                    class="p-error"
                    >{{
                      $t("is_required").replace("Value", "Departement Employee")
                    }}
                  </small>
                  <Skeleton v-else-if="!header" />
                </div>
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
      url: this.$API_URL + "employee/",
      header: null,
      mode: "",
      submitted: false,
      id: "",
      formEditable: true,
    };
  },
  methods: {
    getData() {
      var self = this;
      this.$axios
        .get(this.url + this.id + "/" + this.mode)
        .then((response) => {
          self.header = response.data.data.header;
          self.header.Id = this.$route.query.id;
          self.header.mode = self.mode;
          self.detail = response.data.data.location;
        })
        .catch(function (error) {
          alert(error);
        });
    },
    saveData() {
      console.log(this.header);
      this.$emit("block-ui");
      var self = this;
      this.$axios
        .post(this.url, this.header)
        .then((response) => {
          if (response.data.success) {
            if (this.mode == this.$FORM_MODE_DELETE) {
              self.$router.push({
                name: self.indexName,
                params: {
                  showToast: true,
                  severity: "success",
                  summary: "Data Deleted",
                  detail: "Employee has been deleted successfully",
                  life: 3000,
                },
              });
            } else {
              self.$router.push({
                name: self.indexName,
                params: {
                  showToast: true,
                  severity: "success",
                  summary: "Data Saved",
                  detail: "Employee has been saved successfully",
                  life: 3000,
                },
              });
            }
          } else {
            self.showError("Error Saving Data", response.data.message);
          }
        })
        .catch(function (error) {
          self.showError("Error Saving Data", error);
        });
    },
    showError(summary, message) {
      this.$emit("unblock-ui");
      this.$toast.add({
        severity: "error",
        summary: summary,
        detail: message,
        life: 3000,
      });
    },
    backToIndex() {
      this.$router.push({ path: "/Employee/Index" });
    },
    toggle(event) {
      this.$refs.op.toggle(event);
    },
    confirm(event) {
      this.submitted = true;
      if (
        !this.header.name ||
        !this.header.email ||
        !this.header.salary ||
        !this.header.phone ||
        !this.header.departement
      ) {
        return;
      } else if (this.header.Details == 0) {
        this.showError("Error Saving Data", "Must have at least one detail");
      } else {
        this.saveData();
      }
    },
    hideRow(data) {
      return data.mode == this.$FORM_MODE_DELETE ? "row-hidden" : null;
    },
  },
  created() {
    this.mode = this.$route.query.mode;
    this.id = this.$route.query.id;
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
