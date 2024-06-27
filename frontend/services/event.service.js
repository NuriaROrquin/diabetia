import axios from "./axios";

export const addInsulinEvent = (data) => {
    return axios
        .post(
            `${process.env.NEXT_PUBLIC_API_URL}/Insulin/AddInsulinEvent`,
            data
        );
}

