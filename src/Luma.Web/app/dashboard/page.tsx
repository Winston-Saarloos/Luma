"use client";

import { useSession } from "next-auth/react";
import { useRouter } from "next/navigation";
import { useEffect } from "react";
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";

export default function Dashboard() {
  const { data: session, status } = useSession();
  const router = useRouter();

  useEffect(() => {
    if (status === "unauthenticated") {
      router.push("/auth/signin");
    }
  }, [status, router]);

  if (status === "loading") {
    return (
      <div className="min-h-screen flex items-center justify-center">
        <div className="text-lg">Loading...</div>
      </div>
    );
  }

  if (!session) {
    return null;
  }

  return (
    <div className="min-h-screen p-4 md:p-6 lg:p-8">
      <div className="max-w-4xl mx-auto">
        <h1 className="text-3xl font-bold mb-6">Protected Dashboard</h1>

        <Card>
          <CardHeader>
            <CardTitle>User Information</CardTitle>
            <CardDescription>
              Your authenticated session details
            </CardDescription>
          </CardHeader>
          <CardContent>
            <div className="space-y-2">
              <p>
                <strong>User ID:</strong> {session.user.id}
              </p>
              <p>
                <strong>Name:</strong> {session.user.name}
              </p>
              <p>
                <strong>Email:</strong> {session.user.email}
              </p>
              <p>
                <strong>Access Token:</strong>
              </p>
              <code style={{ wordBreak: "break-word" }}>
                {session.accessToken}
              </code>
            </div>
          </CardContent>
        </Card>
      </div>
    </div>
  );
}
