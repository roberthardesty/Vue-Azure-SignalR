<template>
    <v-layout wrap>
        <v-flex xs12>
            <v-card class="secondary lighten-2">
                <v-card-title primary-title class="secondary">
                    <div class="headline white--text">
                        {{SiteName}}
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

                    <v-tab href="#tab-2" @click="pendingUsersTab()">
                        <v-icon>search</v-icon>
                    </v-tab>

                    <v-tab-item
                    id="tab-1"
                    >
                        <v-container grid-list-xs>
                            <v-layout row wrap v-if="postList.length">
                                <v-flex xs12 sm6 md4 lg3 mt-2 mb-2 pr-2 pl-2 v-for="post in postList" :key="post.ID">
                                    <post-card :post="post"/>
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
                             <v-container grid-list-xs>
                            <v-layout row wrap v-if="pendingSiteUsersList.length">
                                <v-flex xs12 sm6 v-for="siteUser in pendingSiteUsersList" :key="siteUser.user.ID">
                                    <v-card>
                                        <v-card-title primary-title>
                                            <div class="headline black--text">{{siteUser.user.Username}}</div>
                                        </v-card-title>
                                        <v-combobox
                                            v-model="siteUser.site.Role"
                                            :items="AllRoles"
                                            item-text="RoleName"
                                            label="Role"
                                            chips
                                            prepend-icon="people"
                                            solo
                                        >
                                            <template slot="selection" slot-scope="data">
                                            <v-chip
                                                :selected="data.selected"
                                                @input="remove(data.item)"
                                            >
                                                <strong>{{ data.item.RoleName }}</strong>&nbsp;
                                                <span>(role)</span>
                                            </v-chip>
                                            </template>
                                        </v-combobox>
                                        <v-card-actions>
                                                <v-btn @click="inviteUser(siteUser)" 
                                                    tag="div">
                                                    Invite
                                                </v-btn>
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
                        </v-flex>
                    </v-tab-item>
                </v-tabs>
                
            </v-card>
        </v-flex>
        <v-flex v-if="isPiCamSite">
         <pi-cam-controller
            :siteAccountID="activeSite.ID">
         </pi-cam-controller>
        </v-flex>

    </v-layout>
</template>

<script lang="ts">

// THIS IS JUST A COMPONENT TEMPLATE I USE FOR QUICK
// COPY-PASTE-CUSTOMIZE Components

import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import PostCard from './cards/post-card.vue';
import PiCamController from './widgets/pi-cam-controller.vue';
import {Post, UserAccount, SiteAccountUserAccount, Role} from '@entity';
import {SiteAccountType} from '../entity/lookups/SiteAccountType'
import { PostStore, SiteAccountStore, PiCamStore, UserAccountStore, EventBus } from '@store'; 


@Component({
    components: {
        PostCard,
        PiCamController
    }
})

export default class SitePage extends Vue
{
    private connection;

    public SiteName = "";
    public selectedTab: string = "";
    public get postList() { return PostStore.getters.postList; }
    public get activeSite() { return SiteAccountStore.getters.activeSiteAccount; }
    public get isPiCamSite() { return SiteAccountStore.getters.activeSiteAccount.SiteAccountType == SiteAccountType.RaspberryPi }
    public get pendingSiteUsersList()
    {
        return UserAccountStore.getters.userAccountList.map(user => 
        {
            return {
                user: user,
                site: user.SiteAccountUserAccounts.filter(saua => saua.SiteAccountID == this.activeSite.ID)[0]
            }
        }); 
    }
    public get AllRoles() { return Role.AllRoles;}
    
    public async deletePost(postID: string)
    {
        if(!this.activeSite) return;
        console.log("Deleteing Post Event Called!")
        await PostStore.actions.deletePost({ postID: postID, siteID: this.activeSite.ID });
    }

    public async inviteUser(siteUser: {user: UserAccount, site: SiteAccountUserAccount})
    {
        siteUser.site.RoleID = siteUser.site.Role.RoleID;
        siteUser.site.IsActive = true;
        await SiteAccountStore.actions.updateSiteUser(siteUser.site)
    }

    public async pendingUsersTab()
    {
        console.log(this.pendingSiteUsersList.length)
        if(this.pendingSiteUsersList.length)
            return;

        await UserAccountStore.actions.loadPendingUsersForSite(this.activeSite.ID);
    }

    public registerEvents()
    {
        EventBus.$off(this.deletePost.name);
        EventBus.$on(this.deletePost.name, this.deletePost);
    }

    public data(): any
    {
        return { msg: '' };
    }
    
    public created()
    {
        this.registerEvents();
    }
    public async mounted()
    {
        let self = this; 
        self.SiteName = self.$route.params.site
        if(!self.activeSite.ID)
        {
            self.$router.push('/dashboard');
            return;
        }

        self.$vuetify.theme.primary = self.activeSite.SiteAccountThemeColorPrimary;
        self.$vuetify.theme.secondary = self.activeSite.SiteAccountThemeColorSecondary;
    }
}
</script>

<style scoped>

#piToolBar {
  z-index:5;  
  bottom: 0;
  position: fixed;
  -webkit-transition: all 1s ease;
  -moz-transition: all 1s ease;
  transition: all 1s ease;
  right: 30px;
}

</style>
