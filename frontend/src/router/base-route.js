export default [
    {
        path: "/Login",
        name: "Login",
        component: () => import("@/components/Base/Auth/Login.vue"),
        meta: {
            name: "Login",
            menuTitle: "Login",
            requiresAuth: false
        },
        props: true
    },
    {
        path: "/Logout",
        name: "Logout",
        component: () => import("@/components/Base/Auth/Logout.vue"),
        meta: {
            name: "Logout",
            requiresAuth: false
        },
        props: true
    },
    {
        path: "/User/Index",
        name: "UserIndex",
        component: () => import("@/components/Base/User/Index.vue"),
        meta: {
            name: "UserIndex",
            menuTitle: "User",
            requiresAuth: true
        },
        props: true
    },
    {
        path: "/User/Editor",
        name: "UserEditor",
        component:  () => import("@/components/Base/User/Editor.vue"),
        meta: {
            name: "UserEditor",
            menuTitle: "User",
            requiresAuth: true
        },
        props: true
    },
    {
        path: "/Group/Index",
        name: "GroupIndex",
        component: () => import("@/components/Base/Group/Index.vue"),
        meta: {
            name: "GroupIndex",
            menuTitle: "Group",
            requiresAuth: true
        },
        props: true
    },
    {
        path: "/Group/Editor",
        name: "GroupEditor",
        component:  () => import("@/components/Base/Group/Editor.vue"),
        meta: {
            name: "GroupEditor",
            menuTitle: "Group",
            requiresAuth: true
        },
        props: true
    },

]