<template> 
<v-toolbar
    dense
    floating
    id="piToolBar"
    >
    <div class="text-xs-center" v-if="stepDescription">
          <v-chip>{{ stepDescription }}</v-chip>
    </div>

    <v-btn icon @click="requestSingleImageCapture()" :loading="isCameraBusy" >
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
        siteAccountID: String 
    } 
}) 
 
export default class PiCamController extends Vue 
{ 
    private connection; 

    public get stepNumber() { return PiCamStore.getters.stepNumber; }
    public get totalSteps() { return PiCamStore.getters.totalSteps; }
    public get stepDescription() { return PiCamStore.getters.stepDescription; }
    public get isCameraBusy() { return PiCamStore.getters.isCameraBusy; }


    public async requestSingleImageCapture()
    {
        console.log("Requesting Image.");
        if(PiCamStore.getters.isCameraBusy) return;
        await PiCamStore.actions.requestSingleImageCapture(this.$props.siteAccountID);
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