import axios from "./axios";

export const getAllEventToFeedback = () => {
    return axios
        .get(
            `${process.env.NEXT_PUBLIC_API_URL}/Feedback/GetAllEventToFeedback`)
}

export const addFeedback = (data) => {
    return axios
        .post(
            `${process.env.NEXT_PUBLIC_API_URL}/Feedback/AddFeedback`,
            data
        );
}