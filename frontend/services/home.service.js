import axios from "./axios";

export const getTimeline = () => {
    return axios
        .get(
            `${process.env.NEXT_PUBLIC_API_URL}/Home/timeline`)
}

export const getMetrics = (dateFrom, dateTo) => {
    return axios
        .get(
            `${process.env.NEXT_PUBLIC_API_URL}/Home/metrics?dateFrom=${dateFrom}&dateTo=${dateTo}`)
}