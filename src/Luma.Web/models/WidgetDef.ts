export interface WidgetDef {
  id: string;
  displayName: string;
  component: React.LazyExoticComponent<() => React.JSX.Element>;
  defaultLayout: { x: number; y: number; w: number; h: number };
}
