
import {PersonOutline} from "@mui/icons-material";
import {ButtonBlue} from "../../../components/button";
import {CustomLink} from "../../../components/link";
import {useRouter} from "next/router";
import {passwordRecover} from "../../../services/api.service";
import {Input} from "../../../components/input";
import {useState} from "react";

export const PasswordRecover = () => {
    const router = useRouter();
    const [error, setError] = useState(false);

    const onHandleClick = () => {
        const username = document.getElementById("username").value;
        passwordRecover(username)
            .then(() => {
                router.push(`/auth/password-recover/code?username=${username}`);
            })
            .catch((error) => {
                error.response.data ? setError(error.response.data) : setError("Hubo un error")            });
    }

    return(
        <section className="flex">
            <div className="flex w-3/5 bg-gradient-to-b from-blue-primary to-orange-primary min-h-screen">
                <img src="/img-auth.png" alt="Descripción de la imagen" className="w-full h-full"/>
            </div>
            <div className="flex flex-col justify-center items-center w-2/5 bg-white min-h-screen">
                <div className="flex flex-col w-1/2 mb-12">
                    <h1 className="font-bold text-gray-primary text-3xl">Recuperá tu Contraseña</h1>
                    <span className="text-gray-primary text-lg"></span>
                </div>

                <div className="flex flex-col w-1/2 mb-6">
                    <Input type="text" placeholder="Nombre de Usuario" id="username" width="w-full"
                           icon={<PersonOutline/>}/>
                </div>

                {error && <span className="text-red-500 mb-3">{error}</span>}

                <ButtonBlue label="Recuperar contraseña" width="w-1/2" onClick={onHandleClick} className="mb-3"/>

                <CustomLink text="Ya tengo cuenta" href="/auth/login"/>
            </div>

        </section>
    )
}

export default PasswordRecover;