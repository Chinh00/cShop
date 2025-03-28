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
import {CatalogQuery} from "@/app/(home)/services/home.service";
export default function Home() {

    const [query, setQuery] = useState<CatalogQuery>({
        
    })
    
    const {data, isLoading, refetch} = useGetProducts(query);
    
    const {data: types} = useGetTypes({})
    const {data: brands} = useGetBrands({})
    
    const router = useRouter();
    const [brandSelected, setBrandSelected] = useState<string>()
    const [typeSelected, setTypeSelected] = useState<string>()
    const [inputSearch, setInputSearch] = useState<string>()
    useEffect(() => {
        if (brandSelected) {
            setQuery(prevState => {
                return {
                    ...prevState,
                    catalogBrandId: brandSelected
                }
            })
        }
    }, [brandSelected]);

    useEffect(() => {
        if (typeSelected) {
            setQuery(prevState => {
                return {
                    ...prevState,
                   catalogTypeId: typeSelected
                }
            })
        }
    }, [typeSelected]);
    useEffect(() => {
        if (inputSearch) {
            setQuery(prevState => ({
                ...prevState,
                q: inputSearch
            }))
        } 
    }, [inputSearch]);
  return (
      <div className={"grid grid-cols-5 gap-5 p-5"}>
          <div className={"col-span-1 flex flex-col gap-5"}>
              <Search title={"Tìm kiếm sản phẩm"} onSearch={value => {
                  setInputSearch(value)
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
                      cover={<Image style={{height: 250, objectFit: "contain"}}  src={item?.pictures[0]} alt=""/>}
                      onClick={() => {
                      router.push(`/${item.id}`);
                  }} key={id} className={"w-[200px] text-center cursor-pointer flex flex-col items-center h-full"}>
                      
                      
                      <Meta title={<div className={"text-wrap"}>{item.catalogName}</div>} description={<div className={"font-bold text-red-500"}>
                          {PriceFormat.ConvertVND(item?.price ?? 0)}
                      </div>} />
                  </Card>))}
              <div className={"col-span-4 flex justify-center content-center w-full"}>
                  {!!data?.data && <Pagination defaultCurrent={1} current={data?.data?.page} total={Math.floor(data?.data?.total ?? 1 / (data?.data?.pageSize ?? 1))} onChange={(e) => {
                      setQuery( prev => ( {
                          ...prev,
                          page: e,
                          pageSize: 10
                      }))
                  }} />}
              </div>
          </div>
          
      </div>
  );
}
