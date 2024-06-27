import axios from "./axios";

export const getIngredients = () => {
    return axios
        .get(
            `${process.env.NEXT_PUBLIC_API_URL}/Event/GetIngredients`
        );
}