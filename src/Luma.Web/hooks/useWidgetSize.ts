import { useWidgetSizes } from "@/providers/widget-size-provider";

/**
 * Hook to get the current width/height (grid units) for a given widget ID.
 * Leverages the layout data provided by React Grid Layout via context –
 * no DOM measurements required.
 */
export function useWidgetSize(id: string) {
  const sizes = useWidgetSizes();
  const size = sizes[id];

  // Fallback to a sensible default if size data hasn't arrived yet
  if (!size) {
    return { width: 6, height: 4 };
  }

  return { width: size.w, height: size.h };
}
