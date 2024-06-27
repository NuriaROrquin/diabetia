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
        <section className="flex flex-col bg-gradient-to-b from-blue-primary to-orange-primary md:flex-row">
            <div className="hidden md:flex md:w-3/5 bg-gradient-to-b from-blue-primary to-orange-primary min-h-screen justify-center items-center p-0">
                <img src="/img-auth-logo-blanco.png" alt="Logo Diabetia" className="w-1/3 h-max"/>
            </div>
            <div className="flex flex-col justify-center items-center bg-gradient-to-b from-blue-primary to-orange-primary md:from-transparent w-full md:w-2/5 md:!bg-white min-h-screen px-4 pt-0">
                <div className="flex w-full md:hidden justify-start items-start pl-12 pb-4">
                    <img src="/isologo-blanco.png" alt="Isologo Celeste" className="w-24 h-auto" />
                </div>

                <div className="flex flex-col w-full text-center md:w-1/2 mb-6">
                    <h1 className="font-bold md:text-gray-primary text-white text-3xl">Recuperá tu Contraseña</h1>
                <span className="md:text-gray-primary text-white text-lg">Ingresá tu email para recuperar tu contraseña</span>
                </div>

                <div className="flex flex-col w-full md:w-1/2 mb-6 px-12">
                    <Input type="text" placeholder="Email" id="email" width="w-full"
                           icon={<PersonOutline/>}/>
                </div>

                {error && <span className="text-red-500 mb-3">{error}</span>}

                <ButtonBlue label="Recuperar contraseña" width="w-1/2 text-md" onClick={onHandleClick} className="mb-3"/>

                <CustomLink text="Ya tengo cuenta" href="/auth/login"/>
            </div>

        </section>
    )
}

export default PasswordRecover;