import axios, {AxiosInstance, Axios} from "axios"
import { getSession } from 'next-auth/react';

export class Http {
    instance: AxiosInstance
    constructor() {
        this.instance = axios.create({
            baseURL: "https://" + process.env.NEXT_PUBLIC_APIURL,
            headers: {
                "Content-Type": "application/json",
                "ngrok-skip-browser-warning": "undefine"
            }
        })
        
        
        this.instance.interceptors.request.use(async (req) => {
            if (typeof window !== "undefined") {
                const token = localStorage.getItem("access_token");
                if (token) {
                    req.headers.Authorization = `Bearer ${token}`;
                }
            }
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