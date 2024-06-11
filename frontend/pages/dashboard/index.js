import {MetricCard} from "../../components/card";
import {Selector} from "../../components/selector";
import {useEffect, useState} from "react";
import {DASHBOARD_OPTIONS_FILTER_DAYS, DASHBOARD_INDICATORS, DASHBOARD_TIMELINE_EVENTS} from "../../constants";
import {CircleRounded} from "@mui/icons-material";
import {ContainerTitles, SubtitleSection, TitleSection} from "../../components/titles";
import {Timeline} from "../../components/timeline";
import {Section} from "../../components/section";
import {OrangeLink} from "../../components/link";
import {getMetrics, getTimeline} from "../../services/api.service";
import {useCookies} from "react-cookie";
import CustomTooltip from "@/components/tooltip";
import {calculateDateFilter, calculateDateRange, getEmailFromJwt} from "../../helpers";

export const Home = () => {
    const [error, setError] = useState(false);
    const [isOpen, setIsOpen] = useState(false);
    const [selectedOption, setSelectedOption] = useState(DASHBOARD_OPTIONS_FILTER_DAYS[0])
    const [cookies, _setCookie, _removeCookie] = useCookies(['email']);
    const [metrics, setMetrics] = useState({chMetrics:0, glycemia: 99999, hyperglycemia:0, hypoglycemia:0, insulin:0, physicalActivity:0});
    const [loadingMetrics, setLoadingMetrics] = useState(true);
    const [loadingTimeline, setLoadingTimeline] = useState(true);
    const [eventsTimeline, setEventsTimeline] = useState(true);

    const email = getEmailFromJwt();

    useEffect(() => {
        setLoadingTimeline(true)
        email && getTimeline(email)
            .then((res) => {
                setEventsTimeline(res.data);
                setLoadingTimeline(false);
            })
            .catch((error) => {
                error.response.data ? setError(error.response.data) : setError("Hubo un error")
            });
    }, []);

    useEffect(() => {
        const { dateFrom, dateTo } = calculateDateRange(selectedOption);
        setLoadingMetrics(true)
        email && getMetrics({email, dateFilter: {dateFrom, dateTo}})
            .then((res) => {
                setMetrics(res.data);
                setLoadingMetrics(false);
            })
            .catch((error) => {
                console.log(error)
                error.response.data ? setError(error.response.data) : setError("Hubo un error")
            });
    }, [selectedOption]);

    const handleOptionClick = (option) => {
        setSelectedOption(option);
        setIsOpen(false);
    };

    const registrarEventoTooltipText = "Registrá un nuevo evento: mediciones de glucosa, actividad física, eventos de salud, visitas médicas, insulina, comida manual.";

    return (
        <>
        <Section>
            <div className="container pt-20 flex flex-col">
                <div className="w-full flex flex-col justify-self-center justify-center pb-6">
                    <TitleSection className="text-white">Tu panel de salud para la gestión de tu diabetes</TitleSection>
                    <SubtitleSection className="text-white mt-4">Visualizá tus métricas según los registros cargados</SubtitleSection>
                </div>
                <div className="flex flex-col md:flex-row gap-3 md:gap-0">
                    <div className="w-full flex justify-self-start">
                        <Selector width="w-full md:w-1/3 lg:w-1/5" setIsOpen={setIsOpen} isOpen={isOpen} selectedOption={selectedOption}
                                  options={DASHBOARD_OPTIONS_FILTER_DAYS} handleOptionClick={handleOptionClick}/>
                    </div>
                    <CustomTooltip title={registrarEventoTooltipText} arrow>
                        <div className="justify-self-end">
                            <OrangeLink href="/event" label="Registrar Evento" width="w-full lg:w-1/5"/>
                        </div>
                    </CustomTooltip>
                </div>

                <div className="flex flex-col lg:grid lg:grid-cols-3 bg-white rounded-xl px-8 py-4 my-12 gap-8 w-full">
                    <div className="flex gap-2 col-start-1 lg:place-self-center items-center">
                        <CircleRounded className="text-green-primary"/>
                        <span className="text-gray-primary font-medium text-xl">Valores dentro de lo esperado</span>
                    </div>
                    <div className="flex gap-2 col-start-2 lg:place-self-center items-center">
                        <CircleRounded className="text-red-primary"/>
                        <span className="text-gray-primary font-medium text-xl">Cuidado! Prestale atención</span>
                    </div>
                    <div className="flex gap-2 col-start-3 lg:place-self-center items-center">
                        <CircleRounded className="text-blue-primary"/>
                        <span className="text-gray-primary font-medium text-xl">Informativo</span>
                    </div>
                </div>

                <div className="flex flex-wrap mb-12 gap-x-1 gap-y-8 justify-around xl:justify-between">
                    {metrics && DASHBOARD_INDICATORS.map((data, index) => {
                        return (
                        <MetricCard
                            key={index}
                            textIndicator={data.textIndicator}
                            color={data.color}
                            number={metrics[data.key] && metrics[data.key].quantity}
                            title={data.title}
                            description={data.description}
                            tooltipContent={data.tooltipContent}
                            selectedOption={selectedOption}
                            loading={loadingMetrics}
                            isWarning={metrics[data.key] && metrics[data.key].isWarning}
                            unit={data.unit}
                        />)
                    }
                    )}
                </div>


            </div>
        </Section>
        <Section className="bg-white flex flex-col min-h-fit">
            <ContainerTitles>
                <TitleSection>Registros de hoy</TitleSection>
                <SubtitleSection>Acá encontrarás todos los registros cargados en el día actual</SubtitleSection>
            </ContainerTitles>
                {!loadingTimeline && <Timeline events={eventsTimeline.timeline.items} />}
        </Section>
        </>
    )
}

export default Home;