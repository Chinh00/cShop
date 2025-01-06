'use client';
import { ProductCard } from "@/components/card/product-card";
import { useGetProducts } from "./hooks/useGetProducts";
import {useEffect} from "react";
import {Card, Image, Spin} from "antd";
import { useRouter } from "next/navigation";

export default function Home() {
    const {data, isLoading} = useGetProducts();
    const router = useRouter();
  return (
      <div className={"grid grid-cols-5 gap-5 p-5"}>
          <div className={"col-span-1"}>
              left search 
          </div>
          <div className={"p-10 col-span-4 grid grid-cols-4 gap-10 w-full"}>
              {isLoading && <div className={"w-full flex justify-content-center"}><Spin size="large"/></div>}
              {!!data && data?.data?.items.map((item, id) =>
                  (<Card onClick={() => {
                      router.push(`/${item.id}`);
                  }} key={id} className={"w-[200px] text-center cursor-pointer"}>
                      <Image src={item.imageUrl} alt=""/>
                      {item.name}
                  </Card>))}
              
          </div>
      </div>
  );
}
