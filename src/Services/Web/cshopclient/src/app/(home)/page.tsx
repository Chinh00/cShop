'use client';
import { ProductCard } from "@/components/card/product-card";
import { useGetProducts } from "./hooks/useGetProducts";
import {useCallback, useEffect, useState} from "react";
import {Card, Divider, Image, Pagination, Spin} from "antd";
import { useRouter, useSearchParams } from "next/navigation";
import PriceFormat from "@/utils/price-format";
import Meta from "antd/es/card/Meta";
import {FilterModel, XQuery} from "@/utils/xQuery";
import {debounce} from "lodash";
import Search from "antd/es/input/Search";
import {useGetTypes} from "@/app/(home)/hooks/useGetTypes";
import {useGetBrands} from "@/app/(home)/hooks/useGetBrands";
export default function Home() {

    const [query, setQuery] = useState<XQuery>({
        includes: ["Pictures"],
        page: 1,
        pageSize: 10,
    })
    
    const {data, isLoading, refetch} = useGetProducts(query);
    
    const {data: types} = useGetTypes({})
    const {data: brands} = useGetBrands({})
    
    const router = useRouter();
    const [brandSelected, setBrandSelected] = useState<string>()
    const [typeSelected, setTypeSelected] = useState<string>()
    useEffect(() => {
        if (brandSelected) {
            setQuery(prevState => {
                return {
                    ...prevState,
                    filters: prevState.filters ? [...prevState.filters.filter(c => c.field !== "CatalogBrandId"), {
                        field: "CatalogBrandId",
                        operator: "==",
                        value: brandSelected
                    } as FilterModel] : [{
                        field: "CatalogBrandId",
                        operator: "==",
                        value: brandSelected
                    } as FilterModel]
                }
            })
        }
    }, [brandSelected]);

    useEffect(() => {
        if (typeSelected) {
            setQuery(prevState => {
                return {
                    ...prevState,
                    filters: prevState.filters ? [...prevState.filters.filter(c => c.field !== "CatalogTypeId"), {
                        field: "CatalogTypeId",
                        operator: "==",
                        value: typeSelected
                    } as FilterModel] : [{
                        field: "CatalogTypeId",
                        operator: "==",
                        value: typeSelected
                    } as FilterModel]
                }
            })
        }
    }, [typeSelected]);
    
  return (
      <div className={"grid grid-cols-5 gap-5 p-5"}>
          <div className={"col-span-1 flex flex-col gap-5"}>
              <Search title={"Tìm kiếm sản phẩm"} onSearch={value => {
                  setQuery(prevState => ({
                      ...prevState,
                      filters: prevState.filters ? [...prevState.filters, {
                          field: "name",
                          operator: "Contains",
                          value: value
                      } as FilterModel] : [{
                          field: "name",
                          operator: "Contains",
                          value: value
                      } as FilterModel],
                  }))
              }} />
              <Card>
                  <div>Catalog brands</div>
                  <Divider />
                  <div className={"flex flex-col gap-3"}>
                      
                      {!!brands && brands?.data?.items?.map((item, index) => (
                          <div onClick={() => {
                              setBrandSelected(item?.id)
                          }} key={item?.id} className={`cursor-pointer hover:scale-105 transition ${brandSelected === item?.id && "scale-105 font-bold"}`}>{item?.brandName}</div>
                      ))}
                  </div>
              </Card>
              <Card>
                  <div>Catalog types</div>
                  <Divider />
                  <div className={"flex flex-col gap-3"}>

                      {!!types && types?.data?.items?.map((item, index) => (
                          <div onClick={() => {
                              setTypeSelected(item?.id)
                          }} key={item?.id} className={`cursor-pointer hover:scale-105 transition ${typeSelected === item?.id && "scale-105 font-bold"}`}>{item?.name}</div>
                      ))}
                  </div>
              </Card>
          </div>
          <div className={"p-10 col-span-4 grid grid-cols-4 gap-10 w-full"}>
              {isLoading && <div className={"w-full flex justify-content-center"}><Spin size="large"/></div>}
              {!!data?.data && data?.data?.items.map((item, id) =>
                  (<Card
                      hoverable
                      style={{ width: 240 }}
                      cover={<Image style={{height: 250, objectFit: "contain"}}  src={item?.pictures[0]?.pictureUrl} alt=""/>}
                      onClick={() => {
                      router.push(`/${item.id}`);
                  }} key={id} className={"w-[200px] text-center cursor-pointer flex flex-col items-center h-full"}>
                      
                      
                      <Meta title={<div className={"text-wrap"}>{item.name}</div>}  description={<div className={"font-bold text-red-500"}>
                          {PriceFormat.ConvertVND(item?.price ?? 0)}
                      </div>} />
                  </Card>))}
              <div className={"col-span-4 flex justify-center content-center w-full"}>
                  {!!data?.data && <Pagination defaultCurrent={1} current={Math.floor((data?.data?.page ?? 1) / (data?.data?.pageSize ?? 1))} total={Math.floor(data?.data?.total ?? 1 / (data?.data?.pageSize ?? 1))} onChange={(e) => {
                      setQuery( prev => ( {
                          ...prev,
                          page: e * (prev.pageSize ?? 1),
                      }))
                      console.log(Math.floor(data?.data?.total / data?.data?.pageSize))
                  }} />}
              </div>
          </div>
          
      </div>
  );
}
