import { Button } from "antd"
import { signOut } from "next-auth/react";
import { IoIosLogOut } from "react-icons/io";
export const LogoutButton = () => {
    return <Button onClick={() => {
        signOut({
            redirect: true, 
            callbackUrl: "/", 
        });
    }}>
        <IoIosLogOut size={20} />
    </Button>
}