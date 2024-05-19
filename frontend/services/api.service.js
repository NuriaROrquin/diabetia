import axios from "axios";

export const login = (username, password) => {
    return axios
        .post(
            `${process.env.NEXT_PUBLIC_API_URL}/auth/login`,
            { username, password },
            { withCredentials: true }
        );
}

export const logout = () => {
    return axios
        .post(
            `/api/logout`,
            { withCredentials: true }
        );
}

export const register = (username, email, password) => {
    return axios
        .post(
            `${process.env.NEXT_PUBLIC_API_URL}/auth/register`,
            { username, email, password },
            { withCredentials: true }
        );
}

export const passwordRecover = (username) => {
    return axios
        .post(
            `${process.env.NEXT_PUBLIC_API_URL}/auth/passwordRecover`,
            { username },
            { withCredentials: true }
        );
}

export const passwordRecoverCode = (username, confirmationCode, password) => {
    return axios
        .post(
            `${process.env.NEXT_PUBLIC_API_URL}/auth/passwordRecoverCode`,
            { username, confirmationCode, password },
            { withCredentials: true }
        );
}

export const confirmEmailVerification = (username, email, confirmationCode) => {
    return axios
        .post(
            `${process.env.NEXT_PUBLIC_API_URL}/auth/confirmEmailVerification`,
            { username, email, confirmationCode},
            { withCredentials: true }
        );
}

export const tagDetection = (data) => {
    /*return axios
        .post(
            `${process.env.NEXT_PUBLIC_API_URL}/Tag/tagDetection`,
            data
        );*/

    return Promise.resolve([
        {
            "id": "e6186a8f-1425-4956-beec-55be14d04a98",
            "portion": 0.25,
            "grPerPortion": 30,
            "chInPortion": 28
        },
        {
            "id": "d744cde9-1b11-4c38-b12d-d9e38bddd55c",
            "portion": 0.5,
            "grPerPortion": 21,
            "chInPortion": 13
        }
    ]);
}