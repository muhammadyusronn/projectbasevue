export default [
  {
    path: "/Example",
    name: "Example",
    component: () => import("@/components/Example/Index.vue"),
    meta: {
      name: "Example",
      menuTitle: "Example",
      requiresAuth: true,
    },
    props: true,
  },
  {
    path: "/Example/Editor",
    name: "ExampleEditor",
    component: () => import("@/components/Example/Editor.vue"),
    meta: {
      name: "ExampleEditor",
      menuTitle: "Example",
      requiresAuth: true,
    },
    props: true,
  },
  {
    path: "/Employee/Index",
    name: "EmployeeIndex",
    component: () => import("@/components/Employee/Index.vue"),
    meta: {
      name: "EmployeeIndex",
      menuTitle: "Employee",
      requiresAuth: true,
    },
    props: true,
  },
  {
    path: "/Employee/Editor",
    name: "EmployeeEditor",
    component: () => import("@/components/Employee/Editor.vue"),
    meta: {
      name: "EmployeeEditor",
      menuTitle: "Employee",
      requiresAuth: true,
    },
    props: true,
  },
];
