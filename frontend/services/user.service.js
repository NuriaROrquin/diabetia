import axios from './axios';

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

