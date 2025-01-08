'use client'
import SignInPage from "@/app/api/auth/singin/page";
import { AppState } from "@/stores/app.store";
import { useAtom } from "jotai";
import { useSession } from "next-auth/react";
import Link from "next/link";
import { useEffect } from "react";
import { RiShoppingBasketLine } from "react-icons/ri";
import { LogoutButton } from "../button/logout-button";

export const Navbar = () =>  {
    const [appState, setAppState] = useAtom(AppState)
    const session = useSession();
    useEffect(() => {
        if (session.data?.user?.access_token) {
            setAppState({isAuthenticated: true})
        }
    }, [session])
    return <div className={"p-5 flex justify-between align-items-center w-full pl-10 pr-10"}>
        <div>
            <Link href={"/"}>Home</Link>
        </div>    
        <div className={"w-max flex flex-row gap-5 justify-center justify-items-center"}>
            <Link href={"/basket"}><RiShoppingBasketLine size={25} /></Link>
            {!appState.isAuthenticated ? <SignInPage /> : <div className={"flex flex-row justify-center justify-items-center gap-4"}>{session.data?.user.name} <LogoutButton /></div>}
        </div>
    </div>
}