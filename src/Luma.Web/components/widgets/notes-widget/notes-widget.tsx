import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import { Button } from "@/components/ui/button";

export function NotesWidget() {
  return (
    <Card className="h-full card-glass backdrop-blur-md bg-white/10 dark:bg-black/20 border-white/20 dark:border-white/10">
      <CardHeader>
        <CardTitle className="flex items-center gap-2 text-white">
          <span className="text-2xl">📝</span>
          Quick Notes
        </CardTitle>
        <CardDescription className="text-white/70">
          Capture your thoughts
        </CardDescription>
      </CardHeader>
      <CardContent>
        <textarea
          className="w-full h-32 p-2 text-sm border rounded resize-none bg-white/20 border-white/30 text-white placeholder:text-white/50"
          placeholder="Write your notes here..."
        />
        <Button
          className="w-full mt-2 bg-white/20 hover:bg-white/30 border-white/30 text-white"
          size="sm"
        >
          Save Note
        </Button>
      </CardContent>
    </Card>
  );
}
