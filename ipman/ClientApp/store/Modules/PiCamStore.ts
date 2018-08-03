import { IPiCamState } from '../Types';
import { PiCamService } from '../SignalR/Services';
import { storeBuilder } from './Store/Store';
import { PostStore } from '@store';

const state: IPiCamState = 
{
    isCameraBusy: false,
    stepNumber: 0,
    totalSteps: 0,
    stepDescription: ""
};

const b = storeBuilder.module<IPiCamState>("PiCamModule", state);
const stateGetter = b.state()

const piCamService = new PiCamService();

namespace Getters {
    const isCameraBusy = b.read(function isCameraBusy(state) {
        return state.isCameraBusy;
    });

    const stepNumber = b.read(function stepNumber(state) {
        return state.stepNumber;
    });
    const totalSteps = b.read(function totalSteps(state) {
        return state.totalSteps;
    });
    const stepDescription = b.read(function stepDescription(state) {
        return state.stepDescription;
    });

    export const getters = {
        get isCameraBusy() {return isCameraBusy()},
        get stepNumber() {return stepNumber()},
        get totalSteps() {return totalSteps()},
        get stepDescription() {return stepDescription()}
    } 
}

namespace Mutations {

    function updateProgress(state: IPiCamState, progress: { StepName: string, CurrentStep: number, TotalSteps: number})
    {
        state.stepNumber = progress.CurrentStep;
        state.totalSteps = progress.TotalSteps;
        state.stepDescription = progress.StepName;
    }

    export const mutations = {
        updateProgress: b.commit(updateProgress)
    }
}

namespace Actions {

    async function requestSingleImageCapture(siteID: string)
    {
        state.isCameraBusy = true;
        Mutations.mutations.updateProgress({StepName: "Sending", CurrentStep: 0, TotalSteps: 0})
        await piCamService.InitializeConnection();
        await piCamService.JoinSpectators();
        await piCamService.RequestSingleImageCapture();
        await piCamService.UpdateProgress((progress) =>
        {
            console.log(progress)
            Mutations.mutations.updateProgress(progress);
            if(progress.TotalSteps == progress.CurrentStep)
            {
                piCamService.CloseConnection();
                PostStore.actions.fetchPostList(siteID);
                state.isCameraBusy = false;
            }
        });

    }

    export const actions = {
        requestSingleImageCapture: requestSingleImageCapture
    }
}

const PiCamModule = {
    get state() { return stateGetter()},
    getters: Getters.getters,
    mutations: Mutations.mutations,
    actions: Actions.actions
  }
  
  
  export default PiCamModule;
  
  