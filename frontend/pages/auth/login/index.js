import {Input} from "../../../components/input";
import EmailOutlinedIcon from "@mui/icons-material/EmailOutlined";
import KeyOutlinedIcon from "@mui/icons-material/KeyOutlined";
import {ButtonBlue} from "../../../components/button";
import {CustomLink} from "../../../components/link";

export const Login = () => {

    const onHandleClick = () => {
        console.log("onHandleClick");
    }

    return (
        <section className="flex">
            <div className="flex w-3/5 bg-gradient-to-b from-blue-primary to-orange-primary min-h-screen"></div>
            <div className="flex flex-col justify-center items-center w-2/5 bg-white min-h-screen">
                <div className="flex flex-col w-1/2 mb-12">
                    <h1 className="font-bold text-gray-primary text-3xl">Bienvenido!</h1>
                    <span className="text-gray-primary text-lg">Ingrese sus credenciales</span>
                </div>

                <div className="flex flex-col w-1/2 mb-6">
                    <Input type="text" placeholder="Email" id="email" width="w-full" icon={<EmailOutlinedIcon/>}/>
                    <Input type="password" placeholder="Contraseña" id="contrasena" width="w-full"
                           icon={<KeyOutlinedIcon/>}/>
                </div>

                <ButtonBlue label="Ingresar" width="w-1/2" onClick={onHandleClick} className="mb-3"/>

                <CustomLink text="No estoy registrado" href="/"/>
            </div>

        </section>
    )
}

export default Login;