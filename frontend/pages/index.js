import {Input} from "../components/input";
import EmailOutlinedIcon from '@mui/icons-material/EmailOutlined';
import KeyOutlinedIcon from '@mui/icons-material/KeyOutlined';

export default function Page() {
    return (
        <section className="flex">
            <div className="flex w-3/5 bg-gradient-to-b from-blue-primary to-orange-primary min-h-screen"></div>
            <div className="flex flex-col justify-center items-center w-2/5 bg-white min-h-screen gap-8">
                <div className="flex flex-col w-1/2">
                    <h1 className="font-bold text-gray-primary text-3xl">Bienvenido!</h1>
                    <span className="text-gray-primary text-lg">Ingrese sus credenciales</span>
                </div>

                <div className="flex flex-col w-1/2">
                    <Input type="text" placeholder="Email" id="email" label="Email" width="w-full" icon={<EmailOutlinedIcon />} />
                    <Input type="password" placeholder="Contraseña" id="contrasena" label="Contraseña" width="w-full"  icon={<KeyOutlinedIcon />} />
                </div>
            </div>
        </section>
    );
}
