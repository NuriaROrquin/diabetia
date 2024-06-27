import axios from "./axios";

export const addInsulinEvent = (data) => {
    return axios
        .post(
            `${process.env.NEXT_PUBLIC_API_URL}/Insulin/AddInsulinEvent`,
            data
        );
}

export const editInsulinEvent = (data) => {
    return axios
        .post(
            `${process.env.NEXT_PUBLIC_API_URL}/Insulin/EditInsulinEvent`,
            data
        );
}

export const addPhysicalEvent = (data) => {
    return axios
        .post(
            `${process.env.NEXT_PUBLIC_API_URL}/PhysicalActivity/AddPhysicalEvent`,
            data
        );
}


export const addGlucoseEvent = (data) => {
    return axios
        .post(
            `${process.env.NEXT_PUBLIC_API_URL}/Glucose/AddGlucoseEvent`,
            data
        );
}


export const editGlucoseEvent = (data) => {
    return axios
        .post(
            `${process.env.NEXT_PUBLIC_API_URL}/Glucose/EditGlucoseEvent`,
            data
        );
}

export const addFoodManuallyEvent = (data) => {
    return axios
        .post(
            `${process.env.NEXT_PUBLIC_API_URL}/FoodManually/AddFoodManuallyEvent`,
            data
        );
}