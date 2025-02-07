import useGetProductDetail from "@/app/(home)/hooks/useGetProductDetail";
import {Button, Card, Image, Spin } from "antd";
import { MdDeleteForever } from "react-icons/md";
import useRemoveBasketItem from "../hooks/useRemoveBasketItem";
import { BasketItem } from "@/models/basket-item";
import {memo, useEffect} from "react";
import PriceFormat from "@/utils/price-format";
import Link from "next/link";
export type BasketDetailProps = {
    basketItem: BasketItem,
    refetch: any,
    setAmount: any
}

export const BasketDetail = (props: BasketDetailProps) => {
    const {data, isLoading, isSuccess} = useGetProductDetail(props.basketItem?.productId);
    const {mutate, isPending} = useRemoveBasketItem()

    useEffect(() => {
        if (isSuccess && data?.data !== null) {
            props?.setAmount((pre: number) => pre + data?.data?.price * props?.basketItem?.quantity)
        }
    }, [isSuccess]);
    
    
    return <Card> 
        <div className={"flex flex-row gap-5 justify-between"}>
            <div className={"flex flex-row gap-5"}>
                <Image src={data?.data?.pictures[0]?.pictureUrl} alt={data?.data?.name} width={100}/>
                <div className={"flex flex-col gap-5"}>
                    <Link href={`/${data?.data?.id}`}>{data?.data.name}</Link>
                    <div>{PriceFormat.ConvertVND(data?.data?.price ?? 0)}</div>
                </div>
                <div>x {props?.basketItem?.quantity}</div>

            </div>
            <Button disabled={isPending} className={"mr-0"} onClick={() => {
                
                mutate(data?.data?.id!, {
                    onSuccess: () => {
                        props?.setAmount((pre: number) => pre + (data?.data?.price ?? 0) * props?.basketItem?.quantity * -1)
                        props.refetch()
                    }
                });
            }}>
                {!isPending ? <MdDeleteForever size={20} color={"red"} /> : <Spin /> }
            </Button>
        </div>
    </Card>
}

export default BasketDetail;