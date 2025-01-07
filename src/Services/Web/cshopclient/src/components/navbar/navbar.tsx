import SignInPage from "@/app/api/auth/singin/page";
import Link from "next/link";

const Routes = [
    "",
    "/basket"
]
export const Navbar = () =>  {
    return <div className={"p-5 flex justify-content-center align-items-center gap-10"}>
        <Link href={"/"}>Home</Link>
        <Link href={"/basket"}>Basket</Link>
        <SignInPage />
    </div>
}