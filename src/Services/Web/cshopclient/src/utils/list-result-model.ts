export interface ListResultModel<T> {
    items: T[];
    total: number;
    page: number;
    pageSize: number;
}