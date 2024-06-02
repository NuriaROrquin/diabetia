import {MetricCard} from "../../components/card";
import {Selector} from "../../components/selector";
import {useEffect, useState} from "react";
import {DASHBOARD_OPTIONS_FILTER_DAYS, DASHBOARD_INDICATORS, DASHBOARD_TIMELINE_EVENTS} from "../../constants";
import {CircleRounded} from "@mui/icons-material";
import {ContainerTitles, SubtitleSection, TitleSection} from "../../components/titles";
import {Timeline} from "../../components/timeline";
import {Section} from "../../components/section";
import {OrangeLink} from "../../components/link";
import {getMetrics, login} from "../../services/api.service";
import {useCookies} from "react-cookie";
import CustomTooltip from "@/components/tooltip";

export const Home = () => {
    const [error, setError] = useState(false);
    const [isOpen, setIsOpen] = useState(false);
    const [selectedOption, setSelectedOption] = useState(DASHBOARD_OPTIONS_FILTER_DAYS[0])
    const [cookies, _setCookie, _removeCookie] = useCookies(['email']);
    const [metrics, setMetrics] = useState({chMetrics:0, glycemia: 99999, hyperglycemia:0, hypoglycemia:0, insulin:0, physicalActivity:0});
    const [loadingMetrics, setLoadingMetrics] = useState(true);

    useEffect(() => {
        const email = cookies.email;

        email && getMetrics({email})
            .then((res) => {
                setMetrics(res.data);
                setLoadingMetrics(false);
            })
            .catch((error) => {
                error.response.data ? setError(error.response.data) : setError("Hubo un error")
            });
    }, []);

    const handleOptionClick = (option) => {
        setSelectedOption(option);
        setIsOpen(false);
    };

    const registrarEventoTooltipText = "Registrá un nuevo evento: mediciones de glucosa, actividad física, eventos de salud, visitas médicas, insulina, comida manual.";

    return (
        <>
        <Section>
            <div className="container pt-12 flex flex-col">


                <div className="w-full flex justify-self-center justify-center pb-6">
                    <span className="text-xl text-white">Tu panel de salud para la gestión de tu diabetes</span>
                </div>
                <div className="grid grid-cols-3 w-full items-center">
                <div className="w-full col-start-2 flex justify-self-center justify-center">
                        <Selector width="w-1/2" setIsOpen={setIsOpen} isOpen={isOpen} selectedOption={selectedOption}
                                  options={DASHBOARD_OPTIONS_FILTER_DAYS} handleOptionClick={handleOptionClick}/>
                    </div>
                    <CustomTooltip title={registrarEventoTooltipText} arrow>
                        <div className="col-start-3 justify-self-end">
                            <OrangeLink href="/event" label="Registrar Evento" width="w-1/10"/>
                        </div>
                    </CustomTooltip>
                </div>

                <div className="flex flex-wrap justify-between my-12 gap-x-1 gap-y-8">
                    {DASHBOARD_INDICATORS.map((data, index) => (
                        <MetricCard
                            key={index}
                            textIndicator={data.textIndicator}
                            color={data.color}
                            number={metrics[data.key]}
                            title={data.title}
                            description={data.description}
                            tooltipContent={data.tooltipContent}
                            selectedOption={selectedOption}
                            loading={loadingMetrics}
                            state={metrics[data.key]}
                        />
                    ))}
                </div>

                <div className="flex justify-around bg-white w-1/2 self-center rounded-xl p-6 mb-10">
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
            <div className="flex justify-center mb-10">
                <Timeline events={DASHBOARD_TIMELINE_EVENTS} />
            </div>
        </Section>
        </>
    )
}

export default Home;