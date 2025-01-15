'use client'
import { useMutation } from "@tanstack/react-query"
import order from "../services/order.service"

const useCreateOrder = () => {
    return useMutation({
        mutationKey: ["createOrder"],
        mutationFn: order.createOrder
    })
}

export default useCreateOrder