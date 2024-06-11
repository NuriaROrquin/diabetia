import { Section } from "../../components/section";
import { TitleSection } from "../../components/titles";
import { useState, useEffect } from "react";
import { useRouter } from "next/router";
import { OrangeLink } from "../../components/link";

const InitialForm = () => {
    const router = useRouter();
    const [stepCompleted, setStepCompleted] = useState(null);

    useEffect(() => {
        const stepCompletedFromStorage = sessionStorage.getItem('stepCompleted');
        setStepCompleted(stepCompletedFromStorage);
    }, []);


    const generateStepUrl = () => {
        switch (stepCompleted) {
            case null:
                return "/initialForm/step-1";
            case "1":
                return "/initialForm/step-2";
            case "2":
                return "/initialForm/step-3";
            case "3":
                return "/initialForm/step-4";
            default:
                console.error("Valor de stepCompleted no reconocido");
                return "/initialForm/step-1";
        }
    };

    return (
        <Section className="pt-12">
            <div className="container items-center flex w-full justify-center flex-col">
                <TitleSection className="text-white">¡Bienvenido a DiabetIA!</TitleSection>
                <div className="bg-white rounded-xl w-full flex flex-wrap text-gray-primary py-20 px-44 my-12 justify-around gap-x-2 gap-y-12">
                    <p className="text-gray-primary">
                        Por única vez, vamos a solicitarte algunos datos para poder ser más precisos con la información que te vamos a brindar. Queremos asegurarnos de contar con todos los detalles necesarios para ofrecerte las mejores recomendaciones personalizadas en el futuro. Agradecemos tu colaboración y confianza en DiabetIA.
                    </p>
                    <OrangeLink href={generateStepUrl()} label="¡Estoy listo!" width="w-1/10" />
                </div>
            </div>
        </Section>
    );
};

export default InitialForm;
