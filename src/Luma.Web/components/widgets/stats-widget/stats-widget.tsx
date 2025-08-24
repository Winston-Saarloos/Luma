import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";

export function StatsWidget() {
  return (
    <Card className="h-full card-glass backdrop-blur-md bg-white/10 dark:bg-black/20 border-white/20 dark:border-white/10">
      <CardHeader>
        <CardTitle className="flex items-center gap-2 text-white">
          <span className="text-2xl">📊</span>
          Statistics
        </CardTitle>
        <CardDescription className="text-white/70">
          Key metrics overview
        </CardDescription>
      </CardHeader>
      <CardContent>
        <div className="grid grid-cols-2 gap-4">
          <div className="text-center">
            <div className="text-2xl font-bold text-blue-300">1,234</div>
            <div className="text-xs text-white/70">Total Users</div>
          </div>
          <div className="text-center">
            <div className="text-2xl font-bold text-green-300">89%</div>
            <div className="text-xs text-white/70">Uptime</div>
          </div>
          <div className="text-center">
            <div className="text-2xl font-bold text-purple-300">567</div>
            <div className="text-xs text-white/70">Active Sessions</div>
          </div>
          <div className="text-center">
            <div className="text-2xl font-bold text-orange-300">2.3s</div>
            <div className="text-xs text-white/70">Avg Response</div>
          </div>
        </div>
      </CardContent>
    </Card>
  );
}
