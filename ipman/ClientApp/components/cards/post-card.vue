<template>
    <v-card hover color="primary lighten-2" class="white--text">
        <v-card-media v-if="post.PostImageUri"
          :src="post.PostImageUri"
          height="200px"
        ></v-card-media>
        <v-container fluid grid-list-sm>
            <v-layout row wrap>
                <!-- Row 1 -->
                <v-flex xs12>
                    <v-layout row wrap>
                        <v-flex xs10 sm9>
                            <v-card flat tile color="primary lighten-3">
                                <v-card-title primary-title>
                                    <div class="headline black--text">{{ post.PostTitle }}</div>
                                </v-card-title>
                            </v-card>
                        </v-flex>
                        <v-flex xs2 sm3>
                            <v-card flat tile color="primary" width="60">
                                <div class="text-xs-center">
                                    <v-chip small color="grey" text-color="white">
                                        <v-icon>label</v-icon>
                                    </v-chip>
                                    <v-chip small color="grey" text-color="white">
                                        <v-icon>person</v-icon>
                                    </v-chip>
                                    <v-chip small color="grey" text-color="white">
                                        <v-icon>home</v-icon>
                                    </v-chip>
                                </div>
                            </v-card>
                        </v-flex>
                    </v-layout>
                </v-flex>
                <!-- Row 2 -->
                <v-flex xs12>
                    <v-card-actions>
                        <v-layout row wrap>
                            <v-btn icon @click.native="showDescription = !showDescription">
                                <v-icon>{{ showDescription ? 'keyboard_arrow_down' : 'keyboard_arrow_up' }}</v-icon>
                            </v-btn>
                            <v-spacer></v-spacer>
                            <v-btn icon @click="deletePost(post.ID)">
                                <v-icon>
                                    delete
                                </v-icon>
                            </v-btn>
                            <v-btn flat>{{post.UserAccountCreator.Username}}</v-btn>
                            <v-slide-y-transition>
                                <v-card-text v-show="showDescription">
                                    {{post.PostDescription}}
                                </v-card-text>
                            </v-slide-y-transition>
                        </v-layout>
                    </v-card-actions>
                </v-flex>
            </v-layout>
        </v-container>
    </v-card>
</template>

<script lang="ts">

// THIS IS JUST A COMPONENT TEMPLATE I USE FOR QUICK
// COPY-PASTE-CUSTOMIZE Components

import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import { EventBus } from '@store';


@Component({
    props: {
        post: Object
    }
})

export default class PostCard extends Vue
{
    private connection;
    private showDescription: boolean = false;

    private deletePost(postID: string)
    {
        EventBus.$emit(this.deletePost.name, postID);
    }

    public data(): any
    {
        return { msg: '' };
    }

}
</script>

<style scoped>

</style>
