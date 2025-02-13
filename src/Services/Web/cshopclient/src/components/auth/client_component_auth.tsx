'use client'

import {useSession} from "next-auth/react";
import {useRouter} from "next/navigation";
import {ReactNode, useEffect} from "react";
import {Spin} from "antd";

const ClientComponentAuth =  ({children}: {children: ReactNode}) => {
    const {status} = useSession()

    const router = useRouter();

    useEffect(() => {
        if (status === "unauthenticated") {
            router.push("/auth"); 
        }
    }, [status, router]);
    return status == "loading" ? <Spin size={"large"} /> : <>{children}</> 
}

export default ClientComponentAuth
