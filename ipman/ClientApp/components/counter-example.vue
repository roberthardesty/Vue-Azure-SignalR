<template>
    <v-layout>
        <v-flex fill-height>
            <h1>Counter</h1>

            <p>This is a simple example of a Vue.js component & SignalR. To see how this data is coming from the server, open this page in more than one browser tab.  You will notice how the count is synchronized between the two, because the data is being pushed to each client from the server.</p>

            <p>
                Auto count: <strong>{{ count }}</strong>
            </p>
        </v-flex>
    </v-layout>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";
@Component({})
export default class Counter extends Vue {
  connection = null;
  count = 0;

  created() {
    this.connection = new this.$signalR.HubConnectionBuilder()
      .withUrl("/count")
      .configureLogging(this.$signalR.LogLevel.Error)
      .build();
  }
  mounted() {
    this.connection.start();

    this.connection.on("increment", data => {
      this.count = data;
    });
  }
}
</script>

<style>
</style>
