<template>
    <v-layout>
        <v-flex xs12>
            <v-card class="secondary lighten-2">
                <v-card-title primary-title class="secondary">
                    <div class="headline white--text">
                        Welcome, here are your available places
                    </div>
                </v-card-title>
                <v-container grid-list-xs>
                    <v-layout row wrap v-if="siteAccounts.length">
                        <v-flex elevation-10 xs12 sm5 md3 lg2 pa-2 v-for="site in siteAccounts" :key="site.ID">
                            <v-card flat tile color="primary lighten-2" class="white--text">
                                <v-card-title primary-title>
                                    <div class="headline black--text">{{site.SiteAccountName}}</div>
                                </v-card-title>
                                <v-card-actions>
                                    <router-link :to="'/sites/' + site.SiteAccountName">
                                        <v-btn flat>Enter now</v-btn>
                                    </router-link>
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
import { Component } from 'vue-property-decorator';
import SiteAccount from '../entity/SiteAccount';
import {SiteAccountStore} from '@store';
@Component({

})

export default class DashboardPage extends Vue
{
    
    public get siteAccounts (){ return SiteAccountStore.getters.siteAccountList};
    
    public data(): any
    {
        return { msg: '' };
    }

    public async created()
    {

    }

    public async mounted()
    {
        SiteAccountStore.actions.fetchUserSiteAccounts();
        this.$vuetify.theme.secondary = '#43a047';
        this.$vuetify.theme.primary = '#795548';
    }
}
</script>

<style scoped>

</style>
