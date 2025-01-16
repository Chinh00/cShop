import { Order } from "@/models/order"
import { Card } from "antd"
import PriceFormat from "@/utils/price-format";
import moment from "moment-timezone";

export type OrderPreviewProps = {
    order: Order
}

export const OrderPreview = (props: OrderPreviewProps) => {
    return <Card size={"small"}>
        <div>Order code: {props.order.id}</div>
        <div>Total amount: {PriceFormat.ConvertVND(props.order.totalPrice)}</div>
        
        <div>Thời gian đặt hàng: {moment(props?.order?.orderDate).tz("Asia/Ho_Chi_Minh").format("dddd, DD/MM/YYYY HH:mm:ss")}</div>
    </Card>
}