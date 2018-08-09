<template> 
    <v-card :style="style_color(site.SiteAccountThemeColorPrimary)" >
        <v-card-title primary-title>
            <div class="headline black--text">{{site.SiteAccountName}}</div>
        </v-card-title>
        <v-card-actions>
                <v-btn @click="siteCardAction()" 
                    tag="div"
                    :style="style_color(site.SiteAccountThemeColorSecondary)">
                    {{userStatus}}</v-btn>
        </v-card-actions>
    </v-card>
</template> 
 
<script lang="ts"> 
 
// THIS IS JUST A COMPONENT TEMPLATE I USE FOR QUICK 
// COPY-PASTE-CUSTOMIZE Components 
 
import Vue from 'vue'; 
import { Component } from 'vue-property-decorator'; 
import { SiteAccount } from '@entity';
import { SiteAccountStore } from '@store';

@Component({ 
    props: {
        site: Object,
        userID: String
    }
}) 
 
export default class SiteCard extends Vue 
{ 

    public get siteAccount(): SiteAccount
    {
        return this.$props.site as SiteAccount;
    }

    public get userStatus()
    {
        let saua = this.siteAccount.SiteAccountUserAccounts
                        .filter(saua => saua.UserAccountID == this.$props.userID);
        if (!saua.length)
        {

            return "Request Invite";
        }
        if (!saua[0].IsActive)
        {

            return "Pending";
        }
        return "Enter Now";
    }
    public async siteCardAction()
    {
        switch(this.userStatus)
        {
            case "Request Invite":
                await SiteAccountStore.actions.requestInvite(this.$props.site);
                break;
            case "Enter Now":
                await SiteAccountStore.actions.loadSiteAccount(this.$props.site);
                this.$router.push('/sites/' + this.$props.site.SiteAccountName);
            default:
                break;
        }
    }
    public style_color(colorHex) {
      return "background-color:" + colorHex
    };
}

</script> 
 
<style scoped> 



</style> 