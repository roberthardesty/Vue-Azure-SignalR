<template>
  <v-layout row justify-center>
      <v-flex xs10>
           <v-dialog v-model="isOpen" persistent :lazy=true>
            <v-card>
                <v-card-title class="headline">Enter your username</v-card-title>
                <v-card-text>
                    <v-container fluid>
                        <v-form v-on:submit="submit($event)">
                            <v-text-field
                            v-model="username"
                            color="cyan darken"
                            label="Username"
                            placeholder="Start Typing..."
                            autofocus
                            loading
                            required
                            :error-messages="usernameErrors"
                            @input="usernameDirty = true"
                            @blur="usernameDirty = true"
                            >
                            <v-progress-linear
                                v-if="isUserTyping"
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
                <v-btn color="primary" :disabled="!isValid || isDebounceLoading" flat @click="submit">Submit</v-btn>
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
import { required, minLength } from 'vuelidate/lib/validators'
import { EventBus, UserAccountStore } from '@store';
import debounce from 'lodash/debounce';

@Component({
    
})

export default class UsernameForm extends Vue
{
    public username: string = "";
    public usernameDirty: boolean = false;
    public isUserTyping: boolean = true;
    public isOpen: boolean = false;
    public isUsernameTaken = false;
    public isDebounceLoading = false;

    @Watch('username') onUsernameChanged(newVal: string, oldVal: string)
    {
        this.isDebounceLoading = true;
        this.debouncedCheckUsername();
    }

    private debouncedCheckUsername: any = {};
    private async checkUsername()
    {
        if(this.username.length < 8)
        {
            this.isUserTyping = true;
            return;
        }
        this.isUserTyping = false;
        this.isUsernameTaken = !(await UserAccountStore.actions.checkUsernameAvailability(this.username));
        this.isUserTyping = true;
        this.isDebounceLoading = false;
    }

    public get progress () {
        return Math.min(100, this.username.length * 12.5)
    };

    public get color () {
        return this.isUsernameTaken ? "error" : ['error', 'warning', 'success'][Math.floor(this.progress / 45)]
    };

    public get usernameErrors () {
        const errors = []
        if (!this.usernameDirty) return errors
        !(!this.isUsernameTaken) && errors.push('Username taken.')
        !this.username.length && errors.push('Username is required.')
        return errors
    };

    public get isValid () {
        return !(this.usernameErrors.length) && this.progress == 100
    }

    public validations = 
    {
        username: {
            required,
            minLength: minLength(8)
        }
    }
        
    public created()
    {
        let self = this;
        EventBus.$on("username_popup_open", () => {
            self.isOpen = true;
        });

        self.debouncedCheckUsername = debounce(this.checkUsername, 750);
    }

    public submit(event)
    {
        event.preventDefault();
        this.usernameDirty = true;
        if(!this.isValid || this.isDebounceLoading)
            return;

        EventBus.$emit("username_popup_close", this.username);
        this.isOpen = false;
    }
}
</script>

<style scoped>

</style>
