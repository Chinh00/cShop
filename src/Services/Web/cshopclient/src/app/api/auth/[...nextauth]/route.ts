
import NextAuth from "next-auth"
import DuendeIdentityServer6 from "next-auth/providers/duende-identity-server6";

const handler = NextAuth({
    session: {
        strategy: 'jwt',
        maxAge: 30 * 24 * 60 * 60, 
    },
    providers: [
        DuendeIdentityServer6({
            id: 'oidc',
            clientId: 'nextjs',
            clientSecret: 'secret',
            issuer: "https://b53b-113-190-242-151.ngrok-free.app/identityservice",
            authorization: {
                params: {scope: 'openid profile api'}
            },
            idToken: true,
            
        })
    ],
    callbacks: {
        async jwt({token, profile, account, user}) {
            if (profile) {
                token.username = profile.username;
            }
            if (account) {
                token.access_token = account.access_token;
            }
            return token;
        },
        async session({session, token}) {
            if (token) {
                console.log(token)
                session.user.username = token.username;
                session.user.access_token = token.access_token;
            }
            return session;
        },
    }
});
export {handler as GET, handler as POST}