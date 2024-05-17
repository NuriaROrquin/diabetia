import axios from "axios";
import {redirect} from "next/navigation";

export const login = (email, password) => {
    return axios
        .post(
            `${process.env.NEXT_PUBLIC_API_URL}/auth/login`,
            { email, password },
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