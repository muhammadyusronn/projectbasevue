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
  {
    path: "/Schedule/Index",
    name: "ScheduleIndex",
    component: () => import("@/components/Transaction/Schedule/Index.vue"),
    meta: {
      name: "ScheduleIndex",
      menuTitle: "Schedule",
      requiresAuth: true,
    },
    props: true,
  },
  {
    path: "/Schedule/Editor",
    name: "ScheduleEditor",
    component: () => import("@/components/Transaction/Schedule/Editor.vue"),
    meta: {
      name: "ScheduleEditor",
      menuTitle: "Schedule",
      requiresAuth: true,
    },
    props: true,
  },
];
