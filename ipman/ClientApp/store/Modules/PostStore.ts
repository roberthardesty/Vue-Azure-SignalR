import Router from '../../router';
import { Post } from '@entity';
import { IPostState } from '../Types';
import { PostService } from '../Api/Services';
import { storeBuilder } from './Store/Store';
import { DeletePostResponse } from '@serviceModels';

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

    async function deletePost(context, postSite: {postID: string, siteID: string})
    {
        let apiReponse = await postApiService.DeletePost(postSite.postID, postSite.siteID);
        if(apiReponse.success)
        {
            let deleteResponse: DeletePostResponse = apiReponse.data as DeletePostResponse;
            if(!deleteResponse.IsError)
            {
                let deletedPostID = deleteResponse.PostID
                let newList =  Getters.getters.postList.filter(post => post.ID != deletedPostID);
                Mutations.mutations.updatePostList(newList);
            }
            else
                console.log(deleteResponse.ResponseError.ToFormattedString());
        }
        else
            console.log("API Error.");
    }

    async function fetchPostList(context, siteId: string)
    {
        let response = await postApiService.GetPostsBySiteAccount(siteId);
        if(!response.success)
            throw "a fit";
        Mutations.mutations.updatePostList(response.data as Post[])
    }
    export const actions = {
        fetchPostList: b.dispatch(fetchPostList),
        deletePost: b.dispatch(deletePost)
    }
}

const PostModule = {
    get state() { return stateGetter()},
    getters: Getters.getters,
    mutations: Mutations.mutations,
    actions: Actions.actions
}
  
export default PostModule;
