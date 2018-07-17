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
            <v-list-tile-action v-if="item.isColorPicker">
              <v-btn icon @click.native="showColors = !showColors">
                <v-icon color="white">{{ item.icon }}</v-icon>
              </v-btn>
            </v-list-tile-action>

            <v-list-tile-action v-else>
              <v-icon color="white">{{ item.icon }}</v-icon>
            </v-list-tile-action>

            <v-list-tile-content>
              <v-list-tile-title @click.native="showColors = !showColors" class="white--text">
                {{ item.text }}
              </v-list-tile-title>
            </v-list-tile-content>

          </v-list-tile>
        </template>

            <v-slide-y-transition >
              <v-container
                  fluid
                  grid-list-md
                  v-show="showColors"
                >
                  <v-layout row wrap>
                    <v-flex xs12>
                      <v-card >
                          Test
                      </v-card>
                    </v-flex>
                  </v-layout>
              </v-container>
            </v-slide-y-transition>
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
            showColors:  false,
            items: [
                { icon: 'chat_bubble', text: 'Chat' },
                { divider: true },
                { icon: 'archive', text: 'Archive' },
                { icon: 'delete', text: 'Trash' },
                { divider: true },
                { icon: 'settings', text: 'Settings' },
                { icon: 'format_color_fill', text: 'Theme Colors', isColorPicker: true },
                { divider: true },
                { icon: 'help', text: 'Help' },
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