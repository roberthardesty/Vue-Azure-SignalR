import Vue from 'vue'
import axios from 'axios'
import router from './router'
import store from './store'
import { sync } from 'vuex-router-sync'
import App from 'components/app-root.vue'

import 'bootstrap'

import jquery from 'jquery'
(<any>window).$ = jquery
(<any>window).jQuery = jquery

var signalR = require('./signalr.min.js');

Vue.prototype.$http = axios;
Vue.prototype.$signalR = signalR;

sync(store, router);

const app = new Vue({
    el: '#app',
    store,
    router,
    render: (h:any)=> h(App)
});

export {
    app,
    router,
    store
}
