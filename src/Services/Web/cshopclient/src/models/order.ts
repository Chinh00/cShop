import {OrderDetail} from "@/models/order-detail";

export interface Order {
    id: string,
    createdDate: Date,
    customerId: string,
    orderDate: Date,
    totalPrice: number,
    discount: number,
    description: string,
    orderDetails: OrderDetail[]
    orderStatus: number
}