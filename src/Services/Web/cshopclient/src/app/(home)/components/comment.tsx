import { Avatar, Card, Divider, Input } from "antd";
import Search from "antd/es/input/Search";
import {memo, useEffect, useRef, useState } from "react";
import { useSession } from "next-auth/react";
import { useGetComments } from "@/app/(home)/hooks/useGetComments";
import { XQuery } from "@/utils/xQuery";
import moment from "moment-timezone";
import { Comment as CommentDto } from "@/models/comment";
import CommentSignalr from "@/utils/hub";

export type CommentProps = {
    productId: string;
};

export const Comment = ({ productId }: CommentProps) => {
    const { data: session, status } = useSession();
    const socketRef = useRef<CommentSignalr | null>(null);
    const query: XQuery = {
        filters: [{ field: "ProductId", operator: "==", value: productId }],
        sorts: ["CreatedAtDesc"],
    };
    const {data, refetch} = useGetComments(query);
    const [dataRes, setDataRes] = useState<CommentDto[]>([]);
    useEffect(() => {
        if (data?.items) {
            setDataRes(prevState => [...data?.items, ...prevState]);
        }
    }, [data]);

    useEffect(() => {
        const accessToken = session?.user?.access_token;
        if (!accessToken) return
        if (!socketRef.current) {
            socketRef.current = new CommentSignalr(productId, session?.user?.access_token);
        }
    }, [session?.user?.access_token, productId]);
    useEffect(() => {
        if (socketRef.current) {
            socketRef.current.Subscribe("ReceiveMessage", async (m: any) => {
                const res = await JSON.parse(m)?.Value?.Data as CommentDto;
                if (res) {
                    setDataRes([])
                    await refetch()
                }
                
            }).then();
        }
    }, [session?.user?.access_token]);

    const [value, setValue] = useState("")
    return (
        <Card>
            <Search
                onSearch={(data) => {
                    if (socketRef.current) {
                        socketRef.current.Invoke("SendMessage", data);
                    }
                    setValue("")
                }}
                onChange={(e) => setValue(e.target.value)}
                disabled={status === "unauthenticated"}
                value={value}
                placeholder={status === "unauthenticated" ? "Required login" : ""}
            />
            <div className="flex flex-col gap-5 h-[150vh] overflow-scroll pt-5 py-5">
                {dataRes && dataRes?.map((item, index) => {
                    return (
                        <Card key={`${index}`}>
                            <div className="flex items-center gap-3">
                                <Avatar src={item?.user?.avatar || "/default-avatar.png"} />
                                <div>{item?.user?.username || "Unknown User"}</div>
                            </div>
                            <div>{moment(item?.createdAt).format("dddd")}</div>
                            <Divider />
                            <div>{item?.content || "No content available"}</div>
                        </Card>
                    )
                })}
            </div>
        </Card>
    );
};

