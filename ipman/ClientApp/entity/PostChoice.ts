import { EntityBase } from "./EntityBase";
import Post from "./Post";

export default interface PostChoice extends EntityBase
{
    ChoiceName: string;
    ChoiceValue: number;
    Order: number;
    PostID: string;
    Post: Post;
}
