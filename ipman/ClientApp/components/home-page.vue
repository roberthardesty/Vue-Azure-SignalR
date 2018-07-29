<template>
    <v-layout column>
            <v-flex mb-2 xs12>
                <v-flex mb-5 xs12 md6 offset-md3>
                    <v-card height="900" xs12 md6 color="secondary lighten-3">
                        <v-parallax  height="1000" class="home-page-image responsive" :src="require('@assets/forest.jpg')">
                            <v-flex>
                            </v-flex>
                            <!-- :src="require('@assets/forest.jpg')" > -->
                            <h2  style="text-align:center">Welcome</h2>
                            <v-flex>
                            </v-flex>
                            <v-divider></v-divider>
                        </v-parallax>
                    </v-card>
                </v-flex>
                <v-flex mb-2 xs12 md12>
                    <v-card xs12 md6 color="secondary lighten-3">
                        <v-card-media :src="require('@assets/forest.jpg')" ></v-card-media>
                        
                    </v-card>
                </v-flex>
            </v-flex>
    </v-layout>
</template>

<script lang="ts">
import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import { PiCamStore } from '@store'

@Component({

})
export default class HomePage extends Vue {
    
    public async requestSingleImageCapture()
    {
        console.log("Requesting Image.");
        await PiCamStore.actions.requestSingleImageCapture();
        console.log("Should be finished.");

    }
    
    data() {
        return {
        }
    };

    mounted(){
        this.$vuetify.theme.primary = '#43a047';
        this.$vuetify.theme.secondary = '#795548';
    }
}
</script>

<style>
.responsive {
    width: 100%;
    height: auto;
}

.home-page-image {
    
}

.login-button {
    text-decoration: none;
    margin-left: -15px;
}

.round-flex {
    border-radius: 50%;
}
</style>
