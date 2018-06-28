import { IPostState, ISiteAccountState } from ".";
import { Route } from "vue-router/types/router";

export interface RootState
{
    PostModule: IPostState,
    SiteAccountModule: ISiteAccountState,
    route: Route
}