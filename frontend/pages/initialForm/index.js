import {Section} from "../../components/section";
import {TitleSection} from "../../components/titles";
import {useState} from "react";
import {BlueLink, OrangeLink} from "../../components/link";
import {ButtonOrange} from "../../components/button";

const InitialForm = () => {




    return(
        <Section className="pt-12">
            <div className="container items-center flex w-full justify-center flex-col">
                <TitleSection className="text-white">Bienvenido a DiabetIA!</TitleSection>

                <p className="text-gray-primary">Por única vez, vamos a solicitarte algunos datos para poder ser más
                    precisos con la información que te vamos a brindar</p>

            </div>
        </Section>
    )
}

export default InitialForm;