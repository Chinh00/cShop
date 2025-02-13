
import NextAuth from "next-auth"
import GoogleProvider from "next-auth/providers/google";
import type {AuthOptions} from "next-auth";
import http from "@/utils/http"

export const authOptions: AuthOptions = {
    session: {
        strategy: 'jwt',
        maxAge: 5 * 60,
    },
    providers: [
        GoogleProvider({
            clientId: process.env.NEXT_GOOGLE_CLIENTID!,
            clientSecret: process.env.NEXT_GOOGLE_SECRET!,
        }),
    ],
    callbacks: {
        async signIn({ account, profile, user }) {
            console.log("account", user)
            user.id = account?.id_token!
            return true;
        },
        async jwt({token }) {
            
            const res = await http.post<{access_token: string}>("/identityservice/connect/token", {
                client_id: "google",
                client_secret: "secret",
                grant_type: 'external',
                token: token.sub,
                scope: "openid profile api",
                provider: "google",
            }, { 
                headers: {
                    "Content-Type": "application/x-www-form-urlencoded",
                }
            })
            token.access_token = res?.data?.access_token
            return token;
        },
        async session({session, token } ) {
             if (token) {
                 console.log("token", token)
                session.user.access_token = token?.access_token
            }
            return session;
        },
    }
}

const handler = NextAuth(authOptions);
export {handler as GET, handler as POST}