<template>
    <v-layout>
        <v-flex xs12>
            <v-card class="secondary lighten-2">
                <v-card-title primary-title class="secondary">
                    <div class="headline white--text">
                        {{SiteName}}
                    </div>
                </v-card-title>
                <v-flex v-if="isPiCamSite">
                    <v-btn block @click="requestSingleImageCapture">
                        Capture Image
                    </v-btn>
                </v-flex>
                <v-container grid-list-xs>
                    <v-layout row wrap v-if="postList.length">
                        <v-flex xs12 sm6 md4 lg3 mt-2 mb-2 pr-2 pl-2 v-for="post in postList" :key="post.ID">
                            <post-card :post="post"/>
                        </v-flex>
                    </v-layout>
                    <v-layout row wrap v-else>
                        <v-flex>
                            <v-card flat tile>
                                <v-card-title primary-title>
                                    <div class="headline black--text">Loading...</div>
                                </v-card-title>
                            </v-card>
                        </v-flex>
                    </v-layout>
                </v-container>
            </v-card>
        </v-flex>
    </v-layout>
</template>

<script lang="ts">

// THIS IS JUST A COMPONENT TEMPLATE I USE FOR QUICK
// COPY-PASTE-CUSTOMIZE Components

import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import PostCard from './cards/post-card.vue';
import Post from '../entity/Post';
import {SiteAccountType} from '../entity/lookups/SiteAccountType'
import { PostStore, SiteAccountStore, PiCamStore } from '@store'; 


@Component({
    components: {
        PostCard
    }
})

export default class SitePage extends Vue
{
    private connection;

    public SiteName = "";
    public get postList() { return PostStore.getters.postList; }
    public get activeSite() { return SiteAccountStore.getters.activeSiteAccount; }
    public get isPiCamSite() { return SiteAccountStore.getters.activeSiteAccount.SiteAccountType == SiteAccountType.RaspberryPi }
    public async requestSingleImageCapture()
    {
        console.log("Requesting Image.");
        await PiCamStore.actions.requestSingleImageCapture();
        console.log("Should be finished.");
    }
    public data(): any
    {
        return { msg: '' };
    }
    
    public created()
    {

    }
    public async mounted()
    {
        let self = this; 
        self.SiteName = self.$route.params.site 
        PostStore.actions.fetchPostList(self.activeSite.ID);
        self.$vuetify.theme.primary = self.activeSite.SiteAccountThemeColorPrimary;
        self.$vuetify.theme.secondary = self.activeSite.SiteAccountThemeColorSecondary;
    }
}
</script>

<style scoped>

</style>
