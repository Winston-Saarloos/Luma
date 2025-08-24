import { lazy } from "react";
import { WidgetDef } from "@/models/WidgetDef";

export const weatherWidget: WidgetDef = {
  id: "weather",
  displayName: "Weather",
  component: lazy(() => import("@/components/widgets/weather/view")),
  defaultLayout: { x: 0, y: 0, w: 6, h: 4 },
};
