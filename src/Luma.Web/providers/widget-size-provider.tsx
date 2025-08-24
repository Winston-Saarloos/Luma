"use client";

import React, { createContext, useContext } from "react";

export interface WidgetSize {
  w: number;
  h: number;
}

export type WidgetSizeMap = Record<string, WidgetSize>;

const WidgetSizeContext = createContext<WidgetSizeMap>({});

export function WidgetSizeProvider({
  sizes,
  children,
}: {
  sizes: WidgetSizeMap;
  children: React.ReactNode;
}) {
  return (
    <WidgetSizeContext.Provider value={sizes}>
      {children}
    </WidgetSizeContext.Provider>
  );
}

export function useWidgetSizes(): WidgetSizeMap {
  return useContext(WidgetSizeContext);
}
