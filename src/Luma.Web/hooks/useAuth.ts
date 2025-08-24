"use client";

import { useSession, signIn, signOut } from "next-auth/react";

export function useAuth() {
  const { data: session, status, update } = useSession();

  const login = (provider?: string) => {
    return signIn(provider || "luma", { callbackUrl: "/" });
  };

  const logout = () => {
    return signOut({ callbackUrl: "/" });
  };

  return {
    session,
    status,
    update,
    login,
    logout,
    isAuthenticated: !!session,
    isLoading: status === "loading",
  };
}
