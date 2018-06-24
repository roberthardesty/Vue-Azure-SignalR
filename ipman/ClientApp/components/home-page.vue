<template>
    <v-layout column>
        <v-card color="secondary lighten-3">
            <h2  style="text-align:center">Let's make a bet</h2>
        </v-card>
        <!-- row -->
        <!-- <v-layout align-center row justify-center>
            <v-flex elevation-24 xs2 class="round-flex">
                <img elevation-24 src="../assets/super-smash-bros.png" class="responsive">
            </v-flex>
        </v-layout> -->
        
        <!-- row -->
        <v-layout row wrap align-center justify-space-between>
            <v-flex xs2>
                <router-link to="/login-google" tag="a" >
                    <v-btn style="width: 120px; height: 120px" fab ripple color="white">
                        <v-container>
                        <v-layout row wrap>
                            <v-flex xs12 ma-3>
                                <img elevation-24 src="../assets/Google.png" class="responsive">
                            </v-flex>
                            <v-flex class="mt-3" xs12>
                            <v-subheader>Google</v-subheader>
                            </v-flex>
                        </v-layout>
                        </v-container>
                    </v-btn>
                </router-link>
            </v-flex>
            <v-flex xs2>
                <router-link to="/github-login" tag="a" >
                    <v-btn style="width: 120px; height: 120px" fab color="white">
                        <v-container>
                        <v-layout row wrap>
                            <v-flex xs12 ma-3>
                                <img elevation-24 src="../assets/Github.svg" class="responsive">
                            </v-flex>
                            <v-flex class="mt-3" xs12>
                            <v-subheader>Github</v-subheader>
                            </v-flex>
                        </v-layout>
                        </v-container>
                    </v-btn>
                </router-link>
            </v-flex>
        </v-layout>
        <!-- row -->
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
