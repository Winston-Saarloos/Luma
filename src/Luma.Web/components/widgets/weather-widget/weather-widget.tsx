import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";

export function WeatherWidget() {
  return (
    <Card className="h-full card-glass backdrop-blur-md bg-white/10 dark:bg-black/20 border-white/20 dark:border-white/10">
      <CardHeader>
        <CardTitle className="flex items-center gap-2 text-white">
          <span className="text-2xl">🌤️</span>
          Weather
        </CardTitle>
        <CardDescription className="text-white/70">
          Current weather conditions
        </CardDescription>
      </CardHeader>
      <CardContent>
        <div className="text-center">
          <div className="text-4xl font-bold mb-2 text-white">72°F</div>
          <div className="text-sm text-white/70 mb-4">Partly Cloudy</div>
          <div className="grid grid-cols-2 gap-2 text-xs text-white/80">
            <div>Humidity: 65%</div>
            <div>Wind: 8 mph</div>
            <div>Pressure: 30.1 in</div>
            <div>UV Index: 3</div>
          </div>
        </div>
      </CardContent>
    </Card>
  );
}
