'use client'
import {Button, Form, Input} from "antd";
import { FaGoogle } from "react-icons/fa";
import {signIn, useSession} from "next-auth/react";
import {useEffect} from "react";
import { useRouter } from "next/navigation";

const AuthPage = () => {
    const {status} = useSession()
    const router = useRouter()
    useEffect(() => {
        if (status === "authenticated") router.back()
        console.log(status)
    }, [status]);
    return <div className={"w-1/5 mx-auto flex justify-center items-center"}>
        <Button className={"flex justify-center items-center gap-2"} onClick={() => signIn('google', {
            callbackUrl: "/",
        })}><FaGoogle />Login with google</Button>
    </div>
}

export default AuthPage;
