import {Section} from "../../../components/section";
import {TitleSection} from "../../../components/titles";
import {INSULIN_FREQUENCY, STEPS, TYPE_DIABETES, TYPE_INSULIN} from "../../../constants";
import {CustomSwitch, InputWithLabel} from "../../../components/input";
import {ButtonOrange} from "../../../components/button";
import {useRouter} from "next/router";
import {Step, StepLabel, Stepper} from "@mui/material";
import {Select} from "@/components/selector";
import {getPatientInfo, secondStep} from "../../../services/user.service";
import {useEffect, useState} from "react";
import {NavLink} from "../../../components/link"
import {getEmailFromJwt} from "../../../helpers";

const ProfileFormStep2 = () => {
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
    const [patientInfo, setPatientInfo] = useState(null);


    useEffect(() => {
        email && getPatientInfo({email})
            .then((res) => {
                setPatientInfo({...res.data});
                const insulineType = TYPE_INSULIN.find(i => i.id === res.data.typeInsuline);
                const diabetesType = TYPE_DIABETES.find(i => i.id === res.data.typeDiabetes);
                const InsulineFrequency = INSULIN_FREQUENCY.find(i => i.id === res.data.frequency);

                setInsuline(res.data.useInsuline);
                setSelectedOptionTipoInsulina(insulineType)
                setSelectedOptionTipoDiabetes(diabetesType)
                setSelectedOptionFrecuenciaInsulina(InsulineFrequency)
            })
            .catch((error) => {
                error.response ? setError(error.response) : setError("Hubo un error")
            });
    }, []);

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

    const handleSubmit = () => {
        const typeDiabetes = selectedOptionTipoDiabetes.id;
        const useInsuline = insuline;
        const typeInsuline = selectedOptionTipoInsulina.id;
        const frequency = selectedOptionFrecuenciaInsulina.id;
        const needsReminder = reminder;
        const hourReminder = hour ? hour.format('HH:mm') : "00:00";

        secondStep({email, typeDiabetes, useInsuline, typeInsuline, frequency, needsReminder, hourReminder})
            .then((res) => {
                if(res){
                    router.push("/profile/step-2")
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
                            {STEPS.map((step, index) => (
                                <Step key={index}>

                                    <StepLabel><NavLink href={step.url} text={step.title} className="!no-underline !text-gray-secondary text-sm"/></StepLabel>

                                </Step>
                            ))}
                        </Stepper>
                        <TitleSection className="w-full !text-blue-secondary">Datos Personales</TitleSection>

                    </div>
                    <Select label="Tipo de Diabetes" placeholder="¿Qué tipo de diabetes tenés?" options={TYPE_DIABETES}
                            selectedOption={selectedOptionTipoDiabetes && selectedOptionTipoDiabetes}
                            handleOptionClick={handleOptionClickTipoDiabetes} setIsOpen={setIsOpenTipoDiabetes}
                            isOpen={isOpenTipoDiabetes} width="w-1/3"/>
                    <CustomSwitch label="¿Usás insulina?" id="insulin" onChange={() => setInsuline(!insuline)}
                                  width="w-1/3" checked={insuline && insuline}/>
                    {insuline && (
                        <>
                            <Select label="Tipo de Insulina" placeholder="Indicá tipo de insulina"
                                    options={TYPE_INSULIN} selectedOption={selectedOptionTipoInsulina && selectedOptionTipoInsulina}
                                    handleOptionClick={handleOptionClickTipoInsulina} setIsOpen={setIsOpenTipoInsulina}
                                    isOpen={isOpenTipoInsulina} width="w-1/3"/>
                            <Select label="Frecuencia de inyecciones" placeholder="Indicá tipo de insulina"
                                    options={INSULIN_FREQUENCY} selectedOption={selectedOptionFrecuenciaInsulina && selectedOptionFrecuenciaInsulina}
                                    handleOptionClick={handleOptionClickFrecuenciaInsulina}
                                    setIsOpen={setIsOpenFrecuenciaInsulina} isOpen={isOpenFrecuenciaInsulina}
                                    width="w-1/3"/>
                        </>
                    )}

                    {error && <span className="text-red-500 mb-3">{error}</span>}

                    <ButtonOrange onClick={handleSubmit} label="Actualizar" width="w-1/3"/>
                </div>
            </div>
        </Section>
    )
}

export default ProfileFormStep2;