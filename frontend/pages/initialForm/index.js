import {OrangeLink} from "../../components/link";
import {Section} from "@/components/section";

const InitialForm = () => {
    return(
        <Section className="pt-12">
            <div className="container items-center flex w-full justify-center flex-col">
                <div className="bg-white rounded-xl w-full flex flex-col flex-wrap text-gray-primary py-20 px-44 my-12 justify-around gap-x-2 gap-y-12">
        <h1>Bienvenido, por única vez te haremos unas preguntas para iniciar con la aplicación</h1>
        <OrangeLink label="De acuerdo" href="initialForm/step-1"/>
                </div>
            </div>
        </Section>
    )
}

export default InitialForm;