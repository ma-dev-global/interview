export class Comment {
    public commentId?: number;
    public commentText: string;
    public userId: number;
    public createdTimestamp: Date;

    constructor(commentText: string, userId: number, createdTimestamp: Date, commentId?: number) {
        this.commentId = commentId;
        this.commentText = commentText;
        this.userId = userId;
        this.createdTimestamp = createdTimestamp;
    }
}