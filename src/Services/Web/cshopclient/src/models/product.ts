import { CatalogBrand } from "./catalogBrand";
import { CatalogType } from "./catalogType";
import {CatalogPicture} from "@/models/catalogPicture";

export type Product =  {
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

