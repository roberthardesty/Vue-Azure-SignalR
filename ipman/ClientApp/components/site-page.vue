<template>
    <v-layout>
        <v-flex xs12>
            <v-card class="secondary lighten-2">
                <v-card-title primary-title class="secondary">
                    <div class="headline white--text">
                        {{SiteName}}
                    </div>
                </v-card-title>
                <v-container grid-list-xs>
                    <v-layout row wrap v-if="postList.length">
                        <v-flex xs12 sm6 md4 lg3 mt-2 mb-2 pr-2 pl-2 v-for="post in postList" :key="post.ID">
                            <!-- <v-card flat tile color="primary lighten-2" class="white--text">
                                <v-card-title primary-title>
                                    <div class="headline black--text">{{ post.PostTitle }}</div>
                                </v-card-title>
                                <v-card-actions>
                                        <v-btn flat>Enter now</v-btn>
                                </v-card-actions>
                            </v-card> -->
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
import { PostStore, SiteAccountStore } from '@store'; 


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
