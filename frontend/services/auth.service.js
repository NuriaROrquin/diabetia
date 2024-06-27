import axios from "./axios";

export const login = (username, password) => {
    return axios
        .post(
            `${process.env.NEXT_PUBLIC_API_URL}/auth/login`,
            {  userInput: username, password },
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

export const passwordRecover = (email) => {
    return axios
        .post(
            `${process.env.NEXT_PUBLIC_API_URL}/auth/passwordRecover`,
            { email },
            { withCredentials: true }
        );
}

export const passwordRecoverCode = (email, confirmationCode, password) => {
    return axios
        .post(
            `${process.env.NEXT_PUBLIC_API_URL}/auth/passwordRecoverCode`,
            { email, confirmationCode, password },
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