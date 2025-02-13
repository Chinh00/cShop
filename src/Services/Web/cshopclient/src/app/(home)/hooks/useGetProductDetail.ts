'use client'
import { useQuery } from "@tanstack/react-query"
import { getCatalogDetail } from "../services/home.service";
import { useRecoilState } from "recoil";
import {cartState} from "@/app/(basket)/interface";

const useGetProductDetail = (id: string) => {
    return useQuery({
        queryKey: ['productId', id],
        queryFn: () => getCatalogDetail(id),
        staleTime: 0,
        
    });
}
export default useGetProductDetail;