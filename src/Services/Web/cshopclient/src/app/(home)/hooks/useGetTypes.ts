import {useQuery} from "@tanstack/react-query";
import {getCatalogs, getTypes} from "../services/home.service"
import {XQuery} from "@/utils/xQuery";
export const useGetTypes = (xQuery: XQuery) => {
    return useQuery({
        queryKey: ["types", xQuery],
        queryFn: () => getTypes(xQuery),
    })
}