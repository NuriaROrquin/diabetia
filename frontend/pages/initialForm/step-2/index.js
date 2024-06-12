import {Section} from "../../../components/section";
import {TitleSection} from "../../../components/titles";
import {TYPE_INSULIN, TYPE_DIABETES, INSULIN_FREQUENCY} from "../../../constants";
import {useState} from "react";
import {OrangeLink} from "../../../components/link";
import {CustomSwitch, InputWithLabel} from "../../../components/input";
import {Select} from "../../../components/selector";
import dayjs from "dayjs";
import {ButtonOrange} from "../../../components/button";
import {CustomTimePicker} from "../../../components/pickers";
import {Step, StepLabel, Stepper} from "@mui/material";
import {secondStep} from "../../../services/api.service";
import {useRouter} from "next/router";
import {getEmailFromJwt} from "../../../helpers";

const InitialFormStep2 = () => {
    const [error, setError] = useState(false);
    const [isOpenTipoDiabetes, setIsOpenTipoDiabetes] = useState(false);
    const [selectedOptionTipoDiabetes, setSelectedOptionTipoDiabetes] = useState(null);
    const [isOpenTipoInsulina, setIsOpenTipoInsulina] = useState(false);
    const [selectedOptionFrecuenciaInsulina, setSelectedOptionFrecuenciaInsulina] = useState(null);
    const [isOpenFrecuenciaInsulina, setIsOpenFrecuenciaInsulina] = useState(false);
    const [selectedOptionTipoInsulina, setSelectedOptionTipoInsulina] = useState(null);
    const router = useRouter();
    const [insuline, setInsuline] = useState(false);
    const [reminder, setReminder] = useState(false);
    const [hour, setHour] = useState();
    const email = getEmailFromJwt();

    const handleOptionClickTipoDiabetes = (option) => {
        setSelectedOptionTipoDiabetes(option);
        setIsOpenTipoDiabetes(false);
    };

    const handleOptionClickTipoInsulina = (option) => {
        setSelectedOptionTipoInsulina(option);
        setIsOpenTipoInsulina(false);
    };

    const handleOptionClickFrecuenciaInsulina = (option) => {
        setSelectedOptionFrecuenciaInsulina(option);
        setIsOpenFrecuenciaInsulina(false);
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
        const typeInsuline = selectedOptionTipoInsulina ? selectedOptionTipoInsulina.id : null;
        const frequency = selectedOptionTipoInsulina ? selectedOptionFrecuenciaInsulina.id : null ;
        const needsReminder = reminder;
        const hourReminder = hour ? hour.format('HH:mm') : null;
        const insulinePerCHElement = document.getElementById("insulinePerCH");
        const insulinePerCHValue = insulinePerCHElement ? insulinePerCHElement.value : null;
        const insulinePerCH = insulinePerCHValue ? parseInt(insulinePerCHValue, 10) : null;

        secondStep({email, typeDiabetes, useInsuline, typeInsuline, frequency, needsReminder, hourReminder, insulinePerCH})
            .then((res) => {
            if(res){
                router.push("/initialForm/step-3")
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
                        <TitleSection className="w-full !text-blue-secondary">Datos del Paciente</TitleSection>

                    </div>
                    <Select label="Tipo de Diabetes" placeholder="¿Qué tipo de diabetes tenés?" options={TYPE_DIABETES}
                            selectedOption={selectedOptionTipoDiabetes}
                            handleOptionClick={handleOptionClickTipoDiabetes} setIsOpen={setIsOpenTipoDiabetes}
                            isOpen={isOpenTipoDiabetes} width="w-1/3"/>
                    <CustomSwitch label="¿Usás insulina?" id="insulin" onChange={() => setInsuline(!insuline)}
                                  width="w-1/3"/>
                    {insuline && (
                        <>
                            <Select label="Tipo de Insulina" placeholder="Indicá tipo de insulina"
                                    options={TYPE_INSULIN} selectedOption={selectedOptionTipoInsulina}
                                    handleOptionClick={handleOptionClickTipoInsulina} setIsOpen={setIsOpenTipoInsulina}
                                    isOpen={isOpenTipoInsulina} width="w-1/3"/>

                            <Select label="Frecuencia de inyecciones" placeholder="Indicá tipo de insulina"
                                    options={INSULIN_FREQUENCY} selectedOption={selectedOptionFrecuenciaInsulina}
                                    handleOptionClick={handleOptionClickFrecuenciaInsulina}
                                    setIsOpen={setIsOpenFrecuenciaInsulina} isOpen={isOpenFrecuenciaInsulina}
                                    width="w-1/3"/>

                            <div className="w-10/12 flex justify-start">
                            <InputWithLabel label="Carbohidratos por unidad de insulina"
                                            placeholder="Indicá los gr de CH por unidad de insulina"
                                            id="insulinePerCH"
                                            width="w-2/5" type="number"/>
                            </div>

                            <div className={`flex flex-wrap w-10/12 gap-4 ${reminder ? "justify-between": "justify-items-start"}`}>

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
                    <div className="flex justify-around w-full">
                        <OrangeLink href="/initialForm/step-1" label="Atrás" width="w-1/3" background="bg-gray-400 hover:bg-gray-600"/>
                        <ButtonOrange onClick={handleSubmit} label="Finalizar" width="w-1/3"/>
                    </div>
                </div>
            </div>
        </Section>
    )
}

export default InitialFormStep2;