<template>
    <v-layout>
        <v-flex xs12>
            <v-card class="secondary lighten-2">
                <v-card-title primary-title class="secondary">
                    <div class="headline white--text">
                        People
                    </div>
                    <v-spacer></v-spacer>
                    <v-btn color="primary" @click="newPerson()">New Person</v-btn>
                </v-card-title>

                <v-container grid-list-xs>
                    <v-slide-x-transition>
                        <v-layout row wrap>
                            <v-flex xs12 sm5 md3 lg2 pa-2 v-for="person in personList" :key="person.ID">
                                <person-card  
                                    :person="person"
                                />
                            </v-flex>
                        </v-layout>
                    </v-slide-x-transition>
                </v-container>
                <person-form
                    :person="currentPerson"
                    :submitHandler="savePerson"
                />
                <webcam-capture
                />
                <face-collection-selection
                />
            </v-card>
        </v-flex>
    </v-layout>
</template>

<script lang="ts">

// THIS IS JUST A COMPONENT TEMPLATE I USE FOR QUICK
// COPY-PASTE-CUSTOMIZE Components

import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import PersonCard from '../cards/person-card.vue';
import PersonForm from '../forms/person-form.vue';
import FaceCollectionSelection from '../widgets/face-collection-selection.vue'
import WebcamCapture from '../widgets/webcam-capture.vue'
import { PersonStore, EventBus } from '@store'
import { Person } from '@entity';

@Component({
    components: {
        PersonCard,
        PersonForm,
        FaceCollectionSelection,
        WebcamCapture
    }
})

export default class PersonPage extends Vue
{
    public isPersonFormOpen: boolean = false;

    public get personList() { return PersonStore.getters.personList; }
    public get currentPerson() { return PersonStore.getters.currentPerson; }

    public newPerson()
    {
        this.openPersonForm({
            ID: "",
            FirstName: "",
            LastName: "",
            CreatedUTC: "",
            LastUpdatedUTC: "",
            IsActive: false,
            FaceCollections: []
        })
    }

    public openPersonForm(person: Person)
    {
        PersonStore.mutations.updateCurrentPerson(person);
        EventBus.$emit("person_form_popup_open");
    }

    public async savePerson(person: Person)
    {
        await PersonStore.actions.savePerson(person);
    }

    public async created()
    {

    }

    public async mounted()
    {

    }
}
</script>

<style scoped>

</style>
