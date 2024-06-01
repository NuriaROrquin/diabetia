import {Section} from "../../../components/section";
import {TitleSection} from "../../../components/titles";
import {GENDER, TYPE_EVENTS} from "../../../constants";
import {useState} from "react";
import {InputWithLabel} from "../../../components/input";
import dayjs from "dayjs";
import {ButtonOrange} from "../../../components/button";
import {CustomDatePicker} from "../../../components/pickers";
import {useRouter} from "next/router";
import {Step, StepLabel, Stepper} from "@mui/material";
import {useCookies} from "react-cookie";
import {Select} from "@/components/selector";
import {firstStep} from "../../../services/api.service";


const InitialFormStep1 = () => {
    const [error, setError] = useState(false);
    const eventSelected = TYPE_EVENTS.filter((event) => event.id === 2)[0].title;
    const [date, setDate] = useState()
    const router = useRouter()
    const [cookies, _setCookie, _removeCookie] = useCookies(['email']);
    const email = cookies.email
    const [isOpen, setIsOpen] = useState(false);
    const [selectedOption, setSelectedOption] = useState(null);

    const handleOptionClick = (option) => {
        setSelectedOption(option);
        setIsOpen(false);
    };

    const handleSubmit = () => {
        const name = document.getElementById("name").value;
        const lastname = document.getElementById("lastname").value;
        const birthdate = date ? date.format('DD-MM-YYYY') : null;
        const weight = document.getElementById("weight").value;
        const phone = document.getElementById("phone").value;
        const gender = selectedOption.title;

        console.log("Datos del formulario:", name, lastname, birthdate, weight, email, phone, gender);

        firstStep({name, birthdate, email, gender, phone, weight, lastname})
            .then((res) => {
                if(res.data){

                    router.push("/initialForm/step-2")
                }

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