import {Input} from "../../../components/input";
import {CodeOffOutlined } from "@mui/icons-material";
import {ButtonBlue} from "../../../components/button";
import {CustomLink} from "../../../components/link";
import {useRouter} from "next/router";
import {confirmEmailVerification} from "../../../services/api.service";

export const VerifyEmail = () => {
    const router = useRouter();
    const { username } = router.query;

    const onHandleClick = () => {
        const confirmationCode = document.getElementById("confirmationCode").value;
        confirmEmailVerification(username, confirmationCode)
            .then(() => {
                router.push(`/auth/login`)
            })
            .catch((error) => {
                console.log(error);
            });
    }
    return(
        <section className="flex">
            <div className="flex w-3/5 bg-gradient-to-b from-blue-primary to-orange-primary min-h-screen"></div>
            <div className="flex flex-col justify-center items-center w-2/5 bg-white min-h-screen">
                <div className="flex flex-col w-1/2 mb-12">
                    <h1 className="font-bold text-gray-primary text-3xl">Verificá tu email</h1>
                    <span className="text-gray-primary text-lg"></span>
                </div>

                <div className="flex flex-col w-1/2 mb-6">
                    <Input type="text" placeholder="Codigo" id="confirmationCode" width="w-full"
                           icon={<CodeOffOutlined />}/>
                </div>

                <ButtonBlue label="Verificar email" width="w-1/2" onClick={onHandleClick} className="mb-3"/>
            </div>

        </section>
    )
}

export default VerifyEmail;