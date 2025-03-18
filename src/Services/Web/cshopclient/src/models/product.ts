import { CatalogBrand } from "./catalogBrand";
import { CatalogType } from "./catalogType";
import {CatalogPicture} from "@/models/catalogPicture";

export type Product =  {
    id: string;
    catalogName: string,
    price: number,
    imageUrl: string,
    isActive: boolean,
    catalogTypeId: string,
    catalogTypeName?: string,
    catalogBrandId: string,
    catalogBrandName?: string
    description: string,
    pictures: string[],
}



export type Catalog =  {
    id: string;
    name: string,
    price: number,
    imageUrl: string,
    isActive: boolean,
    catalogTypeId: string,
    catalogType?: CatalogType,
    catalogBrandId: string,
    catalogBrand?: CatalogBrand
    description: string,
    pictures: CatalogPicture[],
}   

