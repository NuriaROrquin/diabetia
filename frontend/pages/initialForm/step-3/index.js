import {Section} from "../../../components/section";
import {TitleSection} from "../../../components/titles";
import {
    ACTIVITY_FREQUENCY,
    TYPE_EXERCISES,
    ACTIVITY_HOURS_WEEK,
    TYPE_ILLNESS
} from "../../../constants";
import {useState} from "react";
import {OrangeLink} from "../../../components/link";
import {CustomSwitch} from "../../../components/input";
import {Select} from "../../../components/selector";
import {ButtonOrange} from "../../../components/button";
import {Step, StepLabel, Stepper} from "@mui/material";
import {thirdStep} from "../../../services/user.service";
import {useRouter} from "next/router";
import {getEmailFromJwt} from "../../../helpers";

const InitialFormStep3 = () => {
    const [error, setError] = useState(false);
    const [selectedOptionActivityFrequency, setSelectedOptionActivityFrequency] = useState(null);
    const [selectedOptionActivity, setSelectedOptionActivity] = useState(null);
    const [selectedOptionActivityHoursWeek, setSelectedOptionActivityHoursWeek] = useState(null);
    const [selectedOptionIllness, setSelectedOptionIllness] = useState(null);
    const [isOpenActivityFrequency, setIsOpenActivityFrequency] = useState(false);
    const [isOpenActivity, setIsOpenActivity] = useState(false);
    const [isOpenActivityHoursWeek, setIsOpenActivityHoursWeek] = useState(false);
    const [isOpenIllness, setIsOpenIllness] = useState(false);
    const router = useRouter();
    const [activity, setActivity] = useState(false);
    const [illness, setIllness] = useState(false);
    const [reminder, setReminder] = useState(false);
    const [hour, setHour] = useState();
    const email = getEmailFromJwt();

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
        const haceActividadFisica = activity;
        const frequency = selectedOptionActivityFrequency ? selectedOptionActivityFrequency.id : null;
        const idActividadFisica = selectedOptionActivity ? selectedOptionActivity.id : null;
        const duracion = selectedOptionActivityHoursWeek ? selectedOptionActivityHoursWeek.id : null;


        thirdStep({email, haceActividadFisica, frequency, idActividadFisica, duracion})
            .then((res) => {
                if(res){
                    router.push("/initialForm/step-4")
                }
            })
            .catch((error) => {
                console.error('Error in thirdStep:', error);
                error.response ? setError(error.response) : setError("Hubo un error")
            });
    }

    return(
        <Section className="pt-12">
            <div className="container items-center flex w-full justify-center flex-col">
                <TitleSection className="text-white">Información del paciente</TitleSection>


                {/* FORMULARIO */}
                <div className="bg-white rounded-xl w-full flex flex-wrap text-gray-primary py-20 px-44 my-12 justify-around gap-x-2 gap-y-12">
                    <div className="flex flex-col w-full gap-12">
                        <Stepper activeStep={2} alternativeLabel>
                            {steps.map((label) => (
                                <Step key={label}>
                                    <StepLabel>{label}</StepLabel>
                                </Step>
                            ))}
                        </Stepper>
                        <TitleSection className="w-full !text-blue-secondary">Actividad Física y Salud</TitleSection>

                    </div>

                    <div className={`flex flex-wrap w-11/12 gap-4 ${activity ? "justify-between": "justify-items-start"}`}>

                     <CustomSwitch label="¿Hacés Actividad Física?" id="activity" onChange={() => setActivity(!activity)}
                                  width="w-1/2"/>

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
                    </div>

                    <div className="flex justify-around w-full">
                        <OrangeLink href="/initialForm/step-2" label="Atrás" width="w-1/3" background="bg-gray-400 hover:bg-gray-600"/>
                        <ButtonOrange onClick={handleSubmit} label="Siguiente" width="w-1/3"/>
                    </div>

                </div>
            </div>
        </Section>
    )
}

export default InitialFormStep3;