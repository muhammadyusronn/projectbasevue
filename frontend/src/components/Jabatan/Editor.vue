<template>
  <form id="mainForm" action="#" method="post">
    <Card>
      <template #title>
        <Toolbar>
          <template #left>
            <div class="p-card-title">[{{ mode }}] Jabatan</div>
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
            <ConfirmPopup></ConfirmPopup>
          </template>
        </Toolbar>
      </template>
      <template #content>
        <div class="p-inputtext-sm p-fluid">
          <div class="grid">
            <div class="col-6">
              <div class="field grid">
                <label for="JabatanCode" class="col-4">{{
                  $t("Jabatan Code")
                }}</label>
                <div class="col-8 md-8">
                  <InputText
                    name="JabatanCode"
                    v-model="header.JabatanCode"
                    type="text"
                    :placeholder="$t('Jabatan Code')"
                    v-if="header"
                    :class="{ 'p-invalid': submitted && !header.JabatanCode }"
                    :disabled="mode != this.$FORM_MODE_CREATE"
                    :readonly="mode != this.$FORM_MODE_CREATE"
                  />
                  <Skeleton v-else-if="!header" />
                  <small
                    v-if="
                      header &&
                      !header.JabatanCode &&
                      submitted &&
                      mode != this.$FORM_MODE_DELETE
                    "
                    class="p-error"
                    >{{
                      $t("is_required").replace("Value", $t("Jabatan Code"))
                    }}
                  </small>
                </div>
              </div>
              <div class="field grid">
                <label for="Name" class="col-4">{{ $t("Name") }}</label>
                <div class="col-8 md-8">
                  <InputText
                    name="Name"
                    v-model="header.Name"
                    required="true"
                    type="text"
                    :placeholder="$t('Name')"
                    v-if="header"
                    :class="{ 'p-invalid': submitted && !header.Name }"
                    :disabled="
                      mode != this.$FORM_MODE_CREATE &&
                      mode != this.$FORM_MODE_EDIT
                    "
                  />
                  <Skeleton v-else-if="!header" />
                  <small
                    v-if="
                      header &&
                      !header.Name &&
                      submitted &&
                      mode != this.$FORM_MODE_DELETE
                    "
                    class="p-error"
                    >{{ $t("is_required").replace("Value", $t("Name")) }}
                  </small>
                </div>
              </div>
              <div class="field grid">
                <label for="Seq" class="col-4">{{ $t("Seq") }}</label>
                <div class="col-8 md-8">
                  <InputText
                    name="Seq"
                    v-model="header.Seq"
                    required="true"
                    type="text"
                    :placeholder="$t('Seq')"
                    v-if="header"
                    :class="{ 'p-invalid': submitted && !header.Seq }"
                    :disabled="
                      mode != this.$FORM_MODE_CREATE &&
                      mode != this.$FORM_MODE_EDIT
                    "
                  />
                  <Skeleton v-else-if="!header" />
                  <small
                    v-if="
                      header &&
                      !header.Seq &&
                      submitted &&
                      mode != this.$FORM_MODE_DELETE
                    "
                    class="p-error"
                    >{{ $t("is_required").replace("Value", $t("Seq")) }}
                  </small>
                </div>
              </div>
              <div class="field grid">
                <label for="Area" class="col-4">{{ $t("Area") }}</label>
                <div class="col-8 md-8">
                  <InputText
                    name="Area"
                    v-model="header.Area"
                    required="true"
                    type="text"
                    :placeholder="$t('Area')"
                    v-if="header"
                    :class="{ 'p-invalid': submitted && !header.Area }"
                    :disabled="
                      mode != this.$FORM_MODE_CREATE &&
                      mode != this.$FORM_MODE_EDIT
                    "
                  />
                  <Skeleton v-else-if="!header" />
                  <small
                    v-if="
                      header &&
                      !header.Area &&
                      submitted &&
                      mode != this.$FORM_MODE_DELETE
                    "
                    class="p-error"
                    >{{ $t("is_required").replace("Value", $t("Area")) }}
                  </small>
                </div>
              </div>
              <div class="field grid">
                <label for="Priority" class="col-4">{{ $t("Priority") }}</label>
                <div class="col-8 md-8">
                  <InputText
                    name="Priority"
                    v-model="header.Priority"
                    required="true"
                    type="text"
                    :placeholder="$t('Priority')"
                    v-if="header"
                    :class="{ 'p-invalid': submitted && !header.Priority }"
                    :disabled="
                      mode != this.$FORM_MODE_CREATE &&
                      mode != this.$FORM_MODE_EDIT
                    "
                  />
                  <Skeleton v-else-if="!header" />
                  <small
                    v-if="
                      header &&
                      !header.Priority &&
                      submitted &&
                      mode != this.$FORM_MODE_DELETE
                    "
                    class="p-error"
                    >{{ $t("is_required").replace("Value", $t("Priority")) }}
                  </small>
                </div>
              </div>
              <div class="field grid">
                <label for="Granted" class="col-4">{{ $t("Granted") }}</label>
                <div class="col-8 md-8">
                  <InputText
                    name="Granted"
                    v-model="header.Granted"
                    required="true"
                    type="text"
                    :placeholder="$t('Granted')"
                    v-if="header"
                    :class="{ 'p-invalid': submitted && !header.Granted }"
                    :disabled="
                      mode != this.$FORM_MODE_CREATE &&
                      mode != this.$FORM_MODE_EDIT
                    "
                  />
                  <Skeleton v-else-if="!header" />
                  <small
                    v-if="
                      header &&
                      !header.Granted &&
                      submitted &&
                      mode != this.$FORM_MODE_DELETE
                    "
                    class="p-error"
                    >{{ $t("is_required").replace("Value", $t("Granted")) }}
                  </small>
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
      indexName: "JabatanIndex",
      url: this.$API_URL + "Jabatan/",
      header: null,
      detail: null,
      mode: "",
      submitted: false,
      JabatanCode: "",
      formEditable: true,
    };
  },
  methods: {
    getData() {
      var self = this;
      this.$axios
        .get(this.url + this.JabatanCode + "/" + this.mode)
        .then((response) => {
          self.header = response.data.data.header;
          self.header.mode = self.mode;
        })
        .catch(function (error) {
          alert(error);
        });
    },
    saveData() {
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
                  detail: "Jabatan has been deleted successfully",
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
                  detail: "Jabatan has been saved successfully",
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
      this.$router.push({ path: "/Jabatan/Index" });
    },
    toggle(event) {
      this.$refs.op.toggle(event);
    },
    confirm(event) {
      this.submitted = true;
      if (
        !this.header.JabatanCode ||
        !this.header.Name ||
        !this.header.Seq ||
        !this.header.Area ||
        !this.header.Priority ||
        !this.header.Granted
      ) {
        return;
      } else {
        this.$confirm.require({
          target: event.currentTarget,
          message: "Are you sure?",
          icon: "pi pi-exclamation-triangle",
          accept: () => {
            this.saveData();
          },
          reject: () => {},
        });
      }
    },
    hideRow(data) {
      return data.mode == this.$FORM_MODE_DELETE ? "row-hidden" : null;
    },
    addDetail() {
      var newDetail = this.detail;
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
      if (this.header && this.header.Details) {
        let mode = this.header.Details[index].mode;

        if (mode == this.$FORM_MODE_UNCHANGED) {
          this.header.Details[index].mode = this.$FORM_MODE_EDIT;
        }
      }
    },
  },
  created() {
    this.mode = this.$route.query.mode;
    this.JabatanCode = this.$route.query.JabatanCode ?? "CRTZ";

    this.getData();
  },
};
</script>
