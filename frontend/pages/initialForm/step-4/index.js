import {Section} from "../../../components/section";
import {TitleSection} from "../../../components/titles";
import {
    TYPE_DEVICES, MANY_MEASUREMENTS
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
import {fourthStep} from "../../../services/api.service";
import {useRouter} from "next/router";

const InitialFormStep4 = () => {
    const [error, setError] = useState(false);
    const [selectedOptionDevices, setSelectedOptionDevices] = useState(null);
    const [selectedOptionManyMeasurements, setSelectedOptionManyMeasurements] = useState(null);
    const [isOpenDevices, setIsOpenDevices] = useState(false);
    const [isOpenManyMeasurements, setIsOpenManyMeasurements] = useState(false);
    const router = useRouter();
    const [device, setDevice] = useState(false);
    const [cookies, _setCookie, _removeCookie] = useCookies(['email']);
    const [reminder, setReminder] = useState(false);
    const [hour, setHour] = useState();
    const email = cookies.email


    const handleOptionClickDevices = (option) => {
        setSelectedOptionDevices(option);
        setIsOpenDevices(false);
    };

    const handleOptionClickManyMeasurements = (option) => {
        setSelectedOptionManyMeasurements(option);
        setIsOpenManyMeasurements(false);
    };


    const steps = [
        'Datos personales',
        'Datos del paciente',
        'Actividad física y salud',
        'Dispositivos y sensores',
    ];


    const handleSubmit = () => {
        const tieneDispositivo = device;
        const idDispositivo = selectedOptionDevices.id ;
        const frecuencia = selectedOptionManyMeasurements.id;


        fourthStep({email, tieneDispositivo, idDispositivo, frecuencia})
            .then((res) => {
                if(res){
                    router.push("/dashboard")
                }
            })
            .catch((error) => {
                console.error('Error in fourthStep:', error);
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
                        <Stepper activeStep={3} alternativeLabel>
                            {steps.map((label) => (
                                <Step key={label}>
                                    <StepLabel>{label}</StepLabel>
                                </Step>
                            ))}
                        </Stepper>
                        <TitleSection className="w-full !text-blue-secondary">Dispositivos y Sensores</TitleSection>

                    </div>

                    <div className={`flex flex-wrap w-11/12 gap-4 ${device ? "justify-between": "justify-items-start"}`}>

                        <CustomSwitch label="¿Tenés dispositivo para medir la glucosa?" id="device" onChange={() => setDevice(!device)}
                                      width="w-1/3"/>

                        {device && (
                            <>
                                <Select label="¿Qué tipo es?" placeholder="Indica qué tipo utilizas"
                                        options={TYPE_DEVICES} selectedOption={selectedOptionDevices}
                                        handleOptionClick={handleOptionClickDevices}
                                        setIsOpen={setIsOpenDevices} isOpen={isOpenDevices}
                                        width="w-1/3"/>

                                <Select label="¿Cuántas veces por día hacés mediciones?" placeholder="Indica cuantas mediciones realizas"
                                        options={MANY_MEASUREMENTS} selectedOption={selectedOptionManyMeasurements}
                                        handleOptionClick={handleOptionClickManyMeasurements}
                                        setIsOpen={setIsOpenManyMeasurements} isOpen={isOpenManyMeasurements}
                                        width="w-1/3"/>


                                <div className="flex justify-between w-full mt-4">
                                    <CustomSwitch label="¿Querés un recordatorio de aplicación?" id="reminder"
                                                  onChange={() => setReminder(!reminder)} width="w-1/3" checked={reminder}/>
                                    {reminder && (
                                    <CustomTimePicker
                                        label="Hora del recordatorio"
                                        value={hour}
                                        onChange={(e) => setHour(e)}
                                        defaultValue={dayjs()}
                                        width="w-1/3"
                                        className={reminder ? '' : 'hidden'}
                                    />
                                    )}
                                </div>
                            </>
                        )}
                    </div>

                    <OrangeLink href="/initialForm/step-3" label="Atrás" width="w-1/3"/>
                    <ButtonOrange onClick={handleSubmit} label="Finalizar" width="w-1/3"/>
                </div>
            </div>
        </Section>
    )
}

export default InitialFormStep4;