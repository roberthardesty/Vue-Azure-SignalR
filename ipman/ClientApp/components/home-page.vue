<template>
    <v-layout align-center row wrap>
        <v-flex xs2 offset-xs5>
            <img elevation-24 src="../assets/super-smash-bros.png" class="responsive">
        </v-flex>
        <v-flex xs5>
        </v-flex>

        <v-flex xs6>
            <router-link to="/login-google" tag="a" >
                <v-btn style="width: 120px; height: 120px" fab ripple outline color="black">
                    <v-container>
                    <v-layout row wrap>
                        <v-flex xs12>
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

        <v-flex xs6 >
        <router-link to="/github-login" tag="a" >
        <v-btn style="width: 120px; height: 120px" fab outline color="black">
            <v-container>
            <v-layout row wrap>
                <v-flex xs12 class="mb-2">
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

        <v-flex xs12 >
        <router-link to="/api/site/teams" tag="li" >
        <v-btn style="width: 110px; height: 110px" fab outline :color="buttonColor">
            <v-container>
            <v-layout row wrap>
                <v-flex class="mt-1" d-flex xs12>
                <v-icon dark x-large>people</v-icon>
                </v-flex>
                <v-flex class="mt-3" d-flex xs12>
                <v-subheader>Teams</v-subheader>
                </v-flex>
            </v-layout>
            </v-container>
        </v-btn>
        </router-link>
        </v-flex>
    </v-layout>
</template>

<script>
export default {
    data() {
        return {
        }
    },
}
</script>

<style>
.responsive {
    width: 100%;
    height: auto;
}
</style>
