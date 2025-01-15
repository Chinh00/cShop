import {useMutation} from "@tanstack/react-query";
import payment from "../services/payment.service"
const useGetPaymentUrl = () => {
    return useMutation({
        mutationKey: ["paymentUrl"],
        mutationFn: payment.getPaymentUrl,
        retry: 5,
        retryDelay: 1000,
    })
}
export default useGetPaymentUrl