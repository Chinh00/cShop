import http from "@/utils/http"
import {ResultModel} from "@/utils/result-model";

const getPaymentUrl = async (orderId: string): Promise<ResultModel<string>> => {
    return (await http.post(`/paymentservice/api/v1/payments/payment-url`, {
        orderId: orderId
    })).data
}
export default {
    getPaymentUrl
}