import 'vuetify/dist/vuetify.min.css'
import Vue from 'vue'
import App from 'components/app-root.vue'
import Vuetify from 'vuetify'

import 'bootstrap'

import jquery from 'jquery'

(<any>window).$ = jquery
(<any>window).jQuery = jquery

Vue.use(Vuetify, {
    theme: {
      primary: '#43a047', // #E53935
      secondary: '#795548', // #FFCDD2
      accent: '#43a047' // #3F51B5
    }
});


new App().$mount('#app');
