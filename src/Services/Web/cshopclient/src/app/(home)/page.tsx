'use client';
import { ProductCard } from "@/components/card/product-card";
import { useGetProducts } from "./hooks/useGetProducts";
import {useEffect, useState} from "react";
import {Card, Image, Pagination, Spin} from "antd";
import { useRouter } from "next/navigation";
import PriceFormat from "@/utils/price-format";
import Meta from "antd/es/card/Meta";
import {XQuery} from "@/utils/xQuery";

export default function Home() {

    const [query, setQuery] = useState<XQuery>({
        includes: ["Pictures"],
        page: 1,
        pageSize: 10,
    })
    const {data, isLoading} = useGetProducts(query);
    const router = useRouter();
    
    
  return (
      <div className={"grid grid-cols-5 gap-5 p-5"}>
          <div className={"col-span-1"}>
              left search 
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
