<template>
  <v-layout row justify-center>
      <v-flex xs10>
           <v-dialog v-model="isOpen" persistent :lazy=true>
            <v-card>
                <v-card-title class="headline">Enter your name</v-card-title>
                <v-card-text>
                    <v-container fluid>
                        <v-form v-on:submit="submit($event)">
                            <v-text-field
                            v-model="person.FirstName"
                            color="cyan darken"
                            label="First Name"
                            placeholder="Start Typing..."
                            autofocus
                            loading
                            required
                            :error-messages="firstNameErrors"
                            @input="firstNameDirty = true"
                            @blur="firstNameDirty = true"
                            >
                            <v-text-field
                            v-model="person.LastName"
                            color="cyan darken"
                            label="Last Name"
                            placeholder="Start Typing..."
                            autofocus
                            loading
                            required
                            :error-messages="lastNameErrors"
                            @input="lastNameDirty = true"
                            @blur="lastNameDirty = true"
                            >
                            <v-progress-linear
                                slot="progress"
                                :value="progress"
                                :color="color"
                                height="7"
                            ></v-progress-linear>
                            </v-text-field>
                        </v-form>
                    </v-container>
                </v-card-text>
                <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn color="primary" :disabled="!isValid" flat @click="submit">Submit</v-btn>
                </v-card-actions>
            </v-card>
            </v-dialog>
      </v-flex>
   
  </v-layout>
</template>

<script lang="ts">

// THIS IS JUST A COMPONENT TEMPLATE I USE FOR QUICK
// COPY-PASTE-CUSTOMIZE Components

import Vue from 'vue';
import { Component, Watch } from 'vue-property-decorator';
import { EventBus, UserAccountStore } from '@store';
import debounce from 'lodash/debounce';

@Component({
    props: {
        person: Object
    }
})

export default class PersonForm extends Vue
{
    public isOpen: boolean = false;

    public firstNameDirty: boolean = false;
    public lastNameDirty: boolean = false;

    public get progress () {
        let p = 0
        if(this.$props.person.FirstName.length > 0)
            p++;
        if(this.$props.person.LastName.length > 0)
            p++;
        return Math.min(100, p * 50)
    };

    public get color () {
        return this.isUsernameTaken ? "error" : ['error', 'warning', 'success'][Math.floor(this.progress / 50)]
    };

    public get firstNameErrors () {
        const errors = []
        if (!this.firstNameDirty) return errors
        !this.username.length && errors.push('First name is required.')
        return errors
    };
    
    public get lastNameErrors () {
        const errors = []
        if (!this.lastNameDirty) return errors
        !(this.$props.person.LastName.length > 0) && errors.push('Last name is required.')
        return errors
    };

    public get isValid () {
        return (this.$props.person.FirstName.length > 1 && this.$props.person.LastName.length > 0)
    }
        
    public created()
    {

    }

    public submit(event)
    {
        event.preventDefault();
        this.firstNameDirty = true;
        this.lastNameDirty = true;
        if(!this.isValid)
            return;

        this.isOpen = false;
    }
}
</script>

<style scoped>

</style>
