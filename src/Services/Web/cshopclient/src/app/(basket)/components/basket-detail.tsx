import useGetProductDetail from "@/app/(home)/hooks/useGetProductDetail";
import {Button, Card, Image, Spin } from "antd";
import { MdDeleteForever } from "react-icons/md";
import useRemoveBasketItem from "../hooks/useRemoveBasketItem";
import { BasketItem } from "@/models/basket-item";
export type BasketDetailProps = {
    basketItem: BasketItem,
    refetch: any
}

export const BasketDetail = (props: BasketDetailProps) => {
    const {data, isLoading} = useGetProductDetail(props.basketItem?.productId);
    const {mutate, isPending} = useRemoveBasketItem()
    return <Card>
        <div className={"flex flex-row gap-5 justify-between"}>
            <div className={"flex flex-row gap-5"}>
                <Image src={data?.data?.imageUrl} alt={data?.data?.name} width={100}/>
                <div>
                    {data?.data.name}
                </div>
                <div>x {props?.basketItem?.quantity}</div>

            </div>
            <Button className={"mr-0"} onClick={() => {
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