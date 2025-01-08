import { useMutation } from "@tanstack/react-query"
import basket from "../services/basket.service"

const useRemoveBasketItem = () => {
    return useMutation({
        mutationKey: ["RemoveBasketItem"],
        mutationFn: basket.removeBasketItem
    })
}
export default useRemoveBasketItem;