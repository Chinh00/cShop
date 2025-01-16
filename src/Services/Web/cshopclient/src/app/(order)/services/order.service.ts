import http from '../../../utils/http';
import { OrderCreate } from '../models/order-create';
import {ResultModel} from "@/utils/result-model";
import {Order} from "@/models/order";
import {ListResultModel} from "@/utils/list-result-model";
import {XQuery} from "@/utils/xQuery";
const createOrder = async (model: OrderCreate): Promise<ResultModel<Order>> => (await http.post("/orderservice/api/v1/orders", model)).data
const getOrders = async (xQuery: XQuery): Promise<ResultModel<ListResultModel<Order>>> => (await http.get("/orderservice/api/v1/orders/myorders", {
    headers: {
        "x-query": JSON.stringify(xQuery)
    }
})).data

export default {
    createOrder,
    getOrders,
}