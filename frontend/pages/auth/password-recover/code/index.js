import {Input} from "../../../../components/input";
import {ButtonBlue} from "../../../../components/button";
import {CustomLink} from "../../../../components/link";
import {useRouter} from "next/router";
import KeyOutlinedIcon from "@mui/icons-material/KeyOutlined";
import {LockOutlined} from "@mui/icons-material";
import {passwordRecoverCode} from "../../../../services/api.service";
import {useState} from "react";

export const PasswordRecoverCode = () => {
    const router = useRouter();
    const [error, setError] = useState(false);

    const { username } = router.query;

    const onHandleClick = () => {
        const password = document.getElementById("password").value;
        const confirmationCode = document.getElementById("confirmationCode").value;
        passwordRecoverCode(username, confirmationCode, password)
            .then(() => {
                router.push(`/auth/login`);
            })
            .catch((error) => {
                error.response.data ? setError(error.response.data) : setError("Hubo un error")
            });
    }

    return(
        <section className="flex">
            <div className="flex w-3/5 bg-gradient-to-b from-blue-primary to-orange-primary min-h-screen justify-center items-center">
                <img src="/img-auth-logo-blanco.png" alt="Logo Diabetia" className="w-1/3 h-max"/>
            </div>
            <div className="flex flex-col justify-center items-center w-2/5 bg-white min-h-screen">
                <div className="flex flex-col w-1/2 mb-12">
                    <h1 className="font-bold text-gray-primary text-3xl">Recuperar Contraseña</h1>
                    <span className="text-gray-primary text-lg"></span>
                </div>

                <div className="flex flex-col w-1/2 mb-6 gap-4">
                    <Input type="password" placeholder="Nueva contraseña" id="password" width="w-full"
                           icon={<KeyOutlinedIcon />}/>
                    <Input type="text" placeholder="Codigo" id="confirmationCode" width="w-full"
                           icon={<LockOutlined />}/>
                </div>

                {error && <span className="text-red-500 mb-3">{error}</span>}

                <ButtonBlue label="Cambiar contraseña" width="w-1/2" onClick={onHandleClick} className="mb-3"/>

                <CustomLink text="Ya tengo cuenta" href="/auth/login"/>
            </div>

        </section>
    )
}

export default PasswordRecoverCode;