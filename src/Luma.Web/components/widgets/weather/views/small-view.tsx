import { Card, CardContent } from "@/components/ui/card";
import { useWeatherData } from "@/components/widgets/weather/useWeatherData";

export function SmallWeatherView() {
  const { data } = useWeatherData();

  if (!data) return null;

  return (
    <Card className="h-full card-glass backdrop-blur-md bg-white/10 dark:bg-black/20 border-white/20 dark:border-white/10">
      <CardContent className="p-3 h-full flex flex-col justify-center items-center">
        <div className="text-center">
          <div className="text-3xl mb-1">🌤️</div>
          <div className="text-2xl font-bold text-white">
            {Math.round(data.tempF)}°
          </div>
          <div className="text-xs text-white/70">{data.condition}</div>
        </div>
      </CardContent>
    </Card>
  );
}
