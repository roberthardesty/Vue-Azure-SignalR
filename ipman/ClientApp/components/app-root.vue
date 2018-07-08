<template>
    <v-app id="inspire" light>
        <v-toolbar color="primary"
                   app 
                   scroll-off-screen
                   >
            <v-toolbar-side-icon @click.native="drawerOpen = !drawerOpen"></v-toolbar-side-icon>
            <router-link to="/" tag="a">
                <v-toolbar-title class="black--text">
                    eGAMEbler
                </v-toolbar-title>
            </router-link>
            <v-spacer></v-spacer>
            <router-link to="/dashboard" tag="div">
                <v-btn icon>
                    <v-icon color="black">cloud</v-icon>
                </v-btn>
            </router-link>
                <v-btn icon @click="test()">
                    <v-icon color="black">person</v-icon>
                </v-btn>
            <v-subheader id="usernameTitle" v-if="username">
                    {{username}}
            </v-subheader>
            <span v-else>
                <a href="/login-google">
                    <v-btn icon>
                        <img height="25" elevation-24 src="../assets/Google.png">
                    </v-btn>
                </a>
                <a href="/login-github">
                    <v-btn icon>
                        <img height="25" elevation-24 src="../assets/Github.svg">
                    </v-btn>
                </a>
            </span>
        <username-form />
        </v-toolbar>
        <nav-menu
            app 
            :isOpen="drawerOpen">
        </nav-menu>

        <v-content>
            <v-container fluid fill-height class="grey lighten-3">
                <router-view></router-view>
            </v-container>
        </v-content>
        
        <v-footer height="auto">
            <v-card flat tile class="flex">
            <v-card-title class="grey">
                <strong class="subheading">eSports Interactive Experience</strong>
            </v-card-title>
            <!-- <v-card-text class="grey lighten-3">
                <v-layout>
                    <v-flex xs6 layout column>
                        
                    </v-flex>
                </v-layout>
            </v-card-text> -->
            <v-card-actions class="secondary darken-3 justify-center white--text">
                &copy;2018 — <strong>eGAMEbler</strong>
            </v-card-actions>
            </v-card>
        </v-footer>
    </v-app>
</template>

<script lang="ts">
import Vue from 'vue';
import Vuex, { Store } from 'vuex';
import * as Cookies from 'es-cookie';
import { sync } from 'vuex-router-sync'
import { RootState, storeBuilder, LoginStore, EventBus} from "@store";
import { Component } from 'vue-property-decorator';
import NavMenu from './nav-menu.vue'
import UsernameForm from './forms/username-form.vue'
import router from "../router";

const store: Store<RootState> = storeBuilder.vuexStore({
    strict: false
});

sync(store, router);

  
const accessTokenId = 'access_token_ggl';
const tempEmailId = 'temp_email';
@Component({
    store: store,
    components:{
        NavMenu,
        UsernameForm
    },
    router
})
export default class App extends Vue
{
    // Get the user name cookie.
    public username = Cookies.get('github_username') || "";    
    public checkUser(){
        let token = Cookies.get(accessTokenId);
        let tempEmail = Cookies.get(tempEmailId)
        if(token && !LoginStore.getters.isLoggedIn)
        {
            Cookies.remove(accessTokenId);
            Cookies.remove(tempEmail);
            LoginStore.actions.initializeUserContext({email: tempEmail, refresh_token: token});
        }
    }

    data() {
        return {
            drawerOpen: false
        }
    }
    public mounted()
    {

    }

    public test()
    {
        EventBus.$emit("username_popup_open");
    }
}
</script>

<style>
#usernameTitle {
    font-size: 12pt;
}
</style>