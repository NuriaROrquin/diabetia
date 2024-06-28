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

export const addMedicalVisitEvent = (data) => {
    return axios
        .post(
            `${process.env.NEXT_PUBLIC_API_URL}/MedicalVisit/AddMedicalVisitEvent`,
            data
        );
}

export const addMedicalExaminationEvent = (data) => {
    return axios
        .post(
            `${process.env.NEXT_PUBLIC_API_URL}/MedicalExamination/AddMedicalExaminationEvent`,
            data
        );
}

export const addFreeNoteEvent = (data) => {
    return axios
        .post(
            `${process.env.NEXT_PUBLIC_API_URL}/FreeNote/AddFreeNoteEvent`,
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

export const editMedicalVisitEvent = (data) => {
    return axios
        .post(
            `${process.env.NEXT_PUBLIC_API_URL}/MedicalVisit/EditMedicalVisitEvent`,
            data
        );
}

export const editFreeNoteEvent = (data) => {
    return axios
        .post(
            `${process.env.NEXT_PUBLIC_API_URL}/FreeNote/EditFreeNoteEvent`,
            data
        );
}

//TODO: add nota libre

//TODO: edit de studies

//TODO: add health event

//TODO: edit health event

//TODO: edit physical event

//TODO: edit de food

//TODO: edit nota libre