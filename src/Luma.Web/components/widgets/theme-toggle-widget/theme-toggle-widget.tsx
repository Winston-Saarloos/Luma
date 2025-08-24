import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import { ThemeToggle } from "@/components/theme-toggle";

export function ThemeToggleWidget() {
  return (
    <Card className="h-full card-glass backdrop-blur-md bg-white/10 dark:bg-black/20 border-white/20 dark:border-white/10">
      <CardHeader>
        <CardTitle className="flex items-center gap-2 text-white">
          <span className="text-2xl">🎨</span>
          Appearance
        </CardTitle>
        <CardDescription className="text-white/70">
          Customize your theme
        </CardDescription>
      </CardHeader>
      <CardContent className="flex items-center justify-center">
        <ThemeToggle />
      </CardContent>
    </Card>
  );
}
