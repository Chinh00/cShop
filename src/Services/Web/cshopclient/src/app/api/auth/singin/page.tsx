'use client'
import { Button } from "antd";
import { signIn } from "next-auth/react";

export default function SignInPage() {
    return (
        <button onClick={() => signIn('oidc', {
            callbackUrl: "/hehe",
        })}>
            Login
        </button>
    );
}