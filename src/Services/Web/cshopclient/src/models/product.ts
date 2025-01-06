import { CatalogBrand } from "./catalogBrand";
import { CatalogType } from "./catalogType";

export interface Product {
    id: string;
    name: string,
    price: number,
    imageUrl: string,
    isActive: boolean,
    catalogTypeId: string,
    catalogType?: CatalogType,
    catalogBrandId: string,
    catalogBrand?: CatalogBrand
}   