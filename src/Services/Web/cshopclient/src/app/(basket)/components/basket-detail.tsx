import useGetProductDetail from "@/app/(home)/hooks/useGetProductDetail";
import {Button, Card, Image, Spin } from "antd";
import { MdDeleteForever } from "react-icons/md";
import useRemoveBasketItem from "../hooks/useRemoveBasketItem";
import { BasketItem } from "@/models/basket-item";
import {useEffect} from "react";
import PriceFormat from "@/utils/price-format";
export type BasketDetailProps = {
    basketItem: BasketItem,
    refetch: any,
    setAmount: any
}

export const BasketDetail = (props: BasketDetailProps) => {
    const {data, isLoading} = useGetProductDetail(props.basketItem?.productId);
    const {mutate, isPending} = useRemoveBasketItem()
    
    useEffect(() => {
        if (!!data?.data) {
            props?.setAmount((pre: any) => pre + data?.data?.price)
        }
    }, [isLoading])
    return <Card>
        <div className={"flex flex-row gap-5 justify-between"}>
            <div className={"flex flex-row gap-5"}>
                <Image src={data?.data?.pictures[0]?.pictureUrl} alt={data?.data?.name} width={100}/>
                <div className={"flex flex-col gap-5"}>
                    <div>{data?.data.name}</div>
                    <div>{PriceFormat.ConvertVND(data?.data?.price ?? 0)}</div>
                </div>
                <div>x {props?.basketItem?.quantity}</div>

            </div>
            <Button className={"mr-0"} onClick={() => {
                props?.setAmount((pre: any) => pre - (data?.data?.price ?? 0) * props?.basketItem?.quantity)
                mutate(data?.data?.id!, {
                    onSuccess: () => {
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