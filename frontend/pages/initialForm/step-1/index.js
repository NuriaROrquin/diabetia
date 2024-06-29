import {Section} from "../../../components/section";
import {TitleSection} from "../../../components/titles";
import {GENDER} from "../../../constants";
import {useState} from "react";
import {InputWithLabel} from "../../../components/input";
import dayjs from "dayjs";
import {ButtonOrange} from "../../../components/button";
import {CustomDatePicker} from "../../../components/pickers";
import {useRouter} from "next/router";
import {Step, StepLabel, Stepper} from "@mui/material";
import {Select} from "@/components/selector";
import {firstStep} from "../../../services/user.service";
import {getEmailFromJwt} from "../../../helpers";
import {useCookies} from "react-cookie";

const InitialFormStep1 = () => {
    const [error, setError] = useState(false);
    const [date, setDate] = useState()
    const router = useRouter()
    const email = getEmailFromJwt();
    const [isOpen, setIsOpen] = useState(false);
    const [selectedOption, setSelectedOption] = useState(null);
    const [_cookies, setCookie, _removeCookie] = useCookies(['jwt']);

    const handleOptionClick = (option) => {
        setSelectedOption(option);
        setIsOpen(false);
    };

    const handleSubmit = () => {
        const name = document.getElementById("name").value;
        const lastname = document.getElementById("lastname").value;
        const birthdate = date ? date.format('YYYY-MM-DD') : null;
        const weight = document.getElementById("weight").value;
        const phone = document.getElementById("phone").value;
        const gender = selectedOption.key;


        firstStep({name, birthdate, email, gender, phone, weight, lastname})
            .then((res) => {
                setCookie("jwt", res.data.token, {path: "/", expires: new Date(Date.now() + 1000 * 60 * 60 * 24 * 30)});
                sessionStorage.setItem("jwt", res.data.token);
                router.push("/initialForm/step-2")
            })
            .catch((error) => {
                error.response.data ? setError(error.response.data) : setError("Hubo un error")
            });
    }

    const steps = [
        'Datos personales',
        'Datos del paciente',
        'Actividad física y salud',
        'Dispositivos y sensores',
    ];

    return(
        <Section className="pt-12">
            <div className="container items-center flex w-full justify-center flex-col">
                {/* FORMULARIO */}
                <div
                    className="bg-white rounded-xl w-full flex flex-wrap text-gray-primary py-20 px-44 my-12 justify-around gap-x-2 gap-y-12">
                    <div className="flex flex-col w-full gap-12">
                        <Stepper activeStep={0} alternativeLabel>
                            {steps.map((label) => (
                                <Step key={label}>
                                    <StepLabel>{label}</StepLabel>
                                </Step>
                            ))}
                        </Stepper>
                        <TitleSection className="w-full !text-blue-secondary">Datos Personales</TitleSection>

                    </div>

                    <InputWithLabel label="Nombre" placeholder="Juan" id="name" width="w-1/3"/>
                    <InputWithLabel label="Apellido" placeholder="Perez" id="lastname" width="w-1/3"/>
                    <CustomDatePicker
                        label="Fecha de nacimiento"
                        value={date}
                        onChange={(e) => setDate(e)}
                        defaultValue={dayjs()}
                        width="w-1/3"
                    />
                    <InputWithLabel label="Peso" placeholder="Ingresá tu peso" id="weight" width="w-1/3"/>
                    <Select label="Genero" placeholder="Indicanos tu género" id="gender" options={GENDER}
                            selectedOption={selectedOption}
                            handleOptionClick={handleOptionClick} setIsOpen={setIsOpen}
                            isOpen={isOpen} width="w-1/3"/>
                    <InputWithLabel label="Teléfono" placeholder="1234-5678" id="phone" width="w-1/3"/>

                    {error && <span className="text-red-500 mb-3">{error}</span>}

                    <ButtonOrange onClick={handleSubmit} label="Siguiente" width="w-1/3"/>
                </div>
            </div>
        </Section>
    )
}

export default InitialFormStep1;