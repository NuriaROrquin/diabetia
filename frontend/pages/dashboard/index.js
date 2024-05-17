import {Card} from "../../components/card";
import {Selector} from "../../components/selector";
import Link from "next/link";
import {useState} from "react";
import {DASHBOARD_OPTIONS_FILTER_DAYS, DASHBOARD_INDICATORS, DASHBOARD_TIMELINE_EVENTS} from "../../constants";
import {CircleRounded} from "@mui/icons-material";
import {ContainerTitles, SubtitleSection, TitleSection} from "../../components/titles";
import {Timeline} from "../../components/timeline";
import {Section} from "../../components/section";

export const Home = () => {
    const [isOpen, setIsOpen] = useState(false);
    const [selectedOption, setSelectedOption] = useState(DASHBOARD_OPTIONS_FILTER_DAYS[0])

    const handleOptionClick = (option) => {
        setSelectedOption(option);
        setIsOpen(false);
    };

    return (
        <>
        <Section>
            <div className="container pt-12 flex flex-col">
                <div className="grid grid-cols-3 w-full items-center">
                    <div className="w-full col-start-2 flex justify-self-center justify-center">
                        <Selector width="w-1/2" setIsOpen={setIsOpen} isOpen={isOpen} selectedOption={selectedOption} options={DASHBOARD_OPTIONS_FILTER_DAYS} handleOptionClick={handleOptionClick} />
                    </div>
                    <div className="col-start-3 justify-self-end">
                        <Link href="/event" className="bg-orange-focus hover:bg-orange-primary transition-all text-white py-2 px-8 rounded-full w-full" href="/event">Registrar evento</Link>
                    </div>
                </div>

                <div className="flex flex-wrap justify-around my-12 gap-x-1 gap-y-8">
                    {DASHBOARD_INDICATORS.map((data, index) => (
                        <Card
                            key={index}
                            textIndicator={data.textIndicator}
                            color={data.color}
                            number={data.number}
                            title={data.title}
                            description={data.description}
                        />
                    ))}
                </div>

                <div className="flex justify-around bg-white w-1/2 self-center rounded-xl p-4 mt-10">
                    <div className="flex gap-2">
                        <CircleRounded className="text-green-primary"/>
                        <span className="text-gray-primary font-medium">Bien! Valores correctos</span>
                    </div>
                    <div className="flex gap-2">
                        <CircleRounded className="text-red-primary"/>
                        <span className="text-gray-primary font-medium">Cuidado! Prestale atención</span>
                    </div>
                    <div className="flex gap-2">
                        <CircleRounded className="text-blue-primary"/>
                        <span className="text-gray-primary font-medium">Informativo</span>
                    </div>
                </div>
            </div>
        </Section>
            <Section className="bg-white flex flex-col">
                <ContainerTitles>
                    <TitleSection>Registros de hoy</TitleSection>
                    <SubtitleSection>Acá encontrarás todos los registros cargados en el día actual</SubtitleSection>
            </ContainerTitles>
            <div className="flex justify-center">
                <Timeline events={DASHBOARD_TIMELINE_EVENTS} />
            </div>
        </Section>
        </>
    )
}

export default Home;