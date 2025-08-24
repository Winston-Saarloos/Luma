import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";

export function CalendarWidget() {
  return (
    <Card className="h-full card-glass backdrop-blur-md bg-white/10 dark:bg-black/20 border-white/20 dark:border-white/10">
      <CardHeader>
        <CardTitle className="flex items-center gap-2 text-white">
          <span className="text-2xl">📅</span>
          Calendar
        </CardTitle>
        <CardDescription className="text-white/70">
          Today's schedule
        </CardDescription>
      </CardHeader>
      <CardContent>
        <div className="space-y-3">
          <div className="flex items-center gap-3 p-2 bg-white/10 rounded">
            <div className="w-2 h-2 bg-blue-400 rounded-full"></div>
            <div className="flex-1">
              <div className="text-sm font-medium text-white">Team Standup</div>
              <div className="text-xs text-white/70">9:00 AM - 9:30 AM</div>
            </div>
          </div>
          <div className="flex items-center gap-3 p-2 bg-white/10 rounded">
            <div className="w-2 h-2 bg-green-400 rounded-full"></div>
            <div className="flex-1">
              <div className="text-sm font-medium text-white">
                Client Meeting
              </div>
              <div className="text-xs text-white/70">2:00 PM - 3:00 PM</div>
            </div>
          </div>
          <div className="flex items-center gap-3 p-2 bg-white/10 rounded">
            <div className="w-2 h-2 bg-purple-400 rounded-full"></div>
            <div className="flex-1">
              <div className="text-sm font-medium text-white">Code Review</div>
              <div className="text-xs text-white/70">4:30 PM - 5:00 PM</div>
            </div>
          </div>
        </div>
      </CardContent>
    </Card>
  );
}
