'use client';
import { ProductCard } from "@/components/card/product-card";
import { useGetProducts } from "./hooks/useGetProducts";
import {useEffect} from "react";
import {Image} from "antd";

export default function Home() {
    const {data, isLoading} = useGetProducts();
  return (
      <div className={"grid grid-cols-5 gap-5 p-5"}>
          <div className={"col-span-1"}>
              left search 
          </div>
          <div className={"p-10 col-span-4 grid grid-cols-4 gap-10 w-full"}>
              {!!data && data?.data?.items.map((item, id) =>
                  (<div key={id} className={"w-[200px] text-center"}>
                      <Image src={item.imageUrl} alt=""/>
                      {item.name}
                  </div>))}
          </div>
      </div>
  );
}
