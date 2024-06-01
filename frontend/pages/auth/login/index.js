import {Input} from "../../../components/input";
import KeyOutlinedIcon from "@mui/icons-material/KeyOutlined";
import {PersonOutline} from "@mui/icons-material";
import {ButtonBlue} from "../../../components/button";
import {CustomLink} from "../../../components/link";
import {login} from "../../../services/api.service";
import {useRouter} from "next/router";
import {useState} from "react";
import {useCookies} from "react-cookie";

export const Login = () => {
    const [error, setError] = useState(false);
    const [_cookies, setCookie, _removeCookie] = useCookies(['cookie-name']);

    const router = useRouter();

    const onHandleClick = () => {
        const username = document.getElementById("username").value;
        const password = document.getElementById("contrasena").value;
        login(username, password)
            .then((res) => {
                if(res.data){
                    setCookie("jwt", res.data.token, {path: "/", expires: new Date(Date.now() + 1000 * 60 * 60 * 24 * 30)});
                    setCookie("informationCompleted", res.data.informationCompleted, {path: "/", expires: new Date(Date.now() + 1000 * 60 * 60 * 24 * 30)});
                    router.push(`/dashboard`)
                }

            })
            .catch((error) => {
                error.response.data ? setError(error.response.data) : setError("Hubo un error")
            });
    }

    return (
        <section className="flex">
            <div className="flex w-3/5 bg-gradient-to-b from-blue-primary to-orange-primary min-h-screen justify-center items-center">
                <img src="/img-auth-logo-blanco.png" alt="Logo Diabetia" className="w-1/3 h-max"/>
            </div>
            <div className="flex flex-col justify-center items-center w-2/5 bg-white min-h-screen">
                <div className="flex flex-col w-1/2 mb-12">
                    <h1 className="font-bold text-gray-primary text-3xl">Bienvenido!</h1>
                    <span className="text-gray-primary text-lg">Ingresá tus credenciales</span>
                </div>

                <div className="flex flex-col w-1/2 mb-6">

                    <Input type="text" placeholder="Nombre de Usuario" id="username" width="w-full" icon={<PersonOutline/>}/>

                    <Input type="password" placeholder="Contraseña" id="contrasena" width="w-full"
                           icon={<KeyOutlinedIcon/>} withForgotPassword/>
                </div>

                {error && <span className="text-red-500 mb-3">{error}</span>}

                <ButtonBlue label="Ingresar" width="w-1/2" onClick={onHandleClick} className="mb-3" />

                <CustomLink text="No estoy registrado" href="/auth/register"/>
            </div>

        </section>
    )
}

export default Login;