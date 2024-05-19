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
    return axios
        .post(
            `${process.env.NEXT_PUBLIC_API_URL}/Tag/tagDetection`,
            data
        );
}