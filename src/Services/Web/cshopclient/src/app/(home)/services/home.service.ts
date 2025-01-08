'use client';
import { ResultModel } from "@/utils/result-model"
import http from "@/utils/http"
import { ListResultModel } from "@/utils/list-result-model";
import { Product } from "@/models/Product";
export const getCatalogs = async (): Promise<ResultModel<ListResultModel<Product>>> => {
    const res =  await http.get<ResultModel<ListResultModel<Product>>>("/catalogservice/api/v1/catalogs");
    return res.data;
}

export const getCatalogDetail = async (id: string): Promise<ResultModel<Product>> => {
    const res =  await http.get<ResultModel<Product>>(`/catalogservice/api/v1/catalogs/${id}`);
    return res.data;
}