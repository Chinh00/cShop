export interface ResultModel <T>{
    data: T
    isError: boolean,
    message: string
}