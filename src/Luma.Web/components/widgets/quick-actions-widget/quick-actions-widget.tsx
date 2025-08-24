import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import { Button } from "@/components/ui/button";

export function QuickActionsWidget() {
  return (
    <Card className="h-full card-glass backdrop-blur-md bg-white/10 dark:bg-black/20 border-white/20 dark:border-white/10">
      <CardHeader>
        <CardTitle className="flex items-center gap-2 text-white">
          <span className="text-2xl">⚡</span>
          Quick Actions
        </CardTitle>
        <CardDescription className="text-white/70">
          Common tasks and shortcuts
        </CardDescription>
      </CardHeader>
      <CardContent className="space-y-3">
        <Button
          className="w-full justify-start bg-white/20 hover:bg-white/30 border-white/30 text-white"
          variant="outline"
        >
          📧 New Email
        </Button>
        <Button
          className="w-full justify-start bg-white/20 hover:bg-white/30 border-white/30 text-white"
          variant="outline"
        >
          📅 Schedule Meeting
        </Button>
        <Button
          className="w-full justify-start bg-white/20 hover:bg-white/30 border-white/30 text-white"
          variant="outline"
        >
          📊 View Analytics
        </Button>
        <Button
          className="w-full justify-start bg-white/20 hover:bg-white/30 border-white/30 text-white"
          variant="outline"
        >
          ⚙️ Settings
        </Button>
      </CardContent>
    </Card>
  );
}
