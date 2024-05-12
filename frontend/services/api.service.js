import axios from "axios";

export const login = (email, password) => {
    axios
        .post(
            `${process.env.NEXT_PUBLIC_API_URL}/auth/login/${email}`,
            {password},
            {withCredentials: true}
        )
        .then(({data}) => {
            console.log(data);
        })
        .catch((error) => {
            console.log(error);
        });
}