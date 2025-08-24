import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";
import { useWeatherData } from "@/components/widgets/weather/useWeatherData";

export function MediumWeatherView() {
  const { data } = useWeatherData();

  if (!data) return null;

  return (
    <Card className="h-full card-glass backdrop-blur-md bg-white/10 dark:bg-black/20 border-white/20 dark:border-white/10">
      <CardHeader className="pb-2">
        <CardTitle className="flex items-center gap-2 text-white text-sm">
          <span className="text-xl">🌤️</span>
          Weather
        </CardTitle>
      </CardHeader>
      <CardContent className="pt-0">
        <div className="text-center">
          <div className="text-3xl font-bold mb-1 text-white">
            {Math.round(data.tempF)}°F
          </div>
          <div className="text-xs text-white/70 mb-3">{data.condition}</div>
          <div className="grid grid-cols-2 gap-1 text-xs text-white/80">
            <div>Humidity: {data.humidity}%</div>
            <div>Wind: {data.windMph} mph</div>
          </div>
        </div>
      </CardContent>
    </Card>
  );
}
