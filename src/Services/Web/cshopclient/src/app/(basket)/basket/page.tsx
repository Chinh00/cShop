'use client'

import { useEffect } from "react";
import BasketDetail from "../components/basket-detail";
import useGetBasketDetail from "../hooks/useGetBasketDetail";
import useCreateBasket from "../hooks/useCreateBasket";

const BasketPage = () => {
    const {data, isLoading, isSuccess} = useGetBasketDetail();
    const {mutate} = useCreateBasket()
    useEffect(() => {
        if (isSuccess && data?.data == null) {
            mutate()
        }
    }, []);
    return <div className={"flex flex-col justify-center w-2/3 mx-auto p-10 gap-5"}>
        {!!data && data?.data?.basketItems?.map(e => {
            return <BasketDetail key={e.id} productId={e.productId} />
        })}
    </div>
}
export default BasketPage;