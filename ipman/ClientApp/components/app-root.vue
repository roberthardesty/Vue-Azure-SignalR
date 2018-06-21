<template>
    <v-app id="inspire" light>
        <v-toolbar color="primary" app absolute clipped-left>
            <v-toolbar-side-icon @click.native="drawerOpen = !drawerOpen"></v-toolbar-side-icon>
            <router-link to="/" tag="a">
                <v-toolbar-title class="black--text">
                    Ip Man
                </v-toolbar-title>
            </router-link>
            <v-spacer></v-spacer>
            <router-link to="/counter" tag="div" >
                <v-btn icon>
                    <v-icon color="black">add_circle_outline</v-icon>
                </v-btn>
            </router-link>
            <router-link to="/dashboard" tag="div">
                <v-btn icon>
                    <v-icon color="black">cloud</v-icon>
                </v-btn>
            </router-link>
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
        
        <v-footer app></v-footer>
    </v-app>
</template>

<script lang="ts">
import Vue from 'vue'
import { Component } from 'vue-property-decorator';
import NavMenu from './nav-menu.vue'

@Component({
    components:{
        NavMenu,
    }
})
export default class App extends Vue
{
    // Get the user name cookie.
    private getCookie(key) {
        var cookies = document.cookie.split(';').map(c => c.trim());
        for (var i = 0; i < cookies.length; i++) {
            if (cookies[i].startsWith(key + '=')) return unescape(cookies[i].slice(key.length + 1));
        }
        return '';
    }
    public username = this.getCookie('github_username');    
    data() {
        return {
            drawerOpen: false
        }
    }
    public mounted()
    {
        console.log(this.username);
    }
}
</script>

<style>
#usernameTitle {
    font-size: 12pt;
}
</style>