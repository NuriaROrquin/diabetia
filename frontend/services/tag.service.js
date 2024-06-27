import axios from "./axios";

export const tagDetection = (data) => {
    return axios
        .post(
            `${process.env.NEXT_PUBLIC_API_URL}/Tag/tagDetection`,
            data
        );
}

export const tagRegistration = (data) => {
    return axios
        .post(
            `${process.env.NEXT_PUBLIC_API_URL}/Tag/tagRegistration`,
            data
        );
}