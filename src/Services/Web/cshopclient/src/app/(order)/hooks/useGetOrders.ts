import {useQuery} from "@tanstack/react-query";
import {XQuery} from "@/utils/xQuery";
import order from "../services/order.service"

const useGetOrders = (xQuery: XQuery) => {
    return useQuery({
        queryKey: ["orders", xQuery],
        queryFn: () => order.getOrders(xQuery)
    })
}
export default useGetOrders