'use client'
import useGetBasketDetail from "@/app/(basket)/hooks/useGetBasketDetail";

const OrderPage = () => {
    const {data, isLoading, isSuccess, refetch} = useGetBasketDetail();
    return <>Order</>
}

export default OrderPage;