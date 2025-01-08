import http from '../../../utils/http';
import { OrderCreate } from '../models/order-create';
const createOrder = async (model: OrderCreate) => (await http.post("/orderservice/api/v1/orders", model))
