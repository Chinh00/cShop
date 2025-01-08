export type OrderCreate = {
    items: OrderItemCreate[];
    orderDate: Date;
}

export type OrderItemCreate = {
    productId: string
    quantity: number
}