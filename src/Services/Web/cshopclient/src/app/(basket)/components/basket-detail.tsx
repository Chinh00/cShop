import useGetProductDetail from "@/app/(home)/hooks/useGetProductDetail";
import {Button, Card, Image, Spin } from "antd";
import { MdDeleteForever } from "react-icons/md";
import useRemoveBasketItem from "../hooks/useRemoveBasketItem";
import { useState } from "react";
export type BasketDetailProps = {
    productId: string,
}

export const BasketDetail = (props: BasketDetailProps) => {
    const {data, isLoading} = useGetProductDetail(props.productId);
    const {mutate, isPending} = useRemoveBasketItem()
    return <Card>
        <div className={"flex flex-row gap-5 justify-between"}>
            <div className={"fle flex-row gap-5"}>
                <Image src={data?.data?.imageUrl} alt={data?.data?.name} width={100}/>
                <div>
                    {data?.data.name}
                </div>
            </div>
            <Button className={"mr-0"} onClick={() => {
                mutate(data?.data?.id!);
            }}>
                {!isPending ? <MdDeleteForever size={20} color={"red"} /> : <Spin /> }
            </Button>
        </div>
    </Card>
}

export default BasketDetail;