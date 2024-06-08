import axios from "axios";

export const login = (username, password) => {
    return axios
        .post(
            `${process.env.NEXT_PUBLIC_API_URL}/auth/login`,
            { username, password },
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

export const tagRegistration = (data) => {
    return axios
        .post(
            `${process.env.NEXT_PUBLIC_API_URL}/Tag/tagRegistration`,
            data
        );
}

export const addPhysicalEvent = (data) => {
    return axios
        .post(
            `${process.env.NEXT_PUBLIC_API_URL}/Event/AddPhysicalEvent`,
            data
        );
}

export const addGlucoseEvent = (data) => {
    return axios
        .post(
            `${process.env.NEXT_PUBLIC_API_URL}/Event/AddGlucoseEvent`,
            data
        );
}

export const getMetrics = (data) => {
    return axios
        .post(
            `${process.env.NEXT_PUBLIC_API_URL}/Home/metrics`,
            data)
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

export const getAllEvents = (data) => {
    return axios
        .post(
            `${process.env.NEXT_PUBLIC_API_URL}/Calendar/events`,
            data
        );
}

export const getEventsByDate = (date, email) => {
    return axios.post(
        `${process.env.NEXT_PUBLIC_API_URL}/Calendar/eventsByDate`,
        { date: date, email: email },
        { headers: { 'Content-Type': 'application/json' } }
    );
}