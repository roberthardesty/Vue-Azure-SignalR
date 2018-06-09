import 'vuetify/dist/vuetify.min.css';
import Vue from 'vue';
import axios from 'axios';
import router from './router';
import store from './store';
import { sync } from 'vuex-router-sync';
import App from 'components/app-root.vue';
import Vuetify from 'vuetify';
import 'bootstrap';
import jquery from 'jquery';
window.$ = jquery(window).jQuery = jquery;
var signalR = require('./signalr.min.js');
Vue.prototype.$http = axios;
Vue.prototype.$signalR = signalR;
Vue.use(Vuetify);
sync(store, router);
const app = new Vue({
    el: '#app',
    store,
    router,
    render: (h) => h(App)
});
export { app, router, store };
//# sourceMappingURL=boot.js.map