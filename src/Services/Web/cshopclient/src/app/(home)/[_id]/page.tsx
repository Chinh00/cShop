'use client'
import {useParams, useRouter} from "next/navigation";
import {  toast } from 'react-toastify';
import useGetProductDetail from "../hooks/useGetProductDetail";
import { FaLongArrowAltRight } from "react-icons/fa";
import {Button, Card, Image, Spin} from "antd"
import useAddBasketItem from "@/app/(basket)/hooks/useAddBasketItem";
import { CiSignpostR1 } from "react-icons/ci";
import { Swiper, SwiperSlide } from 'swiper/react';
import { Navigation } from 'swiper/modules';

import 'swiper/css';
import 'swiper/css/navigation';
import Link from "next/link";
import {useSession} from "next-auth/react";
import {Comment} from "@/app/(home)/components/comment";

interface ProductDetailProps {
    _id: string;
}

const ProductDetail = () => {
    const params = useParams();
    const {data, isLoading} = useGetProductDetail(params._id as string);
    const {mutate, isPending} = useAddBasketItem({productId: ""})
    const route = useRouter()
    const {status} = useSession()
    
    return <div className={"p-10 grid grid-cols-5 gap-10 h-full"}>
        {isLoading ? <div className={"flex justify-content-center col-span-5 p-5 h-full"}><Spin size={"large"}/></div> :
            <>
                <div className={"h-full col-span-3 flex justify-center"}>
                    <div className={"flex items-center justify-center flex-col gap-5 fixed top-100 content-center"}>
                        <div className={"text-[40px] font-bold max-w-[900px]"}>{data?.data?.name}</div>

                        <Card>
                            <Swiper centeredSlides={true} navigation={true} modules={[Navigation]} className="mySwiper" style={{width:"500px"}}>

                                {!!data?.data && data?.data?.pictures?.map((item, index) => (
                                    <SwiperSlide key={index}><Image style={{objectFit: "cover"}} src={item?.pictureUrl} alt="" /></SwiperSlide>
                                ))}
                            </Swiper>
                        </Card>
                    </div>
                </div>
                <div className={"col-span-2 flex flex-col gap-5"}>
                    <div className={"p-5 bg-green-500 bg-opacity-30 backdrop-blur rounded-lg flex flex-col gap-5"}>
                        <div>Loại: <span className={"font-bold"}>{data?.data?.catalogType?.name}</span></div>
                        <div>Hãng sản xuất: <span className={"font-bold"}>{data?.data?.catalogBrand?.brandName}</span>
                        </div>
                        <Button type={"dashed"} className={"w-min"} onClick={() => {
                            if (status === "authenticated") {
                                mutate({
                                    productId: data?.data?.id!
                                }, {
                                    onSuccess: () => {
                                        toast(<div>Add to my cart success</div>,{
                                            autoClose: 1500
                                        })
                                    }
                                })
                            } else {
                                route.push("/auth")
                            }
                            
                            
                        }}>Add to cart {isPending && <Spin/>}</Button>
                    </div>
                    <div className={"flex flex-col gap-3 border-2 bg-red-200 backdrop-blur bg-opacity-70 p-5 rounded-lg"}>{data?.data?.description.split("|").map((item, id) => {
                        return <div key={id}
                                    className={`${id === 0 && "font-bold text-xl"} flex flex-row gap-3 justify-start items-start w-full`}> {id !== 0 &&
                            <CiSignpostR1 size={30}/>} {item}</div>
                    })}</div>
                    <Comment productId={data?.data?.id!} />
                </div>
            </>}
        
    </div>
}

export default ProductDetail;