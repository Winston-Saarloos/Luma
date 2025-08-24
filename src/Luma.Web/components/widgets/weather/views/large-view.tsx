import {
  Card,
  CardContent,
  CardHeader,
  CardTitle,
  CardDescription,
} from "@/components/ui/card";
import { useWeatherData } from "@/components/widgets/weather/useWeatherData";

export function LargeWeatherView() {
  const { data } = useWeatherData();

  if (!data) return null;

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
          <div className="text-4xl font-bold mb-2 text-white">
            {Math.round(data.tempF)}°F
          </div>
          <div className="text-sm text-white/70 mb-4">{data.condition}</div>
          <div className="grid grid-cols-2 gap-2 text-xs text-white/80">
            <div>Humidity: {data.humidity}%</div>
            <div>Wind: {data.windMph} mph</div>
            <div>Pressure: {data.pressureIn} in</div>
            <div>UV Index: {data.uv}</div>
          </div>
        </div>
      </CardContent>
    </Card>
  );
}
