import axios, {AxiosInstance, Axios} from "axios"
import { getSession } from 'next-auth/react';

export class Http {
    instance: AxiosInstance
    constructor() {
        this.instance = axios.create({
            baseURL: process.env.NEXT_PUBLIC_APIURL,
            headers: {
                "Content-Type": "application/json",
                "ngrok-skip-browser-warning": "undefine"
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