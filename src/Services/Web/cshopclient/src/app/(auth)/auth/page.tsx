'use client'
import {Button, Form, Input} from "antd";
import { FaGoogle } from "react-icons/fa";
import {signIn} from "next-auth/react";

const AuthPage = () => {
    return <div className={"w-1/5 mx-auto flex justify-center items-center"}>
        <Button className={"flex justify-center items-center gap-2"} onClick={() => signIn('google', {
            callbackUrl: "/",
        })}><FaGoogle />Login with google</Button>
    </div>
}

export default AuthPage;
