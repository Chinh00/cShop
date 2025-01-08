import { useMutation } from "@tanstack/react-query"
import basket from "../services/basket.service"
import { AddBasketItemModel } from "../interface"


const useAddBasketItem = (addBasketItemModel: AddBasketItemModel) => {
    return useMutation({
        mutationKey: ["addBasketItem"],
        mutationFn: basket.addBasketItem
    })
}

export default useAddBasketItem;