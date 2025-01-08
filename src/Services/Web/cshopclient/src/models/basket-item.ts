export interface BasketItem {
    basketId: string,
    productId: string,
    quantity: number,
    id: string,
    createdDate: Date,
    updatedDate?: Date
}