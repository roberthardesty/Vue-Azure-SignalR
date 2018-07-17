<template>
    <v-navigation-drawer
      v-model="drawer"
      clipped
      absolute
      temporary
      class="secondary darken-2"
      app
    >
      <v-list
        dense
        color="secondary"
      >
        <template v-for="(item, i) in items">
          <v-divider
            v-if="item.divider"
            :key="i"
            dark
            class="my-3"
          ></v-divider>
          <v-list-tile
            v-else
            :key="i"
          >
            <v-list-tile-action>
              <v-icon color="white">{{ item.icon }}</v-icon>
            </v-list-tile-action>
            <v-list-tile-content>
              <v-list-tile-title class="white--text">
                {{ item.text }}
              </v-list-tile-title>
            </v-list-tile-content>
          </v-list-tile>
        </template>
      </v-list>
    </v-navigation-drawer>
</template>

<script lang="ts">
import Vue from 'vue'
import { Component } from 'vue-property-decorator';
import { routes } from '../routes'

@Component({
    props: {
        isOpen: Boolean
    }
})
export default class NavMenu extends Vue 
{
    public get drawer()
    {
      return this.$props.isOpen
    };
    public set drawer(r)
    {
    };
    public data(): any
    {
        return {
            items: [
                { icon: 'lightbulb_outline', text: 'Notes' },
                { icon: 'touch_app', text: 'Reminders' },
                { divider: true },
                { icon: 'add', text: 'Create new label' },
                { divider: true },
                { icon: 'archive', text: 'Archive' },
                { icon: 'delete', text: 'Trash' },
                { divider: true },
                { icon: 'settings', text: 'Settings' },
                { icon: 'chat_bubble', text: 'Trash' },
                { icon: 'help', text: 'Help' },
                { icon: 'phonelink', text: 'App downloads' },
                { icon: 'keyboard', text: 'Keyboard shortcuts' }
            ]
        }
    }
}
</script>

<style>
 #keep main .container {
    height: 660px;
  }
  .navigation-drawer__border {
    display: none;
  }
  .text {
    font-weight: 400;
  }
</style>