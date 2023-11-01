
export default [
    {
        path: "/Approval/Index",
        name: "ApprovalIndex",
        component: () => import("@/components/Approval/Index.vue"),
        meta: {
            name: "ApprovalIndex",
            menuTitle: "Approval",
            requiresAuth: true
        },
        props: true
    },
    {
        path: "/Example",
        name: "Example",
        component: () => import("@/components/Example/Index.vue"),
        meta:{
            name: "Example",
            menuTitle : "Example",
            requiresAuth: true
        },
        props: true
    },
    {
        path: "/Example",
        name: "Example",
        component: () => import("@/components/Example/Editor.vue"),
        meta:{
            name: "Example",
            menuTitle : "Example",
            requiresAuth: true
        },
        props: true
    },
    
]