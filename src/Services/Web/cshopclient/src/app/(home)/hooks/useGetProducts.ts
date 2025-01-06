'use client';
import {useQuery} from "@tanstack/react-query";
import {getCatalogs} from "../services/home.service"
import { Product } from "@/models/Product";
export const useGetProducts = () => {
    return useQuery({
        queryKey: ["catalogs"],
        queryFn: () => getCatalogs(),
    })
}