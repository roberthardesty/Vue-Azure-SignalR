<template>
    <v-layout>
        <v-flex xs12>
            <v-card class="secondary lighten-2">
                <v-card-title primary-title class="secondary">
                    <div class="headline white--text">
                        Welcome, here are your available places
                    </div>
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
import { PersonStore } from '@store'

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
