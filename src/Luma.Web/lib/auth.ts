import { NextAuthOptions } from "next-auth";

export const authOptions: NextAuthOptions = {
  providers: [
    {
      id: "luma",
      name: "Luma Auth",
      type: "oauth",
      wellKnown: `${
        process.env.LUMA_AUTH_URL || "http://localhost:3001"
      }/.well-known/openid-configuration`,
      // Remove userinfo endpoint and use ID token instead
      // userinfo: {
      //   url: `${
      //     process.env.LUMA_AUTH_URL || "https://localhost:3002"
      //   }/connect/userinfo`,
      // },
      clientId: process.env.LUMA_CLIENT_ID || "webclient",
      clientSecret: "", // Public client, no secret needed
      checks: ["pkce"],
      profile(profile: { sub: string; name?: string; email?: string }) {
        console.log("Profile data received:", profile);
        return {
          id: profile.sub || "unknown",
          name: profile.name || "Unknown User",
          email: profile.email || "",
          image: null,
        };
      },
    },
  ],
  callbacks: {
    async jwt({ token, account, profile }) {
      // Persist the OAuth access_token and or the user id to the token right after signin
      if (account) {
        token.accessToken = account.access_token;
        token.refreshToken = account.refresh_token;
        token.expiresAt = account.expires_at;
      }

      // If we have profile data, use it
      if (profile) {
        token.name = profile.name;
        token.email = profile.email;
      }

      return token;
    },
    async session({ session, token }) {
      // Send properties to the client, like an access_token and user id from a provider.
      session.accessToken = token.accessToken;
      session.user.id = token.sub!;
      session.user.name = token.name;
      session.user.email = token.email;
      return session;
    },
  },
  pages: {
    signIn: "/auth/signin",
    signOut: "/auth/signout",
    error: "/auth/error",
  },
  session: {
    strategy: "jwt",
  },
  debug: true, // Enable debug mode to see detailed logs
};
