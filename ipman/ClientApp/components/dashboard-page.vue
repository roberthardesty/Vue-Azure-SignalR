<template>
<v-layout row wrap align-center>
        <v-flex xs12>
            <h2>
                welcome, here are your available places
            </h2>
        </v-flex>
        <v-flex xs12>
            <v-card color="blue-grey darken-2" class="white--text" v-for="site in siteAccounts" :key="site.ID">
                <v-card-title primary-title>
                    <div class="headline">{{site.SiteAccountName}}</div>
                </v-card-title>
                <v-card-actions>
                    <v-btn flat dark>Enter now</v-btn>
                </v-card-actions>
            </v-card>
        </v-flex>
</v-layout>
</template>

<script lang="ts">

// THIS IS JUST A COMPONENT TEMPLATE I USE FOR QUICK
// COPY-PASTE-CUSTOMIZE Components

import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import SiteAccountService from '../services/SiteAccountService'
import SiteAccount from '../entity/SiteAccount';
@Component({

})

export default class DashboardPage extends Vue
{
    private _siteAccountSerivice: SiteAccountService;
    
    public siteAccounts: SiteAccount[] = [];
    public data(): any
    {
        return { msg: '' };
    }

    public async created()
    {
        this._siteAccountSerivice = new SiteAccountService(this.$signalR);
        await this._siteAccountSerivice.Connect()
    }

    public async mounted()
    {
        this.siteAccounts = await this._siteAccountSerivice.GetUserSiteAccounts();
        console.log(this.siteAccounts);
    }
}
</script>

<style scoped>

</style>
