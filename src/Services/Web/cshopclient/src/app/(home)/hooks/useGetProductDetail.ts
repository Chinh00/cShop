'use client'
import { useQuery } from "@tanstack/react-query"
import { getCatalogDetail } from "../services/home.service";

const useGetProductDetail = (id: string) => {
    return useQuery({
        queryKey: ['productId', id],
        queryFn: () => getCatalogDetail(id),
    });
}
export default useGetProductDetail;