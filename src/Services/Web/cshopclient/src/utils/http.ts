import { AppState } from "@/stores/app.store"
import axios, {AxiosInstance, Axios} from "axios"
import { useAtom } from "jotai"
import { getSession } from 'next-auth/react';

export class Http {
    instance: AxiosInstance
    constructor() {
        this.instance = axios.create({
            baseURL: "https://7bf0-113-190-242-151.ngrok-free.app",
            headers: {
                "Content-Type": "application/json",
            }
        })
        
        
        this.instance.interceptors.request.use(async (req) => {
            const session = await getSession();
            
            req.headers.Authorization = `Bearer ${session?.user?.access_token}`;
            return req;
        }, error => {
            return Promise.reject(error);
        })
        this.instance.interceptors.response.use((res) => {
            return res
        }, error => {
            
        })
        
    }
    
}
export default new Http().instance