import {PersonOutline} from "@mui/icons-material";
import {ButtonBlue} from "../../../components/button";
import {CustomLink} from "../../../components/link";
import {useRouter} from "next/router";
import {Input} from "../../../components/input";
import {useState} from "react";
import {passwordRecover} from "../../../services/auth.service";

export const PasswordRecover = () => {
    const router = useRouter();
    const [error, setError] = useState(false);

    const onHandleClick = () => {
        const email = document.getElementById("email").value;
        passwordRecover(email)
            .then(() => {
                router.push(`/auth/password-recover/code?email=${email}`);
            })
            .catch((error) => {
                console.log(error.response.data.errors)
            });
    }

    return(
        <section className="flex">
            <div
                className="flex w-3/5 bg-gradient-to-b from-blue-primary to-orange-primary min-h-screen justify-center items-center">
                <img src="/img-auth-logo-blanco.png" alt="Logo Diabetia" className="w-1/3 h-max"/>
            </div>
            <div className="flex flex-col justify-center items-center w-2/5 bg-white min-h-screen">
                <div className="flex flex-col w-1/2 mb-12">
                    <h1 className="font-bold text-gray-primary text-3xl">Recuperá tu Contraseña</h1>
                    <span className="text-gray-primary text-lg"></span>
                </div>

                <div className="flex flex-col w-1/2 mb-6">
                    <Input type="text" placeholder="Email" id="email" width="w-full"
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