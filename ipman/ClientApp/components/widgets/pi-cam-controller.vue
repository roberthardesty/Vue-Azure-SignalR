<template> 
<v-toolbar
    dense
    floating
    id="piToolBar"
    >
    

    <v-btn icon @click="requestSingleImageCapture()">
        <v-icon>my_location</v-icon>
    </v-btn>

    <v-btn icon>
        <v-icon>more_vert</v-icon>
    </v-btn>
</v-toolbar>
</template> 
 
<script lang="ts"> 
 
// THIS IS JUST A COMPONENT TEMPLATE I USE FOR QUICK 
// COPY-PASTE-CUSTOMIZE Components 
 
import Vue from 'vue'; 
import { Component } from 'vue-property-decorator'; 
import { PiCamStore, EventBus } from '@store'; 
 
@Component({ 
    props: { 
        siteAccount: Object 
    } 
}) 
 
export default class PiCamController extends Vue 
{ 
    private connection; 

    public async requestSingleImageCapture()
    {
        console.log("Requesting Image.");
        await PiCamStore.actions.requestSingleImageCapture();
        console.log("Should be finished.");
    }

    public data(): any 
    { 
        return { msg: '' }; 
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
</style> 