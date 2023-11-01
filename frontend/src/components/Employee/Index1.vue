<template>
  <IndexBase
    @auth="onAuth($event)"
    action="Index"
    controller="Employee"
  ></IndexBase>
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
      <DataTable
        ref="dt"
        responsiveLayout="scroll"
        class="p-datatable-sm"
        filterDisplay="row"
        sortMode="multiple"
        removableSort
        stateStorage="local"
        :stateKey="stateKey"
        :value="employeeData"
        :loading="!employeeData || loading"
        paginator
        :rows="10"
        :lazy="true"
        v-model:first="this.start"
        @state-restore="onStateRestore($event)"
        @page="onPage($event)"
        @sort="onSort($event)"
        @filter="onFilter($event)"
        v-model:filters="filters"
        :totalRecords="totalRecords"
        dataKey="Id"
      >
        <Column
          :exportable="false"
          :sortable="false"
          headerStyle="white-space:nowrap"
        >
          <template #header="">
            <Button
              v-show="canCreate"
              :label="$t('create')"
              icon="pi pi-plus"
              class="p-button-primary p-button-sm"
              @click="editor(this.$FORM_MODE_CREATE, 0)"
            />
          </template>
          <template #filter>
            <Button
              :label="$t('clear')"
              icon="pi pi-filter-slash"
              class="p-button-outlined p-button-primary p-button-sm"
              @click="clearFilter"
              :badge="filterBadge"
              badgeClass="p-badge-danger"
            />
          </template>
          <template #body="employeeData">
            <Button
              icon="pi pi-search"
              v-show="canView"
              class="p-button-rounded p-button-info mr-2 p-button-sm"
              @click="editor(this.$FORM_MODE_VIEW, employeeData.data.Id)"
            />
            <Button
              icon="pi pi-pencil"
              v-show="canEdit"
              class="p-button-rounded p-button-success mr-2 p-button-sm"
              @click="editor(this.$FORM_MODE_EDIT, employeeData.data.Id)"
            />
            <Button
              icon="pi pi-trash"
              v-show="canDelete"
              class="p-button-rounded p-button-danger mr-2 p-button-sm"
              @click="editor(this.$FORM_MODE_DELETE, employeeData.data.Id)"
            />
          </template>
        </Column>
        <Column field="email" :header="Email" :sortable="true">
          <template #filter="{ filterModel, filterCallback }">
            <InputText
              type="text"
              :placeholder="Email"
              v-if="filterModel"
              v-model="filterModel.value"
              @keydown.enter="filterCallback()"
              class="p-column-filter p-inputtext-sm"
            />
          </template>
        </Column>
        <Column field="name" :header="Name" :sortable="true">
          <template #filter="{ filterModel, filterCallback }">
            <InputText
              type="text"
              :placeholder="Name"
              v-if="filterModel"
              v-model="filterModel.value"
              @keydown.enter="filterCallback()"
              class="p-column-filter p-inputtext-sm"
            />
          </template>
        </Column>
        <Column field="phone" :header="Phone" :sortable="true">
          <template #filter="{ filterModel, filterCallback }">
            <InputNumber
              name="phone"
              inputId="withoutgrouping"
              :useGrouping="false"
              :placeholder="Phone"
              v-if="filterModel"
              v-model="filterModel.value"
              @keydown.enter="filterCallback()"
              class="p-column-filter p-inputtext-sm"
            />
          </template>
        </Column>
        <Column field="salary" :header="Salary" :sortable="true">
          <template #filter="{ filterModel, filterCallback }">
            <InputNumber
              inputId="integeronly"
              name="salary"
              :placeholder="Salary"
              v-if="filterModel"
              v-model="filterModel.value"
              @keydown.enter="filterCallback()"
              class="p-column-filter p-inputtext-sm"
            />
          </template>
        </Column>
        <Column field="departement" :header="Departement" :sortable="true">
          <template #filter="{ filterModel, filterCallback }">
            <InputText
              type="text"
              :placeholder="Departement"
              v-if="filterModel"
              v-model="filterModel.value"
              @keydown.enter="filterCallback()"
              class="p-column-filter p-inputtext-sm"
            />
          </template>
        </Column>
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
    onAuth(event) {
      this.canCreate = event.canCreate;
      this.canEdit = event.canEdit;
      this.canDelete = event.canDelete;
      this.canView = event.canView;
    },
    onPage(event) {
      this.start = event.first;
      this.loadLazyData();
    },
    onSort(event) {
      this.sorts = event.multiSortMeta;
      this.loadLazyData();
    },
    onFilter() {
      this.loadLazyData();
    },
    onSelect(event) {
      this.selectedRow = event.data;
    },
    onRowExpand(event) {
      this.expandedRows = [];
      this.expandedRows.push(event.data);
    },
    async loadLazyData() {
      this.loading = true;
      this.selectedRow = null;

      this.lazyParams = {
        start: this.start,
        sorts: this.dataSort,
        filters: this.dataFilter,
      };

      var self = this;

      await this.$axios
        .post(this.$API_URL + this.dataUrl, this.lazyParams)
        .then((response) => {
          console.log(response);
          if (response.data.success) {
            this.employeeData = response.data.data;
            this.totalRecords = response.data.totalRecords;
            console.log(this.employeeData);
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
    filter(mode) {
      this.$router.push({ path: this.filterPath, query: { mode: mode } });
    },
    setFilters() {
      this.filters = {};
      var exportable = this.$refs.dt.columns.filter((c) => c.props.field);
      for (var i = 0; i < exportable.length; i++) {
        var filterValue = null;
        if (this.stateFilters) {
          var field = this.stateFilters[exportable[i].props.field];
          if (field) filterValue = field.value;
        }
        this.filters[exportable[i].props.field] = {
          value: filterValue,
          matchMode: "contains",
        };
      }
    },
    onStateRestore(event) {
      this.start = event.first;
      this.stateFilters = event.filters;
      this.sorts = event.multiSortMeta;
    },
    editOrder() {
      this.$router.push({
        path: this.editorPath,
        query: { mode: this.$FORM_MODE_EDIT, id: this.selectedRow[0].Id },
      });
    },
    onModalClose() {
      this.loadLazyData();
    },
    clearFilter() {
      this.filters = {};
      var exportable = this.$refs.dt.columns.filter((c) => c.props.field);
      for (var i = 0; i < exportable.length; i++) {
        var fieldName = exportable[i].props.field;
        var filterValue = "";
        if (fieldName.includes("Date")) {
          filterValue = null;
        }
        this.filters[exportable[i].props.field] = {
          value: filterValue,
          matchMode: "contains",
        };
      }
      this.loadLazyData();
    },
  },
  mounted() {
    this.$emit("unblock-ui");
    this.setFilters();
    this.loadLazyData();
    this.$showToast();
  },
  computed: {
    dataFilter() {
      var h = [];
      if (this.filters) {
        for (const [key, value] of Object.entries(this.filters)) {
          h.push({
            field: key ? key : null,
            value: value.value,
            matchMode: value.matchMode,
          });
        }
      }
      return h;
    },
    dataSort() {
      var h = [];
      if (this.sorts) {
        for (var x = 0; x < this.sorts.length; x++) {
          h.push({
            field: this.sorts[x].field,
            order: this.sorts[x].order == 1 ? "ASC" : "DESC",
          });
        }
      }
      return h;
    },
    filterBadge() {
      var badgeNumber = 0;

      this.dataFilter.filter(function (item) {
        if (item.value && item.field != "Status") {
          badgeNumber++;
        }
      });

      return badgeNumber;
    },
  },
};
</script>

<style #scoped>
.p-card .p-card-content {
  margin: 0 !important;
  padding: 0 !important;
}
</style>
