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
                            required
                            :error-messages="firstNameErrors"
                            @input="firstNameDirty = true"
                            @blur="firstNameDirty = true"
                            >
                            </v-text-field>

                            <v-text-field
                            v-model="person.LastName"
                            color="cyan darken"
                            label="Last Name"
                            placeholder="Start Typing..."
                            required
                            :error-messages="lastNameErrors"
                            @input="lastNameDirty = true"
                            @blur="lastNameDirty = true"
                            >
                            </v-text-field>
                        </v-form>
                    </v-container>
                </v-card-text>
                <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn color="primary" :disabled="!isValid" flat @click="submit">Submit</v-btn>
                <v-btn color="primary" flat @click="isOpen = false">Cancel</v-btn>
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
        person: Object,
        submitHandler: Function
    }
})

export default class PersonForm extends Vue
{
    public isOpen: boolean = false;

    public firstNameDirty: boolean = false;
    public lastNameDirty: boolean = false;

    public get firstNameErrors () {
        const errors = []
        if (!this.firstNameDirty) return errors
        !this.$props.person.FirstName.length && errors.push('First name is required.')
        return errors
    };
    
    public get lastNameErrors () {
        const errors = []
        if (!this.lastNameDirty) return errors
        !this.$props.person.LastName.length && errors.push('Last name is required.')
        return errors
    };

    public get isValid () {
        return (this.$props.person.FirstName.length > 1 && this.$props.person.LastName.length > 0)
    }
        
    public created()
    {
        let self = this;
        EventBus.$on("person_form_popup_open", () => {
            self.isOpen = true;
        });
    }

    public submit(event)
    {
        event.preventDefault();
        this.firstNameDirty = true;
        this.lastNameDirty = true;
        if(!this.isValid)
            return;
        this.$props.submitHandler(this.$props.person);
        this.isOpen = false;
    }
}
</script>

<style scoped>

</style>
