// widgets/weather/useData.ts
import { useQuery } from "@tanstack/react-query";

export interface WeatherData {
  tempF: number;
  condition: string;
  humidity: number;
  windMph: number;
  pressureIn: number;
  uv: number;
}

export function useWeatherData() {
  return useQuery<WeatherData>({
    queryKey: ["weather", "current"],
    queryFn: () => {
      // Mock data - simulate API delay
      return new Promise<WeatherData>((resolve) => {
        setTimeout(() => {
          resolve({
            tempF: 72,
            condition: "Partly Cloudy",
            humidity: 65,
            windMph: 8,
            pressureIn: 30.1,
            uv: 3,
          });
        }, 500); // Simulate 500ms network delay
      });
    },
    staleTime: 5 * 60 * 1000, // 5-min cache
  });
}
