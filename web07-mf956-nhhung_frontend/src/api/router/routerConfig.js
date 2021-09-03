import Vue from "vue";
import VueRouter from "vue-router";
import EmployeeContent from '../../view/employee/EmployeeContent.vue'

import Customer from '../../view/employee/AddEmployeeForm.vue'
Vue.use(VueRouter);

const routes = [
    { path: '/', name: 'Home', component: Customer },
    { path: '/dict/employee', component: EmployeeContent },
    { path: '/dict/customer', component: Customer },
]
const router = new VueRouter({
    mode: 'history',
    routes
})

export default router;