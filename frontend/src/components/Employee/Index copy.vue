<template>
  <Card>
    <template #title>
      <Toolbar>
        <template #left>
          <div class="p-card-title">
            {{ $t("Employee") }}
          </div>
        </template>
      </Toolbar>
    </template>
    <!--Company Code, Code, Name, Address, City, Fax, Contact Person, Created By...., Blocked -->
    <template #content>
      <Button
        :label="$t('create')"
        icon="pi pi-plus"
        class="p-button-primary p-button-sm"
        @click="editor(this.$FORM_MODE_CREATE, 0)"
      />
      <DataTable
        :value="employeeData"
        paginator
        :rows="5"
        :rowsPerPageOptions="[5, 10, 20, 50]"
        tableStyle="min-width: 50rem"
        dataKey="Id"
      >
        <Column
          :exportable="false"
          :sortable="false"
          headerStyle="white-space:nowrap"
        >
          <template #body="employeeData">
            <Button
              icon="pi pi-search"
              class="p-button-rounded p-button-info mr-2 p-button-sm"
              @click="editor(this.$FORM_MODE_VIEW, employeeData.data.id)"
            />
            <Button
              icon="pi pi-pencil"
              class="p-button-rounded p-button-success mr-2 p-button-sm"
              @click="editor(this.$FORM_MODE_EDIT, employeeData.data.id)"
            />
            <Button
              icon="pi pi-trash"
              class="p-button-rounded p-button-danger mr-2 p-button-sm"
              @click="editor(this.$FORM_MODE_DELETE, employeeData.data.id)"
            />
          </template>
        </Column>
        <Column field="email" header="Email" :sortable="true"></Column>
        <Column field="name" header="Name" :sortable="true"></Column>
        <Column field="phone" header="Phone" :sortable="true"></Column>
        <Column field="salary" header="Salary" :sortable="true"></Column>
        <Column
          field="departement"
          header="Departement"
          :sortable="true"
        ></Column>
      </DataTable>
    </template>
  </Card>
</template>

<script>
export default {
  name: "Employee",
  data() {
    return {
      employeeData: null,
      loading: true,
      totalRecords: 0,
      start: 0,
      stateKey:
        this.$STATE_NAME + "employee-" + this.$store.getters.getUsername,
      isAdmin: this.$store.getters.getIsAdmin,
      sorts: null,
      lazyParams: {},
      filters: null,
      stateFilters: null,
      editorPath: "/Employee/Editor",
      dataUrl: "Employee/list",
      canCreate: true,
      canEdit: true,
      canDelete: true,
      canView: true,
    };
  },
  methods: {
    sidebarMessage(event) {
      this.$toast.add(event);
    },
    async loadLazyData() {
      var self = this;

      await this.$axios
        .get(this.$API2_URL + "Employee")
        .then((response) => {
          if (response.data.length > 0) {
            this.employeeData = response.data;
            console.log(this.employeeData);
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
