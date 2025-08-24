import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import { Input } from "@/components/ui/input";

export function SearchWidget() {
  return (
    <Card className="h-full card-glass backdrop-blur-md bg-white/10 dark:bg-black/20 border-white/20 dark:border-white/10">
      <CardHeader>
        <CardTitle className="flex items-center gap-2 text-white">
          <span className="text-2xl">🔍</span>
          Search
        </CardTitle>
        <CardDescription className="text-white/70">
          Search across your workspace
        </CardDescription>
      </CardHeader>
      <CardContent className="space-y-4">
        <Input
          placeholder="Search files, people, or anything..."
          className="bg-white/20 border-white/30 text-white placeholder:text-white/50"
        />
        <div className="text-xs text-white/70">
          Recent searches:
          <div className="mt-2 space-y-1">
            <div className="cursor-pointer hover:text-white">
              📄 project proposal
            </div>
            <div className="cursor-pointer hover:text-white">👤 john smith</div>
            <div className="cursor-pointer hover:text-white">
              📊 quarterly report
            </div>
          </div>
        </div>
      </CardContent>
    </Card>
  );
}
