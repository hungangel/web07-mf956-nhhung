import Vue from 'vue'
import App from './App.vue'
import axios from 'axios'
import VueAxios from 'vue-axios'
import router from './api/router/routerconfig'
import DatePicker from "vue2-datepicker";
import "vue2-datepicker/index.css";

Vue.component('datepicker', DatePicker);
Vue.use(VueAxios, axios)
Vue.config.productionTip = false

export const eventBus = new Vue();

new Vue({
    router,
    render: h => h(App),
}).$mount('#app')