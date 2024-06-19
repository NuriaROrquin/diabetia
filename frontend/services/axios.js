import axios from "axios";

const getToken = () => {
    return sessionStorage.getItem("jwt");
}

axios.interceptors.request.use(
    config => {
        const token = getToken();
        if (token) {
            config.headers.Authorization = `Bearer ${token}`;
        }
        return config;
    },
    error => {
        return Promise.reject(error);
    }
);
export default axios;
