<template>
  <form id="mainForm" action="#" method="post">
    <Card>
      <template #title>
        <Toolbar>
          <template #left>
            <div class="p-card-title">[{{ mode }}] Estate</div>
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
                <label for="EstateCode" class="col-4">{{
                  $t("Estate Code")
                }}</label>
                <div class="col-8 md-8">
                  <InputText
                    name="EstateCode"
                    v-model="header.EstateCode"
                    type="text"
                    :placeholder="$t('Estate Code')"
                    v-if="header"
                    :class="{ 'p-invalid': submitted && !header.EstateCode }"
                    :disabled="mode != this.$FORM_MODE_CREATE"
                    :readonly="mode != this.$FORM_MODE_CREATE"
                  />
                  <Skeleton v-else-if="!header" />
                  <small
                    v-if="
                      header &&
                      !header.EstateCode &&
                      submitted &&
                      mode != this.$FORM_MODE_DELETE
                    "
                    class="p-error"
                    >{{ $t("is_required").replace("Value", $t("Estate Code")) }}
                  </small>
                </div>
              </div>
              <div class="field grid">
                <label for="estateAlias" class="col-4">{{
                  $t("Estate Alias")
                }}</label>
                <div class="col-8 md-8">
                  <InputText
                    name="estateAlias"
                    v-model="header.estateAlias"
                    type="text"
                    :placeholder="$t('Estate Alias')"
                    v-if="header"
                    :disabled="
                      mode != this.$FORM_MODE_CREATE &&
                      mode != this.$FORM_MODE_EDIT
                    "
                  />
                  <Skeleton v-else-if="!header" />
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
                <label for="RegionCode" class="col-4">{{ $t("Region") }}</label>
                <div class="col-8 md-8">
                  <CustomSelect
                    name="RegionCode"
                    v-model="header.RegionCode"
                    v-if="header"
                    :url="$API_URL + 'list/region'"
                    :minLength="0"
                    :filter="true"
                    :disabled="
                      mode != this.$FORM_MODE_CREATE &&
                      mode != this.$FORM_MODE_EDIT
                    "
                    :placeholder="$t('Region')"
                    :class="{ 'p-invalid': submitted && !header.RegionCode }"
                    required="true"
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
                    >{{ $t("is_required").replace("Value", $t("Region")) }}
                  </small>
                </div>
              </div>
              <div class="field grid">
                <label for="CompanyCode" class="col-4">{{
                  $t("Company")
                }}</label>
                <div class="col-8 md-8">
                  <CustomSelect
                    name="CompanyCode"
                    v-model="header.CompanyCode"
                    v-if="header"
                    :url="$API_URL + 'list/company'"
                    :minLength="0"
                    :filter="true"
                    :disabled="
                      mode != this.$FORM_MODE_CREATE &&
                      mode != this.$FORM_MODE_EDIT
                    "
                    :placeholder="$t('Company')"
                    :class="{ 'p-invalid': submitted && !header.CompanyCode }"
                    required="true"
                  />
                  <Skeleton v-else-if="!header" />
                  <small
                    v-if="
                      header &&
                      !header.CompanyCode &&
                      submitted &&
                      mode != this.$FORM_MODE_DELETE
                    "
                    class="p-error"
                    >{{ $t("is_required").replace("Value", $t("Company")) }}
                  </small>
                </div>
              </div>
              <div class="field grid">
                <label for="Type" class="col-4">{{ $t("Type") }}</label>
                <div class="col-8 md-8">
                  <InputText
                    required="true"
                    name="Type"
                    v-model="header.Type"
                    type="text"
                    :placeholder="$t('Type')"
                    v-if="header"
                    :class="{ 'p-invalid': submitted && !header.Type }"
                    :disabled="
                      mode != this.$FORM_MODE_CREATE &&
                      mode != this.$FORM_MODE_EDIT
                    "
                  />
                  <Skeleton v-else-if="!header" />
                  <small
                    v-if="
                      header &&
                      !header.Type &&
                      submitted &&
                      mode != this.$FORM_MODE_DELETE
                    "
                    class="p-error"
                    >{{ $t("is_required").replace("Value", $t("Type")) }}
                  </small>
                </div>
              </div>
              <div class="field grid">
                <label for="ProfileName" class="col-4">{{
                  $t("Profile Name")
                }}</label>
                <div class="col-8 md-8">
                  <InputText
                    name="ProfileName"
                    v-model="header.ProfileName"
                    type="text"
                    :placeholder="$t('Profile Name')"
                    v-if="header"
                    :class="{ 'p-invalid': submitted && !header.ProfileName }"
                    :disabled="
                      mode != this.$FORM_MODE_CREATE &&
                      mode != this.$FORM_MODE_EDIT
                    "
                    required="true"
                  />
                  <Skeleton v-else-if="!header" />
                  <small
                    v-if="
                      header &&
                      !header.ProfileName &&
                      submitted &&
                      mode != this.$FORM_MODE_DELETE
                    "
                    class="p-error"
                    >{{
                      $t("is_required").replace("Value", $t("Profile Name"))
                    }}
                  </small>
                </div>
              </div>
              <div class="field grid">
                <label for="PlantCode" class="col-4">{{
                  $t("Plant Code")
                }}</label>
                <div class="col-8 md-8">
                  <InputText
                    name="PlantCode"
                    v-model="header.PlantCode"
                    type="text"
                    :placeholder="$t('Plant Code')"
                    v-if="header"
                    :class="{ 'p-invalid': submitted && !header.PlantCode }"
                    :disabled="
                      mode != this.$FORM_MODE_CREATE &&
                      mode != this.$FORM_MODE_EDIT
                    "
                    required="true"
                  />
                  <Skeleton v-else-if="!header" />
                  <small
                    v-if="
                      header &&
                      !header.PlantCode &&
                      submitted &&
                      mode != this.$FORM_MODE_DELETE
                    "
                    class="p-error"
                    >{{ $t("is_required").replace("Value", $t("Plant Code")) }}
                  </small>
                </div>
              </div>
              <div class="field grid">
                <label for="Storage" class="col-4">{{ $t("Storage") }}</label>
                <div class="col-8 md-8">
                  <InputText
                    name="Storage"
                    v-model="header.Storage"
                    type="text"
                    :placeholder="$t('Storage')"
                    v-if="header"
                    :disabled="
                      mode != this.$FORM_MODE_CREATE &&
                      mode != this.$FORM_MODE_EDIT
                    "
                  />
                  <Skeleton v-else-if="!header" />
                </div>
              </div>
              <div class="field grid">
                <label for="NoophCode" class="col-4">{{
                  $t("No OPH Code")
                }}</label>
                <div class="col-8 md-8">
                  <InputText
                    name="NoophCode"
                    v-model="header.NoophCode"
                    type="text"
                    :placeholder="$t('No OPH Code')"
                    v-if="header"
                    :class="{ 'p-invalid': submitted && !header.NoophCode }"
                    :disabled="
                      mode != this.$FORM_MODE_CREATE &&
                      mode != this.$FORM_MODE_EDIT
                    "
                    required="true"
                  />
                  <Skeleton v-else-if="!header" />
                  <small
                    v-if="
                      header &&
                      !header.NoophCode &&
                      submitted &&
                      mode != this.$FORM_MODE_DELETE
                    "
                    class="p-error"
                    >{{ $t("is_required").replace("Value", $t("No OPH Code")) }}
                  </small>
                </div>
              </div>
              <div class="field grid">
                <label for="estateFingerCode" class="col-4">{{
                  $t("Estate Finger Code")
                }}</label>
                <div class="col-8 md-8">
                  <InputNumber
                    name="estateFingerCode"
                    v-model="header.estateFingerCode"
                    :placeholder="$t('Estate Finger Code')"
                    v-if="header"
                    :class="{
                      'p-invalid': submitted && !header.estateFingerCode,
                    }"
                    :disabled="
                      mode != this.$FORM_MODE_CREATE &&
                      mode != this.$FORM_MODE_EDIT
                    "
                    inputId="withoutgrouping"
                    :useGrouping="false"
                    required="true"
                  />
                  <Skeleton v-else-if="!header" />
                  <small
                    v-if="
                      header &&
                      !header.estateFingerCode &&
                      submitted &&
                      mode != this.$FORM_MODE_DELETE
                    "
                    class="p-error"
                    >{{
                      $t("is_required").replace(
                        "Value",
                        $t("Estate Finger Code")
                      )
                    }}
                  </small>
                </div>
              </div>
              <div class="field grid">
                <label for="templateCode" class="col-4">{{
                  $t("Template Code")
                }}</label>
                <div class="col-8 md-8">
                  <InputText
                    name="templateCode"
                    v-model="header.templateCode"
                    type="text"
                    :placeholder="$t('Template Code')"
                    v-if="header"
                    :class="{ 'p-invalid': submitted && !header.templateCode }"
                    :disabled="
                      mode != this.$FORM_MODE_CREATE &&
                      mode != this.$FORM_MODE_EDIT
                    "
                  />
                  <Skeleton v-else-if="!header" />
                  <small
                    v-if="
                      header &&
                      !header.templateCode &&
                      submitted &&
                      mode != this.$FORM_MODE_DELETE
                    "
                    class="p-error"
                    >{{
                      $t("is_required").replace("Value", $t("Template Code"))
                    }}
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
                    :class="{ 'p-invalid': submitted && !header.isActive }"
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
      indexName: "EstateIndex",
      url: this.$API_URL + "estate/",
      header: null,
      detail: null,
      mode: "",
      submitted: false,
      id: 0,
      formEditable: true,
    };
  },
  methods: {
    getData() {
      var self = this;
      this.$axios
        .get(this.url + this.id + "/" + this.mode)
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
                  detail:
                    "Estate " +
                    self.header.Name +
                    " has been deleted successfully",
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
                    "Estate " +
                    self.header.Name +
                    " has been saved successfully",
                  life: 3000,
                },
              });
            }
          } else {
            console.log(response);
            self.showError("Error Saving Data", response.data.message);
          }
        })
        .catch(function (error) {
          console.log(error);
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
      this.$router.push({ path: "/Estate/Index" });
    },
    toggle(event) {
      this.$refs.op.toggle(event);
    },
    confirm(event) {
      this.submitted = true;
      if (!this.header.EstateCode || !this.header.Name) {
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
    this.id = this.$route.query.id ?? 0;

    this.getData();
  },
};
</script>
