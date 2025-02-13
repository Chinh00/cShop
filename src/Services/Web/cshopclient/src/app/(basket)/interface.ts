'use client'

import {atom} from "recoil";
import {Basket} from "@/models/basket";

export type AddBasketItemModel = {
    productId: string
}
export type removeBasketItemModel = {
    productId: string
}

export const cartState = atom<Basket>({
    key: "cartState",
    default: {
        id: "",
        basketItems: [],
        userId: "",
        totalPrice: 0
    }
});

