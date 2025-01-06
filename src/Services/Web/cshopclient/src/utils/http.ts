import axios, {AxiosInstance, Axios} from "axios"
export class Http {
    instance: AxiosInstance
    constructor() {
        this.instance = axios.create({
            baseURL: "https://22db-117-6-133-148.ngrok-free.app",
            headers: {
                "Content-Type": "application/json",
            }
        })
        
        this.instance.interceptors.request.use((req) => {
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