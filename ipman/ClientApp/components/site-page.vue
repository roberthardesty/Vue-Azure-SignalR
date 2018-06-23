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
                    <v-layout row wrap v-if="Posts.length">
                        <v-flex elevation-10 xs12 sm5 md3 lg2 ma-5 v-for="post in Posts" :key="post.ID">
                            <v-card flat tile color="primary lighten-2" class="white--text">
                                <v-card-title primary-title>
                                    <div class="headline black--text">{{ post.PostTitle }}</div>
                                </v-card-title>
                                <v-card-actions>
                                        <v-btn flat>Enter now</v-btn>
                                </v-card-actions>
                            </v-card>
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
import PostService from '../services/PostService';
import { Component } from 'vue-property-decorator';
import Post from '../entity/Post'


@Component({
})

export default class SitePage extends Vue
{
    private _postService: PostService;
    private connection;

    public SiteName = "";
    public Posts: Post[] = [];
    public data(): any
    {
        return { msg: '' };
    }
    
    public created()
    {
        this._postService = new PostService(this.$signalR);
    }

    public async mounted()
    {
        let self = this;
        self.SiteName = self.$route.params.site
        self._postService.GetPostsBySiteAccountName(this.$route.params.site)
        .then(response => 
        {
            self.Posts = response.data as Post[];
            console.log(self.Posts)
        })
        .catch(reason =>
        {
            console.log(reason);
        });
    }


}
</script>

<style scoped>

</style>
