import { BasketItem } from "./basket-item";

export interface Basket {
    id: string,
    userId: string,
    basketItems: BasketItem[],
    totalPrice: number,
    createdDate: Date,
    updatedDate?: Date
}