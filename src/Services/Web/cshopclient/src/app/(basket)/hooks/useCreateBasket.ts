import { useMutation } from "@tanstack/react-query"
import basket from "../services/basket.service"

const useCreateBasket = () => {
    return useMutation({
        mutationKey: ["useCreateBasket"],
        mutationFn: basket.createBasket,
    })
}

export default useCreateBasket;