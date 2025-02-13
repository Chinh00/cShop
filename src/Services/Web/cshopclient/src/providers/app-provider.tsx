'use client';
import {QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { SessionProvider } from "next-auth/react";
import { ReactNode } from "react";
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import {RecoilRoot} from "recoil";
const AppProvider = ({children}: {children: ReactNode}) => {
    const queryClient = new QueryClient({
        defaultOptions: {
            queries: {
                staleTime: 0,
                retry: 3,
            },
            mutations: {}
        }
    });
    return <RecoilRoot> 
        <SessionProvider>
        
            <QueryClientProvider client={queryClient}>
                {children}
                <ToastContainer />
            </QueryClientProvider>
        
        </SessionProvider>
    </RecoilRoot>

}

export default AppProvider;