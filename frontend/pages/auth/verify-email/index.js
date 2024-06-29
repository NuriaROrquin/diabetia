import {Input} from "../../../components/input";
import {LockOutlined} from "@mui/icons-material";
import {ButtonBlue} from "../../../components/button";
import {useRouter} from "next/router";
import {useState} from "react";
import {confirmEmailVerification} from "../../../services/auth.service";

export const VerifyEmail = () => {
    const router = useRouter();
    const [error, setError] = useState(false);

    const { username, email } = router.query;

    const onHandleClick = () => {
        const confirmationCode = document.getElementById("confirmationCode").value;
        confirmEmailVerification(username,email, confirmationCode)
            .then(() => {
                router.push(`/auth/login`)
            })
            .catch((error) => {
                error.response.data ? setError(error.response.data.Message) : setError("Hubo un error")            });
    }
    return(
        <section className="flex">
            <div className="hidden md:flex md:w-3/5 bg-gradient-to-b from-blue-primary to-orange-primary min-h-screen justify-center items-center">
                <img src="/img-auth-logo-blanco.png" alt="Logo Diabetia" className="w-1/3 h-max" />
            </div>

            <div className="flex flex-col justify-center items-center bg-gradient-to-b from-blue-primary to-orange-primary md:from-transparent w-full md:w-2/5 md:!bg-white min-h-screen p-4 md:p-0">
                <div className="flex w-full md:hidden justify-start items-start pl-12 pb-4">
                    <img src="/isologo-blanco.png" alt="Isologo Celeste" className="w-24 h-auto" />
                </div>

                <div className="flex flex-col w-full text-center md:w-1/2 mb-6">
                    <h1 className="font-bold md:text-gray-primary text-3xl">Verificá tu email</h1>
                    <span className="md:text-gray-primary text-lg mt-4">Enviamos un código de verificación a tu correo. Ingresa el código.</span>
                </div>

                <div className="flex flex-col w-full md:w-1/2 mb-6">
                    <Input type="text" placeholder="Codigo" id="confirmationCode" width="w-full"
                           icon={<LockOutlined/>}/>
                </div>

                {error && <span className="text-red-500 bg-white mb-3 rounded p-2">{error}</span>}

                <ButtonBlue label="Verificar email" width="w-1/2" onClick={onHandleClick} className="mb-3"/>
            </div>

        </section>
    )
}

export default VerifyEmail;