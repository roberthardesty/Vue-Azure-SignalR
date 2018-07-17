<template>
    <v-layout column>
            <v-card color="secondary lighten-3">
            </v-card>

            <v-flex mb-2 xs12>
                <v-card color="blue-grey darken-2" class="white--text">
                <v-card-title primary-title>
                    <div class="headline">Bobby Newport</div>
                    <div>Has never had a real job... in his life.</div>
                </v-card-title>
                <v-card-actions>
                    <v-btn flat dark>Vote now</v-btn>
                </v-card-actions>
                </v-card>
            </v-flex>
            <v-card color="secondary lighten-3">
                <v-parallax :src="require('@assets/forest.jpg')" >
                    <h2  style="text-align:center">Let's make a bet</h2>
                    <v-layout row wrap align-center justify-space-between>
                        <v-flex xs4>
                            <router-link to="/login-google" tag="a" >
                                <v-btn style="width: 120px; height: 120px" fab ripple color="white">
                                   <v-flex ma-2>
                                        <img elevation-24 src="../assets/Google.png" class="responsive">
                                   </v-flex>
                                </v-btn>
                            </router-link>
                        </v-flex>
                        <v-flex xs4>
                            <router-link to="/github-login" tag="a" >
                                <v-btn style="width: 120px; height: 120px" fab color="white">
                                    <v-flex xs12 ma-2>
                                        <img elevation-24 src="../assets/Github.svg" class="responsive">
                                    </v-flex>
                                </v-btn>
                            </router-link>
                        </v-flex>
                    </v-layout>
                </v-parallax>
            </v-card>

    </v-layout>
</template>

<script>
export default {
    data() {
        return {
        }
    },
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

.round-flex {
    border-radius: 50%;
}
</style>
