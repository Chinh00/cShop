import { NextResponse } from 'next/server'
import type { NextRequest } from 'next/server'

const privatePaths: string[] = [
    '/basket/:path*', "/myorders/:path*",
]
const authPaths: string[] = ["/api/auth/signin"]
const noAuthPath = []

export function middleware(request: NextRequest) {
    const { pathname } = request.nextUrl;
    const token =
        request.cookies.get("next-auth.session-token")?.value 
    
    
    if (!token) {
        console.log("Pathname", pathname)
        if (privatePaths.filter(c => c.match(pathname))) {
            // return NextResponse.redirect(new URL("/api/auth/signin", request.url))
        }
    }

    
    if (token && authPaths.filter(c => c.match(pathname))) {
        // return NextResponse.redirect(new URL("/", request.url));
    }


    // return NextResponse.next();
}

export const config = {
    matcher: [...privatePaths, ...authPaths],
}