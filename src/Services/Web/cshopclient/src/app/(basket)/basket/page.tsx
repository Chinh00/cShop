'use client'

import { useEffect } from "react";
import BasketDetail from "../components/basket-detail";
import useGetBasketDetail from "../hooks/useGetBasketDetail";
import useCreateBasket from "../hooks/useCreateBasket";
import { Button } from "antd";
import { useRouter } from "next/router";
import Link from "next/link";
import axios from "axios";

const BasketPage = () => {
    const {data, isLoading, isSuccess, refetch} = useGetBasketDetail();
    const {mutate} = useCreateBasket()
    
    useEffect(() => {
        if (isSuccess && data?.data == null) {
            mutate()
        }
    }, [data]);
    
    const PayAction = () => {
        const axios_instance = axios.create({
            baseURL: ""
        })
        
        
        
         
    }
    
    return <div className={"w-full"}>
        
        <div className={"flex flex-col justify-center w-2/3 mx-auto p-10 gap-5"}>
            {/*<Link href={"/order"} className={"w-max border-2 p-2 rounded-lg"} color={"danger"} >Đặt hàng</Link>*/}
            <Button onClick={PayAction}>Pay </Button>
            {!!data && data?.data?.basketItems?.map(e => {
                return <BasketDetail key={e.id} basketItem={e} refetch={refetch}/>
            })}
        </div>
        
    </div>
}
export default BasketPage;