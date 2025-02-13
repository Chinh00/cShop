'use client'
import { Button } from "antd"
import { useRouter } from "next/navigation";
import { IoMdLogIn } from "react-icons/io";
const LoginButton = () => {
    const router = useRouter()
    return <Button onClick={() => router.push("/auth", {})}><IoMdLogIn size={25} color={"red"} /></Button>
}
export default LoginButton;