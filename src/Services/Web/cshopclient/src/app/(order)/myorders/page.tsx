'use client'
import UseGetOrders from "@/app/(order)/hooks/useGetOrders";
import {XQuery} from "@/utils/xQuery";
import { Tabs, TabsProps } from "antd";
import { useState } from "react";
import {OrderPreview} from "@/app/(order)/components/order-preview";

const MyOrders = () => {
    const [query, setQuery] = useState<XQuery>({
        includes: ["OrderDetails"],
        sorts: ["CreatedDate.Desc"]
    })
    const {data, isLoading} = UseGetOrders(query)
    console.log(data?.data)
    const onChange = (key: string) => {
        console.log(key);
    };
    const items: TabsProps['items'] = [
        {
            key: '1',
            label: 'Chưa thanh toán',
            children: <div className={"flex flex-col gap-5 p-5"}>
                {!!data?.data && data?.data?.items?.filter(c => c.orderStatus == 0).map(item => (
                    <OrderPreview key={item.id} order={item} />
                ))}
                
            </div>,
        },
        {
            key: '2',
            label: 'Đã thanh toán',
            children: <div className={"flex flex-col gap-5 p-5"}>
                {!!data?.data && data?.data?.items?.filter(c => c.orderStatus == 1).map(item => (
                    <OrderPreview key={item.id} order={item} />
                ))}

            </div>,
        },
        {
            key: '3',
            label: 'Đang vận chuyển',
            children: '',
        },
        {
            key: '4',
            label: 'Đã giao',
            children: '',
        },
    ];
    
    
    return <div className={"p-10 flex justify-center items-center"}>
        <Tabs
            onChange={onChange}
            type="card"
            className={"w-4/5"}
            items={items}
        />
    
    </div>
}

export default MyOrders;