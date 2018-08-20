<template>
    <v-layout>
        <v-flex xs12 v-if="false">
            <v-card class="secondary lighten-2">
                <v-card-title primary-title class="secondary">
                    <div class="headline white--text">
                        Welcome, here are your available places
                    </div>
                </v-card-title>

                <v-container grid-list-xs>
                    <v-slide-x-transition>
                        <v-layout row wrap>
                            <v-flex xs12 sm5 md3 lg2 pa-2>
                                <!-- <site-card 
                                    :site='site'
                                    :userID ='currentUserID()'
                                >
                                </site-card> -->
                            </v-flex>
                        </v-layout>
                    </v-slide-x-transition>
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


@Component({
    components: {
    }
})

export default class FaceCollectionSelection extends Vue
{

    public async created()
    {

    }

    public async mounted()
    {

    }
}
</script>

<style scoped>

</style>
