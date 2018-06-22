import { EntityBase } from "../EntityBase";
import Post from "../Post";
import Wager from "../Wager";

export default interface PostWager extends EntityBase
{
    PostID: string;
    Post: Post;
    WagerID: string;
    Wager: Wager;
    Prediction: number;
    RangeLength: number | null;
}