import {useQuery} from "@tanstack/react-query";
import {getBrands, getCatalogs, getTypes} from "../services/home.service"
import {XQuery} from "@/utils/xQuery";
export const useGetBrands = (xQuery: XQuery) => {
    return useQuery({
        queryKey: ["brands", xQuery],
        queryFn: () => getBrands(xQuery),
    })
}