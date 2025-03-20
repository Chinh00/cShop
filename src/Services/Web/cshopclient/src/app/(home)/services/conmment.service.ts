import {ResultModel} from "@/utils/result-model";
import {ListResultModel} from "@/utils/list-result-model";
import http from "@/utils/http";
import {XQuery} from "@/utils/xQuery";
import {Comment} from "@/models/comment";

export const getComments = async (xQuery: XQuery): Promise<ResultModel<ListResultModel<Comment>>> => {
    const res =  await http.get<ResultModel<ListResultModel<Comment>>>(`/commentservice/api/v1/comments`, {
        headers: {
            "Content-Type": "application/json",
            "x-query": JSON.stringify(xQuery),
        }
    });
    return res.data;
}
