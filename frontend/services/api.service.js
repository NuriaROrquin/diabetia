import axios from './axios';

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

export const firstStep = (data) => {
    return axios
        .put(
            `${process.env.NEXT_PUBLIC_API_URL}/Data/firstStep`,
            data
        );
}

export const secondStep = (data) => {
    return axios
        .put(
            `${process.env.NEXT_PUBLIC_API_URL}/Data/secondStep`,
            data
        );
}

export const thirdStep = (data) => {
    return axios
        .put(
            `${process.env.NEXT_PUBLIC_API_URL}/Data/thirdStep`,
            data
        );
}

export const fourthStep = (data) => {
    return axios
        .put(
            `${process.env.NEXT_PUBLIC_API_URL}/Data/fourthStep`,
            data
        );
}

export const getUserInfo = (data) => {
    return axios
        .get(
            `${process.env.NEXT_PUBLIC_API_URL}/Profile/getUserInfo?email=${data.email}`)
}

export const getPatientInfo = (data) => {
    return axios
        .get(
            `${process.env.NEXT_PUBLIC_API_URL}/Profile/getPatientInfo?email=${data.email}`)
}

export const deleteEventById = (eventId) => {
    return axios.post(
        `${process.env.NEXT_PUBLIC_API_URL}/Event/DeleteEvent/${eventId}`
    )
}

export const getEventType = (data) => {
    return axios
        .get(
            `${process.env.NEXT_PUBLIC_API_URL}/Event/GetEventType/${data.id}`)
}

export const getIngredients = () => {
    return axios
        .get(
            `${process.env.NEXT_PUBLIC_API_URL}/Event/GetIngredients`
        );
}