'use client'

import { Button } from "antd";
import { signIn } from "next-auth/react";
import { CiLogin } from "react-icons/ci";

export default function SignInPage() {
    return (
        <Button color={"danger"} onClick={() => {
            signIn('oidc', {
                callbackUrl: "/",
            })
        }}>
            <CiLogin size={25} />
        </Button>
    );
}