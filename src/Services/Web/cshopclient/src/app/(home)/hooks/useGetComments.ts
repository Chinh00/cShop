import {XQuery} from "@/utils/xQuery";
import {useQuery} from "@tanstack/react-query";
import {getComments} from "@/app/(home)/services/conmment.service";
import {ListResultModel} from "@/utils/list-result-model";
import {Comment} from "@/models/comment";

export const useGetComments = (xQuery: XQuery, action?: (data: ListResultModel<Comment>) => void) => {
    return useQuery({
        queryKey: ["comments", xQuery],
        queryFn: () => getComments(xQuery),
        staleTime: 0,
        select: (data) => {
            if (action) {
                action(data?.data)
            }
            return data?.data
        }
    })
}