import Router from '../../router';
import { Post } from '@entity';
import { IPostState } from '../Types';
import { storeBuilder } from './Store/Store';

const state: IPostState = 
{
    postSearchCriteria: {},
    isSearchingPostList: false,
    postList: []
};

const b = storeBuilder.module<IPostState>("PostModule", state);
const stateGetter = b.state()

namespace Getters {
    export const getters = {
    } 
}

namespace Mutations {
    export const mutations = {

    }
}

namespace Actions {
    export const actions = {

    }
}

const PostModule = {
    get state() { return stateGetter()},
    getters: Getters.getters,
    mutations: Mutations.mutations,
    actions: Actions.actions
  }
  
  
  export default PostModule;
  
  