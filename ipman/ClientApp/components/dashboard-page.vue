<template>
<v-layout>
    <v-container fill-height >
        <h2>
            Title thing welcome
        </h2>
        <hr/>
        <v-layout align-center row wrap>
            <v-flex xs12>
            </v-flex>
        </v-layout>
    </v-container>
</v-layout>
</template>

<script lang="ts">

// THIS IS JUST A COMPONENT TEMPLATE I USE FOR QUICK
// COPY-PASTE-CUSTOMIZE Components

import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import SiteAccountService from '../services/SiteAccountService'
@Component({

})

export default class DashboardPage extends Vue
{
    private _siteAccountSerivice: SiteAccountService;
    private connection;
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
            this._siteAccountSerivice.GetUserSiteAccounts()
            .then(console.log)
            .catch(console.log)
    }
}
</script>

<style scoped>

</style>
