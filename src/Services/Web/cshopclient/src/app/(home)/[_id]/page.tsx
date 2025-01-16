'use client'
import { useParams } from "next/navigation";
import {  toast } from 'react-toastify';
import useGetProductDetail from "../hooks/useGetProductDetail";
import { FaLongArrowAltRight } from "react-icons/fa";
import {Button, Image, Spin} from "antd"
import useAddBasketItem from "@/app/(basket)/hooks/useAddBasketItem";
import { CiDroplet } from "react-icons/ci";
import { CiSignpostR1 } from "react-icons/ci";
import { Swiper, SwiperSlide } from 'swiper/react';
import { Navigation } from 'swiper/modules';

import 'swiper/css';
import 'swiper/css/navigation';
import Link from "next/link";

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
                    
                    <Swiper centeredSlides={true} navigation={true} modules={[Navigation]} className="mySwiper" style={{width:"500px"}}>
                        
                        {!!data?.data && data?.data?.pictures?.map((item, index) => (
                            <SwiperSlide key={index}><Image style={{objectFit: "cover"}} src={item?.pictureUrl} alt="" /></SwiperSlide>
                        ))} 
                    </Swiper>
                </div>
                <div className={"col-span-2 flex flex-col gap-5"}>
                    <div className={"p-5 bg-green-500 bg-opacity-30 backdrop-blur rounded-lg flex flex-col gap-5"}>
                        <div>Loại: <span className={"font-bold"}>{data?.data?.catalogType?.name}</span></div>
                        <div>Hãng sản xuất: <span className={"font-bold"}>{data?.data?.catalogBrand?.brandName}</span>
                        </div>
                        <Button type={"dashed"} className={"w-min"} onClick={() => {
                            mutate({
                                productId: data?.data?.id!
                            }, {
                                onSuccess: () => {
                                    toast(<div>Add basket success <FaLongArrowAltRight size={20} /> <Link href={"/basket"} >Go to my basket</Link></div>, {
                                        autoClose: 1500
                                    })
                                }
                            })
                        }}>Thêm vào giỏ hàng {isPending && <Spin/>}</Button>
                    </div>
                    <div
                        className={"flex flex-col gap-3 border-2 bg-red-200 backdrop-blur bg-opacity-70 p-5 rounded-lg"}>{data?.data?.description.split("|").map((item, id) => {
                        return <div key={id}
                                    className={`${id === 0 && "font-bold text-xl"} flex flex-row gap-3 justify-start items-start w-full`}> {id !== 0 &&
                            <CiSignpostR1 size={30}/>} {item}</div>
                    })}</div>
                </div>
            </>}
        
    </div>
}

export default ProductDetail;