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
        if (isSuccess && data?.data == undefined) {
            mutate()
        }
    }, []);
    
    const PayAction = () => {
        const axios_instance = axios.create({
            baseURL: ""
        })
        
        fetch("https://sandbox.vnpayment.vn/paymentv2/vpcpay.html", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify({
                vnp_TmnCode: "48FVW9FU",
                vnp_HashSecret: "LFIQ8YSXU6XQO92AGALYDQ2ATK9Q4W34",
                vnp_Version: "2.1.0",
                vnp_Command: "pay",
                vnp_Amount: 1000000 * 100,
                vnp_CreateDate: 20220101103111,
                vnp_CurrCode: "VND",
                vnp_Locale: "vn",
                vnp_OrderInfo: "Thanh toan don hang",
                vnp_OrderType: "other",
                vnp_ReturnUrl: "http://localhost:3000",
                vnp_ExpireDate: 20220101103111,
                vnp_IpAddr: "192.168.1.1",
                vnp_TxnRef: "123431243"
            }),
        },).then(res => {
            console.log("res", res)
        });
        
        
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