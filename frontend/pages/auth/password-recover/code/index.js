import {Input} from "../../../../components/input";
import {ButtonBlue} from "../../../../components/button";
import {CustomLink} from "../../../../components/link";
import {useRouter} from "next/router";
import KeyOutlinedIcon from "@mui/icons-material/KeyOutlined";
import {LockOutlined} from "@mui/icons-material";
import {useState} from "react";
import {passwordRecoverCode} from "../../../../services/auth.service";

export const PasswordRecoverCode = () => {
    const router = useRouter();
    const [error, setError] = useState(false);

    const { email } = router.query;

    const onHandleClick = () => {
        const password = document.getElementById("password").value;
        const confirmationCode = document.getElementById("confirmationCode").value;
        passwordRecoverCode(email, confirmationCode, password)
            .then(() => {
                router.push(`/auth/login`);
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
                    <h1 className="font-bold md:text-gray-primary text-3xl">Recuperar Contraseña</h1>
                    <span className="text-gray-primary text-lg"></span>
                </div>

                <div className="flex flex-col w-1/2 mb-6 gap-4">
                    <Input type="password" placeholder="Nueva contraseña" id="password" width="w-full"
                           icon={<KeyOutlinedIcon/>}/>
                    <Input type="text" placeholder="Codigo" id="confirmationCode" width="w-full"
                           icon={<LockOutlined/>}/>
                </div>

                {error && <span className="text-red-500 bg-white mb-3 rounded p-2">{error}</span>}

                <ButtonBlue label="Cambiar contraseña" width="w-1/2" onClick={onHandleClick} className="mb-3"/>

                <CustomLink text="Ya tengo cuenta" href="/auth/login"/>
            </div>

        </section>
    )
}

export default PasswordRecoverCode;