<template>
    <v-layout>
        <v-flex xs12>
            <v-card class="secondary lighten-2">
                <v-card-title primary-title class="secondary">
                    <div class="headline white--text">
                        Welcome, here are your available places
                    </div>
                </v-card-title>

                <v-tabs
                    centered
                    color="secondary"
                    v-model="selectedTab"
                    dark
                    grow
                    icons-and-text
                >
                    <v-tabs-slider color="secondary darken-3"></v-tabs-slider>

                    <v-tab href="#tab-1">
                        <v-icon>favorite</v-icon>
                    </v-tab>

                    <v-tab href="#tab-2" @click="searchTab()">
                        <v-icon>search</v-icon>
                    </v-tab>

                    <v-tab-item
                    id="tab-1"
                    >
                    </v-tab-item>

                    <v-tab-item
                    id='tab-2'
                    lazy
                    >
                        <v-flex xs12 sm6>
                            <v-text-field
                                color="primary lighten-3"
                                label="Search..."
                                single-line
                                box
                                clearable
                            ></v-text-field>
                        </v-flex>
                    </v-tab-item>
                </v-tabs>
                <v-container grid-list-xs>
                    <v-slide-x-transition>
                        <v-layout row wrap v-show="siteAccounts.length">
                            <v-flex xs12 sm5 md3 lg2 pa-2 v-for="site in siteAccounts" :key="site.ID">
                                <site-card 
                                    :site='site'
                                    :userID ='currentUserID()'
                                >
                                </site-card>
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
import SiteCard from './cards/site-card.vue'
import SiteAccount from '../entity/SiteAccount';
import { SiteAccountStore, LoginStore } from '@store';
import { SiteAccountUserAccount } from '@entity';

const MY_SITES_MODE = 1;
const SEARCH_SITES_MODE = 2;

@Component({
    components: {
        SiteCard
    }
})

export default class DashboardPage extends Vue
{
    public selectedTab: string = "";

    public currentUserID() : string
    {
        return LoginStore.getters.user.ID
    }

    public get siteListMode()
    {
        return parseInt(this.selectedTab.replace("tab-", ""));
    }

    public get siteAccounts ()
    { 
        switch(this.siteListMode)
        {
            case MY_SITES_MODE:
                return SiteAccountStore.getters.userSiteAccountList;
            case SEARCH_SITES_MODE:
                return SiteAccountStore.getters.searchSiteAccountList;
            default:
                return [];
        }
    };

    public async search(keyword: string)
    {
        await SiteAccountStore.actions.search({
            Keyword: keyword,
            OtherUserSites: true,
            CurrentUserSites: false,
            UserEmail: '',
            IncludeSiteAccountUserAccounts: true,
            ExcludedSiteAccounts: [],
            PageNumber: 0,
            PageSize: 0,
            RecordsToSkip: 0,
            SortBy: []
        })
    }
    
    public async updateActiveSite(site: SiteAccount)
    {
        console.log("Updating Active Site To: ", site);
        await SiteAccountStore.actions.loadSiteAccount(site);
        this.$router.push('/sites/' + site.SiteAccountName);
    }
    
    public async searchTab()
    {
        console.log(SiteAccountStore.getters.searchSiteAccountList.length)
        if(SiteAccountStore.getters.searchSiteAccountList.length)
            return;

        await this.search('');
    }

    public async created()
    {

    }

    public async mounted()
    {
        SiteAccountStore.actions.fetchUserSiteAccounts();
        this.$vuetify.theme.primary = '#43a047';
        this.$vuetify.theme.secondary = '#795548';
    }
}
</script>

<style scoped>

</style>
