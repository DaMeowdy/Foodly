"use client";

import { QueryClientProvider, QueryClient } from '@tanstack/react-query'
import { useRef } from 'react';
interface ReactQueryProviderProps {
    children: React.ReactNode;
}

export const RQProvider: React.FC<ReactQueryProviderProps> = ({ children }) => {
    const _queryClient = useRef(new QueryClient());
    return (<QueryClientProvider client={_queryClient.current}>{children}</QueryClientProvider>)
}