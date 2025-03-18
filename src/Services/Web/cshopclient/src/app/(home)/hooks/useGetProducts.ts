'use client';
import {useQuery} from "@tanstack/react-query";
import {CatalogQuery, getCatalogs} from "../services/home.service"
export const useGetProducts = (xQuery: CatalogQuery) => {
    return useQuery({
        queryKey: ["catalogs", xQuery],
        queryFn: () => getCatalogs(xQuery),
    })
}