export interface Paginator<T> {
    pageItems: T[];
    totalItems: number;
    currentPageIndex: number;
}