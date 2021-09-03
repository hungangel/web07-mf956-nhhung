import Vue from "vue";
import VueRouter from "vue-router";
import EmployeeContent from '../../view/employee/EmployeeContent.vue'
Vue.use(VueRouter);

const routes = [
    { path: '/', name: 'Home', component: EmployeeContent },
    { path: '/dict/employee', component: EmployeeContent },
]
const router = new VueRouter({
    mode: 'history',
    routes
})

export default router;