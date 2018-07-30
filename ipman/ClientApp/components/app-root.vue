<template>
    <v-app id="inspire" light>
        <v-toolbar color="primary"
                   app
                   scroll-off-screen
                   >
            <v-toolbar-side-icon @click.native="drawerOpen = !drawerOpen"></v-toolbar-side-icon>
            <v-btn flat color="primary" to="/">
                <v-toolbar-title class="black--text">
                    Codename
                </v-toolbar-title>
            </v-btn>
            <v-spacer></v-spacer>
            <v-spacer></v-spacer>
            <v-spacer></v-spacer>
            <v-spacer></v-spacer>
            <v-spacer></v-spacer>
            <v-btn icon to="/dashboard" v-if="Username">
                <v-icon>cloud</v-icon>
            </v-btn>
            <v-btn flat id="usernameTitle" tag="div" v-if="Username">
                    {{Username}}
            </v-btn>
            <v-btn v-else icon color="primary" href="/login-google">
                <img height="25" elevation-24 src="../assets/Google.png">
            </v-btn>
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
            <v-card-actions class="secondary darken-3 justify-center white--text">
                &copy;2018 — <strong>Codename</strong>
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
    public get Username()
    {
        return LoginStore.getters.user ? LoginStore.getters.user.Username : "";
    }
    // Get the user name cookie.
    public checkUser(){
        let token = Cookies.get(accessTokenId);
        let tempEmail = Cookies.get(tempEmailId)
        console.log("Checking user: ",token, tempEmail);
        if(token && !LoginStore.getters.isLoggedIn)
        {
            Cookies.remove(accessTokenId);
            Cookies.remove(tempEmailId);
            LoginStore.actions.initializeUserContext({email: tempEmail, refresh_token: token});
        }
        else
            LoginStore.actions.validateUserContext();
    }

    data() {
        return {
            drawerOpen: false
        }
    }
    public mounted()
    {

    }
    public created()
    {
        this.checkUser();
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