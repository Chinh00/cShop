import NextAuth from "next-auth"
import DuendeIdentityServer6 from "next-auth/providers/duende-identity-server6";

const handler = NextAuth({
    session: {
        strategy: 'jwt'
    },
    providers: [
        DuendeIdentityServer6({
            id: 'oidc',
            clientId: 'nextjs',
            clientSecret: 'secret',
            issuer: "http://localhost:5001",
            authorization: {
                params: {scope: 'openid profile api'}
            },
            idToken: true,
            
        })
    ],
    callbacks: {
        async jwt({token, profile, account, user}) {
            if (profile) {
                // token.username = profile.username;
                console.log(profile)
            }
            if (account) {
                token.access_token = account.access_token;
                console.log(token)
            }
            return token;
        },
        async session({session, token}) {
            if (token) {
                // session.user.username = token.username;
            }
            return session;
        },
    }
});
export {handler as GET, handler as POST}