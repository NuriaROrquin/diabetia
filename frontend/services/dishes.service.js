import axios from "./axios";

export const foodDetection = (data) => {
    console.log("data", data)
    return axios
        .post(
            `${process.env.NEXT_PUBLIC_API_URL}/FoodDishDetection/foodDetection`,
            data,
        );
}

export const confirmIngredients = (data) => {
    return axios
        .post(
            `${process.env.NEXT_PUBLIC_API_URL}/FoodDishDetection/confirmIngredients`,
            data,
        );
}

export const confirmQuantity = (data) => {
    return axios
        .post(
            `${process.env.NEXT_PUBLIC_API_URL}/FoodDishDetection/confirmQuantity`,
            data,
        );
}