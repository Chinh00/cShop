'use client'
import { useParams } from "next/navigation";
import {  toast } from 'react-toastify';
import useGetProductDetail from "../hooks/useGetProductDetail";

import {Button, Image, Spin} from "antd"
import useAddBasketItem from "@/app/(basket)/hooks/useAddBasketItem";
interface ProductDetailProps {
    _id: string;
}

const ProductDetail = () => {
    const params = useParams();
    const {data, isLoading} = useGetProductDetail(params._id as string);
    const {mutate, isPending} = useAddBasketItem({productId: ""})
    return <div className={"p-10 grid grid-cols-5 gap-10"}>
        {isLoading ? <div className={"flex justify-content-center col-span-5 p-5"}><Spin size={"large"}/></div> :
            <>
                <div className={"col-span-3 flex items-center justify-center flex-col gap-5"}>
                    <div className={"text-[40px] font-bold"}>{data?.data?.name}</div>
                    <Image src={data?.data?.imageUrl} alt=""/>
                </div>
                <div className={"col-span-2 flex flex-col gap-5"}>
                    <div>Loại: <span className={"font-bold"}>{data?.data?.catalogType?.name}</span></div>
                    <div>Hãng sản xuất: <span className={"font-bold"}>{data?.data?.catalogBrand?.brandName}</span></div>
                    <Button type={"dashed"} className={"w-min"} onClick={() => {
                        mutate({
                            productId: data?.data?.id!
                        }, {
                            onSuccess: () => {
                                toast("Thêm vào giỏ hàng")
                            }
                        })
                    }}>Thêm vào giỏ hàng {isPending && <Spin />}</Button>
                </div>
            </>}

    </div>
}

export default ProductDetail;