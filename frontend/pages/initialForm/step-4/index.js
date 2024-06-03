import {Section} from "../../../components/section";
import {TitleSection} from "../../../components/titles";
import {
    ACTIVITY_FREQUENCY,
    TYPE_EXERCISES,
    ACTIVITY_HOURS_WEEK,
    TYPE_ILLNESS,
    TYPE_DEVICES
} from "../../../constants";
import {useState} from "react";
import {OrangeLink} from "../../../components/link";
import {CustomSwitch} from "../../../components/input";
import {Select} from "../../../components/selector";
import dayjs from "dayjs";
import {ButtonOrange} from "../../../components/button";
import {CustomTimePicker} from "../../../components/pickers";
import {useCookies} from "react-cookie";
import {Step, StepLabel, Stepper} from "@mui/material";
import {secondStep} from "../../../services/api.service";
import {useRouter} from "next/router";

const InitialFormStep4 = () => {
    const [error, setError] = useState(false);
    const [selectedOptionDevices, setSelectedOptionDevices] = useState(null);
    const [selectedOptionActivity, setSelectedOptionActivity] = useState(null);
    const [selectedOptionActivityHoursWeek, setSelectedOptionActivityHoursWeek] = useState(null);
    const [selectedOptionIllness, setSelectedOptionIllness] = useState(null);
    const [isOpenDevices, setIsOpenDevices] = useState(false);
    const [isOpenActivity, setIsOpenActivity] = useState(false);
    const [isOpenActivityHoursWeek, setIsOpenActivityHoursWeek] = useState(false);
    const [isOpenIllness, setIsOpenIllness] = useState(false);
    const router = useRouter();
    const [device, setDevice] = useState(false);
    const [illness, setIllness] = useState(false);
    const [cookies, _setCookie, _removeCookie] = useCookies(['email']);
    const email = cookies.email


    const handleOptionClickDevices = (option) => {
        setSelectedOptionDevices(option);
        setIsOpenActivityDevices(false);
    };

    const handleOptionClickActivity = (option) => {
        setSelectedOptionActivity(option);
        setIsOpenActivity(false);
    };

    const handleOptionClickActivityHoursWeek = (option) => {
        setSelectedOptionActivityHoursWeek(option);
        setIsOpenActivityHoursWeek(false);
    };

    const handleOptionClickIllness = (option) => {
        setSelectedOptionIllness(option);
        setIsOpenIllness(false);
    };

    const steps = [
        'Datos personales',
        'Datos del paciente',
        'Actividad física y salud',
        'Dispositivos y sensores',
    ];


    const handleSubmit = () => {
        const haceActividadFisica = activity;
        const frequency = selectedOptionActivityFrequency.id;
        const idActividadFisica = selectedOptionActivity.id;
        const duracion = selectedOptionActivityHoursWeek.id;


        thirdStep({email, haceActividadFisica, frequency, idActividadFisica, duracion})
            .then((res) => {
                if(res){
                    router.push("/initialForm/step-4")
                }
            })
            .catch((error) => {
                console.error('Error in secondStep:', error);
                error.response ? setError(error.response) : setError("Hubo un error")
            });
    }

    return(
        <Section className="pt-12">
            <div className="container items-center flex w-full justify-center flex-col">
                <TitleSection className="text-white">Información del paciente</TitleSection>


                {/* FORMULARIO */}
                <div
                    className="bg-white rounded-xl w-full flex flex-wrap text-gray-primary py-20 px-44 my-12 justify-around gap-x-2 gap-y-12">
                    <div className="flex flex-col w-full gap-12">
                        <Stepper activeStep={1} alternativeLabel>
                            {steps.map((label) => (
                                <Step key={label}>
                                    <StepLabel>{label}</StepLabel>
                                </Step>
                            ))}
                        </Stepper>
                        <TitleSection className="w-full !text-blue-secondary">Dispositivos y Sensores</TitleSection>

                    </div>
                    <CustomSwitch label="¿Tenés dispositivo para medir la glucosa?" id="device" onChange={() => setDevice(!device)}
                                  width="w-1/3"/>

                    {device && (
                        <>
                            <Select label="¿Qué tipo es?" placeholder="Indica qué tipo utilizas"
                                    options={TYPE_DEVICES} selectedOption={selectedOptionDevices}
                                    handleOptionClick={handleOptionClickDevices}
                                    setIsOpen={setIsOpenDevices} isOpen={isOpenDevices}
                                    width="w-1/3"/>

                            <Select label="¿Qué tipo de actividad fisica realizas?" placeholder="Indica qué actividad realizas"
                                    options={TYPE_EXERCISES} selectedOption={selectedOptionActivity}
                                    handleOptionClick={handleOptionClickActivity}
                                    setIsOpen={setIsOpenActivity} isOpen={isOpenActivity}
                                    width="w-1/3"/>

                            <Select label="Cantidad de horas por semana" placeholder="Indica las horas semanales"
                                    options={ACTIVITY_HOURS_WEEK} selectedOption={selectedOptionActivityHoursWeek}
                                    handleOptionClick={handleOptionClickActivityHoursWeek}
                                    setIsOpen={setIsOpenActivityHoursWeek} isOpen={isOpenActivityHoursWeek}
                                    width="w-1/3"/>

                            <CustomSwitch label="¿Tenés alguna enfermedad preexistente?" id="illness" onChange={() => setIllness(!illness)}
                                          width="w-1/3"/>
                            {illness && (
                                <>
                                    <Select label="¿Cuál/es?" placeholder="Indica cual"
                                            options={TYPE_ILLNESS} selectedOption={selectedOptionIllness}
                                            handleOptionClick={handleOptionClickIllness}
                                            setIsOpen={setIsOpenIllness} isOpen={isOpenIllness}
                                            width="w-1/3"/>
                                </>
                            )}
                        </>
                    )}

                    <OrangeLink href="/initialForm/step-2" label="Atrás" width="w-1/3"/>
                    <ButtonOrange onClick={handleSubmit} label="Finalizar" width="w-1/3"/>
                </div>
            </div>
        </Section>
    )
}

export default InitialFormStep4;