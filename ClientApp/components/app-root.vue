<template>
    <v-app id="inspire" light>
            <nav-menu :isOpen="drawerOpen"></nav-menu>
            <v-toolbar color="amber" app absolute clipped-left>
                <v-toolbar-side-icon @click.native="drawerOpen = !drawerOpen"></v-toolbar-side-icon>
                <span class="title ml-3 mr-5">Ip&nbsp;<span class="text">Man</span></span>
                <v-text-field
                    solo-inverted
                    flat
                    label="Search"
                    prepend-icon="search"
                ></v-text-field>
                <v-spacer></v-spacer>
            </v-toolbar>
            <main>
                <v-content >
                    <v-container fluid fill-height class="grey lighten-4">
                        <v-layout justify-center align-center>
                            <router-view></router-view>
                        </v-layout>
                    </v-container>
                </v-content>
            </main>
    </v-app>
</template>

<script>
import Vue from 'vue'
import CounterExample from './counter-example'
import FetchData from './fetch-data'
import HomePage from './home-page'
import NavMenu from './nav-menu'

Vue.component('counter-example', CounterExample);
Vue.component('fetch-data', FetchData);
Vue.component('home-page', HomePage);
Vue.component('nav-menu', NavMenu);

export default {
    data() {
        return {
            drawerOpen: false
        }
    }
}
</script>

<style>
</style>