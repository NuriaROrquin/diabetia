import {MetricCard} from "../../components/card";
import {Selector} from "../../components/selector";
import {useState} from "react";
import {DASHBOARD_OPTIONS_FILTER_DAYS, DASHBOARD_INDICATORS, DASHBOARD_TIMELINE_EVENTS} from "../../constants";
import {CircleRounded} from "@mui/icons-material";
import {ContainerTitles, SubtitleSection, TitleSection} from "../../components/titles";
import {Timeline} from "../../components/timeline";
import {Section} from "../../components/section";
import {OrangeLink} from "../../components/link";
import { Tooltip } from '@mui/material';

export const Home = () => {
    const [isOpen, setIsOpen] = useState(false);
    const [selectedOption, setSelectedOption] = useState(DASHBOARD_OPTIONS_FILTER_DAYS[0])

    const handleOptionClick = (option) => {
        setSelectedOption(option);
        setIsOpen(false);
    };

    const registrarEventoTooltipText = "Registrá un nuevo evento: mediciones de glucosa, actividad física, eventos de salud, visitas médicas, insulina, comida manual.";

    return (
        <>
        <Section>
            <div className="container pt-12 flex flex-col">


                <div className="w-full col-start-2 flex justify-self-center justify-center pb-6">
                    <span className="text-xl">Tu panel de salud para la gestión de tu diabetes</span>
                </div>
                <div className="grid grid-cols-3 w-full items-center">
                <div className="w-full col-start-2 flex justify-self-center justify-center">
                        <Selector width="w-1/2" setIsOpen={setIsOpen} isOpen={isOpen} selectedOption={selectedOption}
                                  options={DASHBOARD_OPTIONS_FILTER_DAYS} handleOptionClick={handleOptionClick}/>
                    </div>
                    <Tooltip title={registrarEventoTooltipText} arrow>
                        <div className="col-start-3 justify-self-end">
                            <OrangeLink href="/event" label="Registrar Evento" width="w-1/10"/>
                        </div>
                    </Tooltip>
                </div>

                <div className="flex flex-wrap justify-around my-12 gap-x-1 gap-y-8">
                    {DASHBOARD_INDICATORS.map((data, index) => (
                        <MetricCard
                            key={index}
                            textIndicator={data.textIndicator}
                            color={data.color}
                            number={data.number}
                            title={data.title}
                            description={data.description}
                            tooltipContent={data.tooltipContent}
                            selectedOption={selectedOption}
                        />
                    ))}
                </div>

                <div className="flex justify-around bg-white w-1/2 self-center rounded-xl p-6 mt-10">
                    <div className="flex gap-2">
                        <CircleRounded className="text-green-primary"/>
                        <span className="text-gray-primary font-medium">Valores dentro de lo esperado</span>
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