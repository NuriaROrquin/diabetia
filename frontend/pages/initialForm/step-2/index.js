import {Section} from "../../../components/section";
import {TitleSection} from "../../../components/titles";
import {TYPE_INSULIN, TYPE_DIABETES, INSULIN_FREQUENCY} from "../../../constants";
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

const InitialFormStep2 = () => {
    const [isOpenTipoDiabetes, setIsOpenTipoDiabetes] = useState(false);
    const [selectedOptionTipoDiabetes, setSelectedOptionTipoDiabetes] = useState(null);
    const [isOpenTipoInsulina, setIsOpenTipoInsulina] = useState(false);
    const [selectedOptionFrecuenciaInsulina, setSelectedOptionFrecuenciaInsulina] = useState(null);
    const [isOpenFrecuenciaInsulina, setIsOpenFrecuenciaInsulina] = useState(false);
    const [selectedOptionTipoInsulina, setSelectedOptionTipoInsulina] = useState(null);
    const [date, setDate] = useState()
    const [insuline, setInsuline] = useState(false)
    const [reminder, setReminder] = useState(false);
    const [Hour, setHour] = useState()
    const [_cookies, setCookie, _removeCookie] = useCookies(['cookie-name']);

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
        const tipoDiabetes = selectedOptionTipoDiabetes;
        const usaInsulina = insuline;
        const tipoInsulina = selectedOptionTipoInsulina;
        const FrecuenciaInsulina = selectedOptionFrecuenciaInsulina;
        const quiereReminder = reminder;
        const dateFormatted = date ? date.format('DD-MM-YYYY') : null;

        console.log("Datos del formulario:", tipoDiabetes, usaInsulina, tipoInsulina, FrecuenciaInsulina, quiereReminder, dateFormatted);
        setCookie("informationCompleted", true, {path: "/", expires: new Date(Date.now() + 1000 * 60 * 60 * 24 * 30)});
        window.location.href = '/';
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
                        <TitleSection className="w-full !text-blue-secondary">Datos Personales</TitleSection>

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
                            <CustomSwitch label="¿Querés un recordatorio de aplicación?" id="reminder"
                                          onChange={() => setReminder(!reminder)} width="w-1/3"/>
                            <CustomTimePicker
                                label="Hora del recordatorio"
                                value={Hour}
                                onChange={(e) => setHour(e)}
                                defaultValue={dayjs()}
                                width="w-1/3"
                                className={reminder ? '' : 'hidden'}
                            />
                        </>
                    )}
                    <OrangeLink href="/initialForm/step-1" label="Atrás" width="w-1/3"/>
                    <ButtonOrange onClick={handleSubmit} label="Finalizar" width="w-1/3"/>
                </div>
            </div>
        </Section>
    )
}

export default InitialFormStep2;