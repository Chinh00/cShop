'use client'

import {memo, useEffect, useState} from "react";
import BasketDetail from "../components/basket-detail";
import useGetBasketDetail from "../hooks/useGetBasketDetail";
import useCreateBasket from "../hooks/useCreateBasket";
import Link from "next/link";
import PriceFormat from "@/utils/price-format";
import ClientComponentAuth from "@/components/auth/client_component_auth";
import {Spin} from "antd";
import { useRecoilState } from "recoil";
import {cartState} from "@/app/(basket)/interface";
const BasketPage = () => {
    const {data, isLoading, refetch} = useGetBasketDetail();
    const [amount, setAmount] = useState(0)

    
    
    return <ClientComponentAuth >
        <div className={"w-full"}>

            {isLoading ? <Spin /> : <div className={"flex flex-col justify-center w-2/3 mx-auto p-10 gap-5"}>
                <div className={"flex justify-between justify-items-center flex-row w-full"}>
                    <Link href={"/order"} className={"max-w-max p-2 rounded-lg hover:scale-105 hover:backdrop-blur hover:bg-blue-200 transition border-2"}>Thanh toán</Link>
                    <div>Amount: {PriceFormat.ConvertVND(amount)}</div>
                </div>

                {!!data && data?.data?.basketItems?.map(e => {
                    return <BasketDetail key={e.id} basketItem={e} refetch={refetch} setAmount={setAmount}/>
                })}
            </div> }
    
        </div>
    </ClientComponentAuth>
}
export default BasketPage;