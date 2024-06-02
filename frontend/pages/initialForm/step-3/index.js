import {Section} from "../../../components/section";
import {TitleSection} from "../../../components/titles";
import {
    TYPE_INSULIN,
    TYPE_DIABETES,
    ACTIVITY_FREQUENCY,
    TYPE_EXERCISES,
    ACTIVITY_HOURS_WEEK,
    TYPE_ILLNESS
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

const InitialFormStep3 = () => {
    const [error, setError] = useState(false);
    const [isOpenTipoDiabetes, setIsOpenTipoDiabetes] = useState(false);
    const [selectedOptionTipoDiabetes, setSelectedOptionTipoDiabetes] = useState(null);
    const [isOpenTipoInsulina, setIsOpenTipoInsulina] = useState(false);
    const [selectedOptionActivityFrequency, setSelectedOptionActivityFrequency] = useState(null);
    const [selectedOptionActivity, setSelectedOptionActivity] = useState(null);
    const [selectedOptionActivityHoursWeek, setSelectedOptionActivityHoursWeek] = useState(null);
    const [selectedOptionIllness, setSelectedOptionIllness] = useState(null);
    //const [isOpenFrecuenciaInsulina, setIsOpenFrecuenciaInsulina] = useState(false);
    const [isOpenActivityFrequency, setIsOpenActivityFrequency] = useState(false);
    const [isOpenActivity, setIsOpenActivity] = useState(false);
    const [isOpenActivityHoursWeek, setIsOpenActivityHoursWeek] = useState(false);
    const [isOpenIllness, setIsOpenIllness] = useState(false);
    const [selectedOptionTipoInsulina, setSelectedOptionTipoInsulina] = useState(null);
    const router = useRouter();
    const [activity, setActivity] = useState(false);
    const [illness, setIllness] = useState(false);
    const [reminder, setReminder] = useState(false);
    const [hour, setHour] = useState();
    const [cookies, _setCookie, _removeCookie] = useCookies(['email']);
    const email = cookies.email

    const handleOptionClick = (option) => {
        setSelectedOptionTipoDiabetes(option);
        setIsOpenTipoDiabetes(false);
    };

    const handleOptionClickTipoInsulina = (option) => {
        setSelectedOptionTipoInsulina(option);
        setIsOpenTipoInsulina(false);
    };

    const handleOptionClickActivityFrequency = (option) => {
        setSelectedOptionActivityFrequency(option);
        setIsOpenActivityFrequency(false);
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
        const typeDiabetes = selectedOptionTipoDiabetes.id;
        const useInsuline = insuline;
        const typeInsuline = selectedOptionTipoInsulina.id;
        const frequency = selectedOptionFrecuenciaInsulina.id;
        const needsReminder = reminder;
        const hourReminder = hour ? hour.format('HH:mm') : null;

        secondStep({email, typeDiabetes, useInsuline, typeInsuline, frequency, needsReminder, hourReminder})
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
                        <TitleSection className="w-full !text-blue-secondary">Actividad Física y Salud</TitleSection>

                    </div>
                    <CustomSwitch label="¿Hacés Actividad Física?" id="activity" onChange={() => setActivity(!activity)}
                                  width="w-1/3"/>

                    {activity && (
                        <>
                            <Select label="¿Con qué frecuencia?" placeholder="Indica qué días realizas"
                                    options={ACTIVITY_FREQUENCY} selectedOption={selectedOptionActivityFrequency}
                                    handleOptionClick={handleOptionClickActivityFrequency}
                                    setIsOpen={setIsOpenActivityFrequency} isOpen={isOpenActivityFrequency}
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

export default InitialFormStep3;