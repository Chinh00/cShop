'use client'
import { useQuery } from "@tanstack/react-query"
import basket from "../services/basket.service"
import {useRecoilState, useSetRecoilState} from "recoil";
import {cartState} from "@/app/(basket)/interface";
const useGetBasketDetail = () => {
    
    return useQuery({
        queryKey: ["GetBasketDetail"],
        queryFn: () => basket.getBasketDetail(),
        staleTime: 0,
        select: data => {
            return data
        }
        
    })
}
export default useGetBasketDetail;