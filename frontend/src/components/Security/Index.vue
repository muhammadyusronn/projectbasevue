<template>
  <Card>
    <template #title>
      <Toolbar>
        <template #left>
          <div class="p-card-title">
            {{ $t("security") }}
          </div>
        </template>
      </Toolbar>
    </template>
    <template #content>
      <Button
        label="Add New"
        icon="pi pi-plus"
        iconPos="left"
        @click="editor(this.$FORM_MODE_CREATE, 0)"
      />
      <DataTable
        :value="security"
        paginator
        :rows="5"
        :rowsPerPageOptions="[5, 10, 20, 50]"
        tableStyle="min-width: 50rem"
      >
        <Column
          field="employeeCode"
          header="Employee Code"
          :sortable="true"
        ></Column>
        <Column field="name" header="Name" :sortable="true"></Column>
      </DataTable>
    </template>
  </Card>
</template>

<script>
export default {
  name: "CompanyNew",
  data() {
    return {
      security: null,
      loading: true,
      totalRecords: 0,
      start: 0,
      stateKey:
        this.$STATE_NAME + "security-" + this.$store.getters.getUsername,
      isAdmin: this.$store.getters.getIsAdmin,
      sorts: null,
      lazyParams: {},
      filters: null,
      stateFilters: null,
      editorPath: "/Security/Editor",
      dataUrl: "security/list",
      canCreate: false,
      canEdit: false,
      canDelete: false,
      canView: false,
    };
  },
  methods: {
    sidebarMessage(event) {
      this.$toast.add(event);
    },
    loadLazyData() {
      var self = this;

      this.$axios
        .get(
          "http://localhost:1239/WSePatrolling.asmx/getMaster?table=securityData&username=securitylead.testing&estateCode=CTC01"
        )
        .then((response) => {
          console.log(response.data);
          if ((response.status = 1)) {
            this.security = response.data.data;
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
    },
    editor(mode, id) {
      this.$router.push({
        path: this.editorPath,
        query: { mode: mode, id: id },
      });
    },
  },
  mounted() {
    this.$emit("unblock-ui");
    this.loadLazyData();
  },
  computed: {},
};
</script>

<style #scoped>
.p-card .p-card-content {
  margin: 0 !important;
  padding: 0 !important;
}
</style>
