'use client'
import useGetBasketDetail from "@/app/(basket)/hooks/useGetBasketDetail";
import useCreateOrder from "../hooks/useCreateOrder";
import {useEffect, useState} from "react";
import {OrderCreate, OrderItemCreate} from "@/app/(order)/models/order-create";
import BasketDetail from "@/app/(basket)/components/basket-detail";
import {Button, Spin } from "antd";
import Meta from "antd/es/card/Meta";
import PriceFormat from "@/utils/price-format";
import {toast} from "react-toastify";
import useGetPaymentUrl from "@/app/(order)/hooks/useGetPaymentUrl";
import ClientComponentAuth from "@/components/auth/client_component_auth";

const OrderPage = () => {
    const {data, isLoading, isSuccess, refetch} = useGetBasketDetail();
    const {mutate} = useCreateOrder()
    const [orderState, setOrderState] = useState<OrderCreate | undefined>(undefined)
    const [amount, setAmount] = useState(0)
    const {mutate: paymentMutate, isPending: paymentLoading} = useGetPaymentUrl()
    useEffect(() => {
        if (data) {
            setOrderState(pre => ({
                ...pre,
                items: !!data?.data ? data?.data?.basketItems?.map((c) => {
                    return {
                        productId: c.productId,
                        quantity: c.quantity,
                    } as OrderItemCreate;
                }): [],
                orderDate: new Date(),
            }))
        }
    }, [])

    useEffect(() => {
        return () => {
            setAmount(0)
        }
    }, []);
    return <ClientComponentAuth>
        <div className={"p-10 flex justify-center items-center"}>
            {isLoading ? <Spin size={"large"} /> : <div className={"w-full flex flex-row"}>
                <div className={"flex flex-col gap-5 w-2/3"}>
                    <div className={"border-2 p-3 rounded-lg flex flex-row justify-between items-center"}>
                        <Meta title={"Order detail"}/>
                        <div>Amount: {PriceFormat.ConvertVND(amount)}</div>
                    </div>
                    {!!data && data?.data?.basketItems?.map(e => {
                        return <BasketDetail key={e.id} basketItem={e} refetch={refetch} setAmount={setAmount}/>
                    })}
                </div>
                <div className={"w-1/3 pl-10 pr-10 h-full"}>
                    <Button onClick={() => {
                        orderState &&
                        mutate(orderState, {
                            onSuccess: (data) => {
                                toast("Tạo đơn hàng thành công")
                                console.log(data)
                                paymentMutate(data?.data?.id, {
                                    onSuccess: (data) => {
                                        console.log(data)
                                        window.location.href = data?.data
                                    }
                                })
                            }
                        })
                    }} type={"dashed"} className={"w-full"} size={"large"} color={"danger"}>Confirm order {paymentLoading && <Spin size={"default"} />}</Button>

                </div>
            </div>}
        </div>
    </ClientComponentAuth>
}

export default OrderPage;