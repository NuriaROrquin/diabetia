import {useRouter} from "next/router";
import {Input} from "../../../components/input";
import KeyOutlinedIcon from "@mui/icons-material/KeyOutlined";
import {ButtonBlue} from "../../../components/button";
import {CustomLink} from "../../../components/link";
import {EmailOutlined, PersonOutline} from "@mui/icons-material";
import {useState} from "react";
import {register} from "../../../services/auth.service";

export const Register = () => {
  const router = useRouter();
    const [errors, setErrors] = useState({});

    const onHandleClick = () => {
    const username = document.getElementById("username").value;
    const email = document.getElementById("email").value;
    const password = document.getElementById("contrasena").value;
    register(username, email, password)
        .then(() => {
          router.push(`/auth/verify-email?username=${username}&email=${email}`);
        })
        .catch((error) => {
            if (error.response && error.response.data && error.response.data.errors) {
                setErrors(error.response.data.errors); // Handle backend validation errors
            } else if (error.response && error.response.data && error.response.data.Message) {
                setErrors({ general: error.response.data.Message }); // Handle specific error messages
            } else {
                setErrors({ general: "Hubo un error" }); // Fallback for other errors
            }
        });
  }

  return (
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
                  <h1 className="font-bold md:text-gray-primary text-3xl">Comenzá a utilizar DiabetIA!</h1>
                  <span className="text-gray-primary text-lg"></span>
              </div>

              <div className="flex flex-col w-full md:w-1/2 mb-6">
                  <Input type="text" placeholder="Nombre de Usuario" id="username" width="w-full"
                         icon={<PersonOutline/>}/>

                  <Input type="email" placeholder="Email" id="email" width="w-full" icon={<EmailOutlined/>}/>

                  <Input type="password" placeholder="Contraseña" id="contrasena" width="w-full"
                         icon={<KeyOutlinedIcon/>}/>
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

              <ButtonBlue label="Registrarme" width="w-1/2" onClick={onHandleClick} className="mb-3"/>

              <CustomLink text="Ya estoy registrado" href="/auth/login"/>
          </div>

      </section>
  )
}

export default Register;