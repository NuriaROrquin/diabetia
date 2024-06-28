import {Section} from "@/components/section";
import {ChartAreaComponent, ChartMultipleLineComponent} from "@/components/chart";
import {TitleSection} from "@/components/titles";
import {getComparativeQuantityEvents, getInsulineChartData} from "../../services/reporting.service";

const ReportingPage = () => {
    return(
        <Section className="">
            <div className="container items-center flex w-full justify-center flex-col">
                <TitleSection className="text-white my-20">Reportes</TitleSection>
            </div>
            <div className="flex flex-col gap-y-10 pb-20 w-full">
            <div>
                <div className="p-4 bg-white container rounded-xl overflow-hidden">
                    <ChartAreaComponent title="Insulina" helper="Esta es la cantidad de insulina inyectada"
                                        getChartData={getInsulineChartData}></ChartAreaComponent>
                </div>
            </div>
            <div className="p-4 bg-white container rounded-xl overflow-hidden">
                <ChartMultipleLineComponent title="Carga de eventos" helper="Cantidad de eventos cargados de los distintos tipos"
                                    getComparative={getComparativeQuantityEvents}></ChartMultipleLineComponent>
            </div>
            </div>
        </Section>
    )
}

export default ReportingPage;