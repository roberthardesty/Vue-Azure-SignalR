import { IPiCamState } from '../Types';
import { PiCamService } from '../SignalR/Services';
import { storeBuilder } from './Store/Store';

const state: IPiCamState = 
{
    isCameraBusy: false
};

const b = storeBuilder.module<IPiCamState>("PiCamModule", state);
const stateGetter = b.state()

const piCamService = new PiCamService();

namespace Getters {
    export const getters = {

    } 
}

namespace Mutations {
    export const mutations = {

    }
}

namespace Actions {
    async function requestSingleImageCapture()
    {
        await piCamService.RequestSingleImageCapture();
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
  
  