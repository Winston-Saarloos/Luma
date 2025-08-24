import { SmallWeatherView } from "@/components/widgets/weather/views/small-view";
import { MediumWeatherView } from "@/components/widgets/weather/views/medium-view";
import { LargeWeatherView } from "@/components/widgets/weather/views/large-view";
import { useWidgetSize } from "@/hooks/useWidgetSize";

export default function ResponsiveWeatherView() {
  const { width, height } = useWidgetSize("weather");

  // Calculate the area to determine the appropriate view
  const area = width * height;

  // Small view: 2x2 to 3x3 (area 4-9)
  if (area <= 9) {
    return <SmallWeatherView />;
  }

  // Medium view: 4x3 to 6x4 (area 12-24)
  if (area <= 24) {
    return <MediumWeatherView />;
  }

  // Large view: anything larger
  return <LargeWeatherView />;
}
