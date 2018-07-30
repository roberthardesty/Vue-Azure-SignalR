<template>
    <v-layout wrap>
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
        <v-flex>
         <v-toolbar
            v-if="isPiCamSite"
            dense
            floating
            id="piToolBar"
            >
            <v-text-field
                hide-details
                prepend-icon="search"
                single-line
            ></v-text-field>

            <v-btn icon>
                <v-icon>my_location</v-icon>
            </v-btn>

            <v-btn icon>
                <v-icon>more_vert</v-icon>
            </v-btn>
         </v-toolbar>
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
import { PostStore, SiteAccountStore, PiCamStore, EventBus } from '@store'; 


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

    public async deletePost(postID: string)
    {
        if(!this.activeSite) return;
        await PostStore.actions.deletePost({ postID: postID, siteID: this.activeSite.ID });
    }

    public async requestSingleImageCapture()
    {
        console.log("Requesting Image.");
        await PiCamStore.actions.requestSingleImageCapture();
        console.log("Should be finished.");
    }

    public registerEvents()
    {
        EventBus.$on(this.deletePost.name, this.deletePost);
    }

    public data(): any
    {
        return { msg: '' };
    }
    
    public created()
    {
        this.registerEvents();
    }
    public async mounted()
    {
        let self = this; 
        self.SiteName = self.$route.params.site
        if(!self.activeSite)
        {
            self.$router.push('/dashboard');
            return;
        }

        self.$vuetify.theme.primary = self.activeSite.SiteAccountThemeColorPrimary;
        self.$vuetify.theme.secondary = self.activeSite.SiteAccountThemeColorSecondary;
    }
}
</script>

<style scoped>

#piToolBar {
  z-index:5;  
  bottom: 0;
  position: fixed;
  -webkit-transition: all 1s ease;
  -moz-transition: all 1s ease;
  transition: all 1s ease;
  right: 30px;
}

</style>
