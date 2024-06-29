import {PersonOutline} from "@mui/icons-material";
import {ButtonBlue} from "../../../components/button";
import {CustomLink} from "../../../components/link";
import {useRouter} from "next/router";
import {Input} from "../../../components/input";
import {useState} from "react";
import {passwordRecover} from "../../../services/auth.service";

export const PasswordRecover = () => {
    const router = useRouter();
    const [errors, setErrors] = useState({});

    const onHandleClick = () => {
        const email = document.getElementById("email").value;
        passwordRecover(email)
            .then(() => {
                router.push(`/auth/password-recover/code?email=${email}`);
            })
            .catch((error) => {
                if (error.response && error.response.data && error.response.data.errors) {
                    setErrors(error.response.data.errors);
                } else if (error.response && error.response.data && error.response.data.Message) {
                    setErrors({ general: error.response.data.Message });
                } else {
                    setErrors({ general: "Hubo un error" });
                }
            });
    }

    return(
        <section className="flex">
            <div className="hidden md:flex md:w-3/5 bg-gradient-to-b from-blue-primary to-orange-primary min-h-screen justify-center items-center">
                <img src="/img-auth-logo-blanco.png" alt="Logo Diabetia" className="w-1/3 h-max" />
            </div>

            <div
                className="flex flex-col justify-center items-center bg-gradient-to-b from-blue-primary to-orange-primary md:from-transparent w-full md:w-2/5 md:!bg-white min-h-screen p-4 md:p-0">
                <div className="flex w-full md:hidden justify-start items-start pl-12 pb-4">
                    <img src="/isologo-blanco.png" alt="Isologo Celeste" className="w-24 h-auto"/>
                </div>

                <div className="flex flex-col w-full text-center md:w-1/2 mb-6">
                    <h1 className="font-bold md:text-gray-primary text-white text-3xl">Recuperá tu Contraseña</h1>
                    <span className="md:text-gray-primary text-white text-lg">Ingresá tu email para recuperar tu contraseña</span>
                </div>

                <div className="flex flex-col w-full md:w-1/2 mb-6 px-12">
                    <Input type="text" placeholder="Email" id="email" width="w-full"
                           icon={<PersonOutline/>}/>
                </div>

                <div>
                    {Object.keys(errors).length > 0 &&
                        Object.keys(errors).map((field) =>
                                Array.isArray(errors[field]) ? (
                                    errors[field].map((message, index) => (
                                        <span key={`${field}-${index}`}
                                              className="text-red-500 bg-white mb-3 rounded p-2 block">
            {message}
          </span>
                                    ))
                                ) : (
                                    <span key={field} className="text-red-500 bg-white mb-3 rounded p-2 block">
          {errors[field]}
        </span>
                                )
                        )}
                </div>

                <ButtonBlue label="Recuperar contraseña" width="w-1/2 text-md" onClick={onHandleClick}
                            className="mb-3"/>

                <CustomLink text="Ya tengo cuenta" href="/auth/login"/>
            </div>

        </section>
    )
}

export default PasswordRecover;