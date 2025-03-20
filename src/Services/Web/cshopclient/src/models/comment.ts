export type Comment = {
    id: string,
    userId: string,
    user: {
        username?: string,
        avatar?: string,
    }
    productId: string,
    content: string,
    commentLines: Comment[],
    createdAt: string,
    updatedAt: string
}