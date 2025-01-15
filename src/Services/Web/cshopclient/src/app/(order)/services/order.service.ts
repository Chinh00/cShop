import http from '../../../utils/http';
import { OrderCreate } from '../models/order-create';
import {ResultModel} from "@/utils/result-model";
import {Order} from "@/models/order";
const createOrder = async (model: OrderCreate): Promise<ResultModel<Order>> => (await http.post("/orderservice/api/v1/orders", model)).data

export default {
    createOrder
}