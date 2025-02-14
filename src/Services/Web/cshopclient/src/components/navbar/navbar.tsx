'use client'
import { useSession } from "next-auth/react";
import Link from "next/link";
import { useEffect } from "react";
import { RiShoppingBasketLine } from "react-icons/ri";
import { LogoutButton } from "../button/logout-button";
import {Dropdown, MenuProps, Space} from "antd";
import {DownOutlined, SmileOutlined} from "@ant-design/icons";
import useCreateBasket from "@/app/(basket)/hooks/useCreateBasket";
import LoginButton from "@/components/button/login-button";
const items: MenuProps['items'] = [
    {
        key: '1',
        label: (
            <LogoutButton />
        ),
    },
    {
        key: '2',
        label: (
            <Link href={"/myorders"}>
                My order
            </Link>
        ),
        icon: <SmileOutlined size={25} />,
    },
    {
        key: '3',
        label: (
            <Link href={"/basket"}>Basket</Link>
        ),
        icon: <RiShoppingBasketLine size={25} />
    },
];
export const Navbar = () =>  {
    const {mutate} = useCreateBasket()
    const session = useSession();
    useEffect(() => {
        if (session.status === "authenticated") mutate()
    }, [session]);
   
    return <div className={"p-5 flex justify-between align-items-center w-full pl-10 pr-10"}>
        <div>
            <Link href={"/"}>Home</Link>
        </div>    
        <div className={"w-max flex flex-row gap-5 justify-center justify-items-center"}>
            
            {session.status == "unauthenticated" ? <LoginButton /> : 
                <>
                    <Dropdown menu={{ items }}>
                        <a onClick={(e) => e.preventDefault()}>
                            <Space>
                                {session.data?.user.name}
                                <DownOutlined />
                            </Space>
                        </a>
                    </Dropdown>
                </>
            }
        </div>
    </div>
}