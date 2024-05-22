import {Section} from "../../components/section";
import {TitleSection} from "../../components/titles";
import {useState} from "react";
import {BlueLink, OrangeLink} from "../../components/link";
import {ButtonOrange} from "../../components/button";

const InitialForm = () => {




    return(
        <Section className="pt-12">
            <div className="container items-center flex w-full justify-center flex-col">
                <TitleSection className="text-white">¡Bienvenido a DiabetIA!</TitleSection>
                <div className="bg-white rounded-xl w-full flex flex-wrap text-gray-primary py-20 px-44 my-12 justify-around gap-x-2 gap-y-12">

                    <p className="text-gray-primary">Por única vez, vamos a solicitarte algunos datos para poder ser más
                        precisos con la información que te vamos a brindar</p>
                    <OrangeLink href="/initialForm/step-1" label="¡Estoy listo!" width="w-1/10"/>

                </div>
            </div>
        </Section>
)
}

export default InitialForm;