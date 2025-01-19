// middleware.js  
import { NextResponse } from 'next/server';
import { NextRequest } from 'next/server';
import {useSession} from "next-auth/react";
import {getServerSession} from "next-auth";
import {authOptions} from "@/app/api/auth/[...nextauth]/route";

export async function middleware(request: NextRequest) {
    const session = await getServerSession(authOptions);

    

    // Trả về Response gốc nếu không có gì thay đổi  
    return NextResponse.next();
}
export const config = {
    matcher: ['/basket/:path*', '/profile/:path*'], 
};  