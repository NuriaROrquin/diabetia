import axios from "./axios";

export const getEventType = (data) => {
    return axios
        .get(
            `${process.env.NEXT_PUBLIC_API_URL}/Event/GetEventType/${data.id}`)
}

export const deleteEventById = (eventId) => {
    return axios.post(
        `${process.env.NEXT_PUBLIC_API_URL}/Event/DeleteEvent/${eventId}`
    )
}

export const addInsulinEvent = (data) => {
    return axios
        .post(
            `${process.env.NEXT_PUBLIC_API_URL}/Insulin/AddInsulinEvent`,
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

export const addFoodManuallyEvent = (data) => {
    return axios
        .post(
            `${process.env.NEXT_PUBLIC_API_URL}/FoodManually/AddFoodManuallyEvent`,
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

export const editGlucoseEvent = (data) => {
    return axios
        .post(
            `${process.env.NEXT_PUBLIC_API_URL}/Glucose/EditGlucoseEvent`,
            data
        );
}

//TODO: add nota libre

//TODO: add de studies

//TODO: add medical visit

//TODO: add health event

//TODO: edit medical visit

//TODO: edit health event

//TODO: edit de studies

//TODO: edit physical event

//TODO: edit de food

//TODO: edit nota libre