import 'vuetify/dist/vuetify.min.css'
import Vue from 'vue'
import axios from 'axios'
import router from './router'
import store from './store'
import { sync } from 'vuex-router-sync'
import App from 'components/app-root.vue'
import Vuetify from 'vuetify'

import 'bootstrap'

import jquery from 'jquery'
(<any>window).$ = jquery
(<any>window).jQuery = jquery

import * as signalR from "@aspnet/signalr";
//var signalR = require('./signalr.min.js');

Vue.prototype.$http = axios;
Vue.prototype.$signalR = signalR;

declare module 'vue/types/vue' 
{
    // 3. Declare augmentation for Vue
    interface Vue 
    {
      $signalR: any
    }
}

Vue.use(Vuetify, {
    theme: {
      primary: '#43a047', // #E53935
      secondary: '#795548', // #FFCDD2
      accent: '#43a047' // #3F51B5
    }
});

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
