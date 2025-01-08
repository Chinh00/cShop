import { ResultModel } from '@/utils/result-model';
import http from '../../../utils/http';
import { Basket } from '@/models/basket';
import { AddBasketItemModel } from '../interface';

const getBasketDetail = async (): Promise<ResultModel<Basket>> => 
    (await http.get("/basketservice/api/v1/baskets"))?.data;

const createBasket = async (): Promise<ResultModel<Basket>> =>
    await http.post("/basketservice/api/v1/baskets/create");
const addBasketItem = async (addBasketItemModel: AddBasketItemModel): Promise<ResultModel<Basket>> =>
    await http.post("/basketservice/api/v1/baskets/add", addBasketItemModel);
const removeBasketItem = async (productId: string): Promise<ResultModel<Basket>> =>
    await http.delete(`/basketservice/api/v1/baskets/${productId}`);




export default {
    getBasketDetail,
    addBasketItem,
    createBasket,
    removeBasketItem,
}