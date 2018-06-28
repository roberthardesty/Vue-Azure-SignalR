import { Post } from "@entity";

export interface IPostState
{
    postSearchCriteria: any;
    isSearchingPostList: boolean;
    postList: Post[];
}