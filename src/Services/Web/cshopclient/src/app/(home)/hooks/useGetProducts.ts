'use client';
import {useQuery} from "@tanstack/react-query";
import {getCatalogs} from "../services/home.service"
import {XQuery} from "@/utils/xQuery";
export const useGetProducts = (xQuery: XQuery) => {
    return useQuery({
        queryKey: ["catalogs", xQuery],
        queryFn: () => getCatalogs(xQuery),
    })
}