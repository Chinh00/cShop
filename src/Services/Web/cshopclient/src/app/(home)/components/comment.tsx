import {Card} from "antd";

export type CommentProps =  {
    productId: string
}

export const Comment = (props: CommentProps) => {
    return <Card>Comment: {props?.productId}</Card>
}