import {MetricCard} from "../../components/card";
import {Selector} from "../../components/selector";
import {useEffect, useState} from "react";
import {DASHBOARD_OPTIONS_FILTER_DAYS, DASHBOARD_INDICATORS} from "../../constants";
import {CircleRounded} from "@mui/icons-material";
import {ContainerTitles, SubtitleSection, TitleSection} from "../../components/titles";
import {Timeline} from "../../components/timeline";
import {Section} from "../../components/section";
import {OrangeLink} from "../../components/link";
import {useCookies} from "react-cookie";
import CustomTooltip from "../../components/tooltip";
import {calculateDateRange} from "../../helpers";
import {getMetrics, getTimeline} from "../../services/home.service";

export const Home = () => {
    const [error, setError] = useState(false);
    const [isOpen, setIsOpen] = useState(false);
    const [selectedOption, setSelectedOption] = useState(DASHBOARD_OPTIONS_FILTER_DAYS[0])
    const [cookies, _setCookie, _removeCookie] = useCookies(['email']);
    const [metrics, setMetrics] = useState({chMetrics:0, glycemia: 99999, hyperglycemia:0, hypoglycemia:0, insulin:0, physicalActivity:0});
    const [loadingMetrics, setLoadingMetrics] = useState(true);
    const [loadingTimeline, setLoadingTimeline] = useState(true);
    const [eventsTimeline, setEventsTimeline] = useState(true);

    useEffect(() => {
        setLoadingTimeline(true)
        getTimeline()
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
        getMetrics(dateFrom, dateTo)
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
                        <Selector dataTestId="selector-button" width="w-full md:w-1/3 lg:w-1/5" setIsOpen={setIsOpen} isOpen={isOpen} selectedOption={selectedOption}
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
                        <span className="text-gray-primary font-medium text-xl">¡Cuidado! Prestale atención</span>
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
                <TitleSection>Registros del día</TitleSection>
                <SubtitleSection>Acá encontrarás todos los registros cargados en el día actual</SubtitleSection>
            </ContainerTitles>
                {loadingTimeline &&
                    <div className="w-full flex justify-center items-center mb-5">
                        <svg aria-hidden="true"
                             className="inline w-10 h-10 text-blue-secondary animate-spin dark:text-blue-secondary fill-white"
                             viewBox="0 0 100 101" fill="none" xmlns="http://www.w3.org/2000/svg">
                            <path
                                d="M100 50.5908C100 78.2051 77.6142 100.591 50 100.591C22.3858 100.591 0 78.2051 0 50.5908C0 22.9766 22.3858 0.59082 50 0.59082C77.6142 0.59082 100 22.9766 100 50.5908ZM9.08144 50.5908C9.08144 73.1895 27.4013 91.5094 50 91.5094C72.5987 91.5094 90.9186 73.1895 90.9186 50.5908C90.9186 27.9921 72.5987 9.67226 50 9.67226C27.4013 9.67226 9.08144 27.9921 9.08144 50.5908Z"
                                fill="currentColor"/>
                            <path
                                d="M93.9676 39.0409C96.393 38.4038 97.8624 35.9116 97.0079 33.5539C95.2932 28.8227 92.871 24.3692 89.8167 20.348C85.8452 15.1192 80.8826 10.7238 75.2124 7.41289C69.5422 4.10194 63.2754 1.94025 56.7698 1.05124C51.7666 0.367541 46.6976 0.446843 41.7345 1.27873C39.2613 1.69328 37.813 4.19778 38.4501 6.62326C39.0873 9.04874 41.5694 10.4717 44.0505 10.1071C47.8511 9.54855 51.7191 9.52689 55.5402 10.0491C60.8642 10.7766 65.9928 12.5457 70.6331 15.2552C75.2735 17.9648 79.3347 21.5619 82.5849 25.841C84.9175 28.9121 86.7997 32.2913 88.1811 35.8758C89.083 38.2158 91.5421 39.6781 93.9676 39.0409Z"
                                fill="currentFill"/>
                        </svg>
                    </div>
                }
                {!loadingTimeline && <Timeline events={eventsTimeline.timeline.items} />}
        </Section>
        </>
    )
}

export default Home;