<template>
  <v-layout row justify-center>
      <v-flex xs10>
           <v-dialog v-model="isOpen" persistent :lazy=true>
            <v-card>
                <v-card-title class="headline">Enter your username</v-card-title>
                <v-card-text>
                    <v-container fluid>
                        <form>
                            <v-text-field
                            v-model="username"
                            color="cyan darken"
                            label="Username"
                            placeholder="Start typing..."
                            autofocus
                            loading
                            required
                            :error-messages="usernameErrors"
                            @input="usernameDirty = true"
                            @blur="usernameDirty = true"
                            >
                            <v-progress-linear
                                v-if="typing"
                                slot="progress"
                                :value="progress"
                                :color="color"
                                height="7"
                            ></v-progress-linear>
                            </v-text-field>
                        </form>
                    </v-container>
                </v-card-text>
                <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn color="primary" flat @click="submit">Submit</v-btn>
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
    public typing: boolean = true;
    public isOpen: boolean = false;
    public isUsernameTaken = false;

    @Watch('username') onUsernameChanged(newVal: string, oldVal: string)
    {
        if(this.username.length < 8)
        {
            this.typing = true;
            return;
        }
        this.debouncedCheckUsername();
    }

    private debouncedCheckUsername: any = {};
    private async checkUsername()
    {
        this.typing = false;
        await UserAccountStore.actions.checkUsernameAvailability(this.username);
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
        //!(this.username.length > 8) && errors.push('Name must be at least 8 characters long')
        !this.username.length && errors.push('Username is required.')
        return errors
    };

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

    public submit()
    {
        EventBus.$emit("username_popup_close", this.username);
        this.isOpen = false;
    }
}
</script>

<style scoped>

</style>
