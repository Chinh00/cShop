import { useQuery } from "@tanstack/react-query"
import basket from "../services/basket.service"
const useGetBasketDetail = () => {
    return useQuery({
        queryKey: ["GetBasketDetail"],
        queryFn: () => basket.getBasketDetail(),
        staleTime: 0,
        
    })
}
export default useGetBasketDetail;