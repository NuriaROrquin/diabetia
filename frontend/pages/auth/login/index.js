import {Input} from "../../../components/input";
import KeyOutlinedIcon from "@mui/icons-material/KeyOutlined";
import {PersonOutline} from "@mui/icons-material";
import {ButtonBlue} from "../../../components/button";
import {CustomLink} from "../../../components/link";
import {useRouter} from "next/router";
import {useState} from "react";
import {useCookies} from "react-cookie";
import {jwtDecode} from "jwt-decode";
import {login} from "../../../services/auth.service";

export const Login = () => {
    const [error, setError] = useState(false);
    const [_cookies, setCookie, _removeCookie] = useCookies(['cookie-name']);

    const router = useRouter();

    const onHandleClick = () => {
        const username = document.getElementById("username").value;
        const password = document.getElementById("contrasena").value;
        login(username, password)
            .then((res) => {
                setCookie("jwt", res.data.token, {path: "/", expires: new Date(Date.now() + 1000 * 60 * 60 * 24 * 30)});
                sessionStorage.setItem("jwt", res.data.token);

                const jwt = res.data.token;

                const jwtDecoded = jwtDecode(jwt)

                const stepCompleted = jwtDecoded.stepCompleted

                if (stepCompleted !== "4"){
                    router.push(`/initialForm`)
                }else{
                    router.push(`/dashboard`)
                }
            })
            .catch((error) => {
                console.log(error.response.data.Message)
                error.response.data ? setError(error.response.data.Message) : setError("Hubo un error")
            });
    }

    return (
        <section className="flex flex-col bg-gradient-to-b from-blue-primary to-orange-primary md:flex-row">
            <div className="hidden md:flex md:w-3/5 bg-gradient-to-b from-blue-primary to-orange-primary min-h-screen justify-center items-center">
                <img src="/img-auth-logo-blanco.png" alt="Logo Diabetia" className="w-1/3 h-max" />
            </div>

            <div className="flex flex-col justify-center items-center bg-gradient-to-b from-blue-primary to-orange-primary md:from-transparent w-full md:w-2/5 md:!bg-white min-h-screen p-4 md:p-0">
                <div className="flex w-full md:hidden justify-start items-start pl-12 pb-4">
                    <img src="/isologo-blanco.png" alt="Isologo Celeste" className="w-24 h-auto" />
                </div>

                <div className="flex flex-col w-full text-center md:w-1/2 mb-6">
                    <h1 className="font-bold md:text-gray-primary text-white text-3xl">¡Bienvenido!</h1>
                    <span className="md:text-gray-primary text-white text-lg">Ingresá tus credenciales</span>
                </div>

                <div className="flex flex-col w-full md:w-1/2 mb-6">

                    <Input type="text" placeholder="Nombre de Usuario" id="username" width="w-full" icon={<PersonOutline/>}/>

                    <Input type="password" placeholder="Contraseña" id="contrasena" width="w-full text-white"
                           icon={<KeyOutlinedIcon/>} withForgotPassword/>
                </div>

                {error && <span className="text-red-500 mb-3">{error}</span>}

                <ButtonBlue label="Ingresar" width="w-1/2" onClick={onHandleClick} className="mb-3" />

                <CustomLink text="No estoy registrado" className="text-white" href="/auth/register"/>
            </div>

        </section>
    )
}

export default Login;