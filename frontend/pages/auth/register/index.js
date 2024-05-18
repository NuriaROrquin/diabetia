import {useRouter} from "next/router";
import {register} from "../../../services/api.service";
import {Input} from "../../../components/input";
import KeyOutlinedIcon from "@mui/icons-material/KeyOutlined";
import {ButtonBlue} from "../../../components/button";
import {CustomLink} from "../../../components/link";
import {EmailOutlined, PersonOutline} from "@mui/icons-material";

export const Register = () => {
  const router = useRouter();

  const onHandleClick = () => {
    const username = document.getElementById("username").value;
    const email = document.getElementById("email").value;
    const password = document.getElementById("contrasena").value;
    register(username, email, password)
        .then(() => {
          router.push(`/auth/verify-email?userName=${username}`);
        })
        .catch((error) => {
          console.log(error);
        });
  }

  return (
      <section className="flex">
        <div className="flex w-3/5 bg-gradient-to-b from-blue-primary to-orange-primary min-h-screen"></div>
        <div className="flex flex-col justify-center items-center w-2/5 bg-white min-h-screen">
          <div className="flex flex-col w-1/2 mb-12">
            <h1 className="font-bold text-gray-primary text-3xl">Comenzá a utilizar DiabetIA!</h1>
            <span className="text-gray-primary text-lg"></span>
          </div>

          <div className="flex flex-col w-1/2 mb-6">
              <Input type="text" placeholder="Nombre de Usuario" id="username" width="w-full" icon={<PersonOutline/>}/>

              <Input type="email" placeholder="Email" id="email" width="w-full" icon={<EmailOutlined/>}/>

              <Input type="password" placeholder="Contraseña" id="contrasena" width="w-full" icon={<KeyOutlinedIcon/>}/>
          </div>

          <ButtonBlue label="Registrarme" width="w-1/2" onClick={onHandleClick} className="mb-3"/>

          <CustomLink text="Ya estoy registrado" href="/auth/login"/>
        </div>

      </section>
  )
}

export default Register;