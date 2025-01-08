import { useMutation } from "@tanstack/react-query"

const useCreateOrder = () => {
    return useMutation({
        mutationKey: ["createOrder"],
    })
}