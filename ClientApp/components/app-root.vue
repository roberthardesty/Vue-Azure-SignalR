<template>
    <v-app id="inspire" light>
            <v-toolbar color="amber" app absolute clipped-left>
                <v-toolbar-side-icon @click.native="drawerOpen = !drawerOpen"></v-toolbar-side-icon>
                <router-link to="/" tag="a">
                    <v-toolbar-title class="black--text">
                        Ip Man
                    </v-toolbar-title>
                </router-link>
                <v-spacer></v-spacer>
                <router-link to="/counter" tag="div" >
                    <v-btn icon>
                        <v-icon color="white">add_circle_outline</v-icon>
                    </v-btn>
                </router-link>
                <router-link to="/fetch-data" tag="div">
                    <v-btn icon>
                        <v-icon color="black">cloud</v-icon>
                    </v-btn>
                </router-link>
                <a href="/login">
                    <v-btn icon>
                        <v-icon color="gray">person</v-icon>
                    </v-btn>
                </a>
            </v-toolbar>
            <nav-menu 
                :isOpen="drawerOpen">
            </nav-menu>
            <main>
                <v-content fluid>
                    <v-container fluid fill-height class="grey lighten-4">
                        <v-layout fill-height>
                            <router-view></router-view>
                        </v-layout>
                    </v-container>
                </v-content>
            </main>
    </v-app>
</template>

<script lang="ts">
import Vue from 'vue'
import { Component } from 'vue-property-decorator';
import CounterExample from './counter-example.vue'
import FetchData from './fetch-data.vue'
import HomePage from './home-page.vue'
import NavMenu from './nav-menu.vue'

// Vue.component('counter-example', CounterExample);
// Vue.component('fetch-data', FetchData);
// Vue.component('home-page', HomePage);
// Vue.component('nav-menu', NavMenu);

@Component({
    components:{
        NavMenu,
        CounterExample,
        FetchData,
        HomePage
    }
})
export default class App extends Vue
{
    data() {
        return {
            drawerOpen: false
        }
    }
}
</script>

<style>
</style>