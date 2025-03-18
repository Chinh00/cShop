'use client';
import { ResultModel } from "@/utils/result-model"
import http from "@/utils/http"
import { ListResultModel } from "@/utils/list-result-model";
import {XQuery} from "@/utils/xQuery";
import {Catalog, Product} from "@/models/product";
import {CatalogType} from "@/models/catalogType";
import {CatalogBrand} from "@/models/catalogBrand";

export type CatalogQuery = {
    q?: string;
    catalogTypeId?: string;
    catalogBrandId?: string;
    page?: number;
    pageSize?: number;
}

export const getCatalogs = async (xQuery: CatalogQuery): Promise<ResultModel<ListResultModel<Product>>> => {
    const res =  await http.get<ResultModel<ListResultModel<Product>>>(`/searchservice/api/v1/search/catalogs?${xQuery.q ? `q=${xQuery.q}` : ""}${xQuery.catalogTypeId ? `&catalogTypeId=${xQuery.catalogTypeId}` : ""}${xQuery.catalogBrandId ? `&catalogBrandId=${xQuery.catalogBrandId}` : ""}${xQuery.page ? `&Page=${xQuery.page}` : ""}${xQuery.pageSize ? `&PageSize=${xQuery.pageSize}` : ""}`, {
        headers: {
            "Content-Type": "application/json",
        }
    });
    return res.data;
}

export const getBrands = async (xQuery: XQuery): Promise<ResultModel<ListResultModel<CatalogBrand>>> => {
    const res =  await http.get<ResultModel<ListResultModel<CatalogBrand>>>("/catalogservice/api/v1/catalogs/brands", {
        headers: {
            "Content-Type": "application/json",
            "x-query": JSON.stringify(xQuery),
        }
    });
    return res.data;
}
export const getTypes = async (xQuery: XQuery): Promise<ResultModel<ListResultModel<CatalogType>>> => {
    const res =  await http.get<ResultModel<ListResultModel<CatalogType>>>("/catalogservice/api/v1/catalogs/types", {
        headers: {
            "Content-Type": "application/json",
            "x-query": JSON.stringify(xQuery),
        }
    });
    return res.data;
}

export const getCatalogDetail = async (id: string): Promise<ResultModel<Catalog>> => {
    const res =  await http.get<ResultModel<Catalog>>(`/catalogservice/api/v1/catalogs/${id}`);
    return res.data;
}