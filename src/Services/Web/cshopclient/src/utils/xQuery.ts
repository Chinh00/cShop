export type XQuery = {
    filters?: FilterModel[],
    includes?: string[],
    page?: number,
    pageSize?: number,
    sorts?: string[]
}

export type FilterModel = {
    field: string,
    operator: "==" | ">" | "<" | "!=" | "Contains",
    value: string
}
