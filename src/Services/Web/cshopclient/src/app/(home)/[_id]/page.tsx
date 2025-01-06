'use client'
import { GetServerSideProps } from "next";
import { useParams } from "next/navigation";
import { useRouter } from "next/router";
import { useEffect } from "react";
import useGetProductDetail from "../hooks/useGetProductDetail";

import {Image, Spin} from "antd"
interface ProductDetailProps {
    _id: string;
}

const ProductDetail = () => {
    const params = useParams();
    const {data, isLoading} = useGetProductDetail(params._id as string);
    return <div className={"p-10 grid grid-cols-5 gap-10"}>
        {isLoading ? <div className={"flex justify-content-center col-span-5 p-5"}><Spin size={"large"}/></div> :
            <>
                <div className={"col-span-3 flex items-center justify-center flex-col gap-5"}>
                    <div className={"text-[40px] font-bold"}>{data?.data?.name}</div>
                    <Image src={data?.data?.imageUrl} alt=""/>
                </div>
                <div className={"col-span-2 flex flex-col"}>
                    <div>Loại: <span>{data?.data?.catalogType?.name}</span></div>
                </div>
            </>}

    </div>
}

export default ProductDetail;