import {HttpResponse, HttpTransportType, HubConnection, HubConnectionBuilder, HubConnectionState} from "@microsoft/signalr"





class CommentSignalr {

    public connection: HubConnection | undefined

    constructor(productId: string, access_token: string) {
        this.connection = new HubConnectionBuilder().withUrl(
            `ws://localhost:9998/hubs/comments?group-id=${productId}`
            , {
                skipNegotiation: true,
                transport: HttpTransportType.WebSockets,
                accessTokenFactory(): string | Promise<string> {
                    return access_token || ""
                }
                
            })
            .withAutomaticReconnect()
            .build()


    }

    public async Invoke<T>(action: string, model: T) {
        try {
            await this.connect()
            if (this.IsConnected()) {
                await this.connection?.invoke(action, model)
            }
        } catch (e) {
            console.log(e)
        }
    }

    public async Subscribe(actionName: string, action: (data: any) => void) {
        try {
            await this.connect()
            if (this.IsConnected()) {
                this.connection?.on(actionName, args => {
                    action(args)
                })
            }
        } catch (e) {

        }
    }

    public async connect() {
        if (this.connection?.state === HubConnectionState.Disconnected) {
            await this.connection?.start()
        }
    }
    public async disConnect() {
        await this.connection?.stop()
    }

    public IsConnected() {
        return this.connection?.state === HubConnectionState.Connected
    }
}

export default CommentSignalr