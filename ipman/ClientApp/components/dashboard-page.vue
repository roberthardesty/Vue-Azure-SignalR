<template>
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
</template>

<script lang="ts">

// THIS IS JUST A COMPONENT TEMPLATE I USE FOR QUICK
// COPY-PASTE-CUSTOMIZE Components

import Vue from 'vue';
import { Component } from 'vue-property-decorator';

@Component({

})

export default class DashboardPage extends Vue
{
    public connection: any = null;
    public data(): any
    {
        return { msg: '' };
    }

    public created()
    {
        this.connection = new this.$signalR.HubConnectionBuilder()
            .withUrl("/count")
            .configureLogging(this.$signalR.LogLevel.Error)
            .build();

    }

    public mounted(): void
    {
        this.connection.start();
        
        this.connection.on('increment', data => {
            
        });
    }
}
</script>

<style scoped>

</style>
