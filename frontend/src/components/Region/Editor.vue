<template>
  <form id="mainForm" action="#" method="post">
    <Card>
      <template #title>
        <Toolbar>
          <template #left>
            <div class="p-card-title">[{{ mode }}] Region</div>
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
                <label for="RegionCode" class="col-4">{{
                  $t("Region Code")
                }}</label>
                <div class="col-8 md-8">
                  <InputText
                    name="RegionCode"
                    v-model="header.RegionCode"
                    type="text"
                    :placeholder="$t('RegionCode')"
                    v-if="header"
                    :class="{ 'p-invalid': submitted && !header.RegionCode }"
                    :disabled="mode != this.$FORM_MODE_CREATE"
                    :readonly="mode != this.$FORM_MODE_CREATE"
                  />
                  <Skeleton v-else-if="!header" />
                  <small
                    v-if="
                      header &&
                      !header.RegionCode &&
                      submitted &&
                      mode != this.$FORM_MODE_DELETE
                    "
                    class="p-error"
                    >{{ $t("is_required").replace("Value", $t("Region Code")) }}
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
                <label for="CountryCode" class="col-4">{{
                  $t("Country")
                }}</label>
                <div class="col-8 md-8">
                  <CustomSelect
                    name="CountryCode"
                    v-model="header.CountryCode"
                    v-if="header"
                    :url="$API_URL + 'list/country'"
                    :minLength="0"
                    :filter="true"
                    :disabled="
                      mode != this.$FORM_MODE_CREATE &&
                      mode != this.$FORM_MODE_EDIT
                    "
                    :placeholder="$t('Country')"
                    :class="{ 'p-invalid': submitted && !header.CountryCode }"
                    required="true"
                  />
                  <Skeleton v-else-if="!header" />
                  <small
                    v-if="
                      header &&
                      !header.CountryCode &&
                      submitted &&
                      mode != this.$FORM_MODE_DELETE
                    "
                    class="p-error"
                    >{{ $t("is_required").replace("Value", $t("Country")) }}
                  </small>
                </div>
              </div>
              <div class="field grid">
                <label for="isActive" class="col-4">{{
                  $t("Is Active")
                }}</label>
                <div class="col-8 md-8">
                  <Checkbox
                    name="isActive"
                    v-model="header.isActive"
                    v-if="header"
                    :binary="true"
                    :disabled="
                      mode != this.$FORM_MODE_CREATE &&
                      mode != this.$FORM_MODE_EDIT
                    "
                  />
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
      indexName: "RegionIndex",
      url: this.$API_URL + "Region/",
      header: null,
      detail: null,
      mode: "",
      submitted: false,
      RegionCode: "0",
      formEditable: true,
    };
  },
  methods: {
    getData() {
      var self = this;
      this.$axios
        .get(this.url + this.RegionCode + "/" + this.mode)
        .then((response) => {
          response.data.data.header.isActive =
            response.data.data.header.isActive == "1" ? true : false;
          self.header = response.data.data.header;
          self.header.mode = self.mode;
        })
        .catch(function (error) {
          alert(error);
        });
    },
    saveData() {
      this.header.isActive = this.header.isActive ? 1 : 0;
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
                  detail: "Region has been deleted successfully",
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
                  detail:
                    "Region " +
                    self.header.Name +
                    " has been saved successfully",
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
      this.$router.push({ path: "/Region/Index" });
    },
    toggle(event) {
      this.$refs.op.toggle(event);
    },
    confirm(event) {
      this.submitted = true;
      if (
        !this.header.Name ||
        !this.header.RegionCode ||
        !this.header.CountryCode
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
    this.RegionCode = this.$route.query.RegionCode ?? 0;

    this.getData();
  },
};
</script>
