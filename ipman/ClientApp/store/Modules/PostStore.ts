import Router from '../../router';
import { Post } from '@entity';
import { IPostState } from '../Types';
import { PostService } from '../Api/Services';
import { storeBuilder } from './Store/Store';

const postApiService = new PostService();
const state: IPostState = 
{
    postSearchCriteria: {},
    isSearchingPostList: false,
    postList: []
};

const b = storeBuilder.module<IPostState>("PostModule", state);
const stateGetter = b.state()

namespace Getters 
{
    const postList = b.read(function postList(state) {
        return state.postList;
    })

    export const getters = {
        get postList() {return postList()}
    } 
}

namespace Mutations {

    function updatePostList(state:IPostState, newList: Post[])
    {
        state.postList = newList;
    }

    export const mutations = {
        updatePostList: b.commit(updatePostList)
    }
}

namespace Actions {
    async function fetchPostList(context, siteName: string)
    {
        let response = await postApiService.GetPostsBySiteAccountName(siteName);
        if(!response.success)
            throw "a fit";
        Mutations.mutations.updatePostList(response.data as Post[])
    }
    export const actions = {
        fetchPostList: b.dispatch(fetchPostList)
    }
}

const PostModule = {
    get state() { return stateGetter()},
    getters: Getters.getters,
    mutations: Mutations.mutations,
    actions: Actions.actions
}
  
export default PostModule;
