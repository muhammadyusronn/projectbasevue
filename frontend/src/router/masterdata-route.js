export default [
  {
    path: "/Company/Index",
    name: "CompanyIndex",
    component: () => import("@/components/Company/Index.vue"),
    meta: {
      name: "CompanyIndex",
      menuTitle: "Company",
      requiresAuth: true,
    },
    props: true,
  },
  {
    path: "/Company/Editor",
    name: "CompanyEditor",
    component: () => import("@/components/Company/Editor.vue"),
    meta: {
      name: "CompanyEditor",
      menuTitle: "Company",
      requiresAuth: true,
    },
    props: true,
  },
  {
    path: "/Location/Index",
    name: "LocationIndex",
    component: () => import("@/components/Location/Index.vue"),
    meta: {
      name: "LocationIndex",
      menuTitle: "Location",
      requiresAuth: true,
    },
    props: true,
  },
  {
    path: "/Location/Editor",
    name: "LocationEditor",
    component: () => import("@/components/Location/Editor.vue"),
    meta: {
      name: "LocationEditor",
      menuTitle: "Location",
      requiresAuth: true,
    },
    props: true,
  },
  {
    path: "/Department/Index",
    name: "DepartmentIndex",
    component: () => import("@/components/Department/Index.vue"),
    meta: {
      name: "DepartmentIndex",
      menuTitle: "Department",
      requiresAuth: true,
    },
    props: true,
  },
  {
    path: "/Department/Editor",
    name: "DepartmentEditor",
    component: () => import("@/components/Department/Editor.vue"),
    meta: {
      name: "DepartmentEditor",
      menuTitle: "Department",
      requiresAuth: true,
    },
    props: true,
  },
];
