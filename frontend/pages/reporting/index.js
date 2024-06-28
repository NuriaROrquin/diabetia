import {Section} from "@/components/section";
import {ChartAreaComponent, ChartMultipleLineComponent, ChartPieComponent} from "@/components/chart";
import {TitleSection} from "@/components/titles";
import {
    getComparativeQuantityEvents,
    getInsulineChartData,
    getPhysicalActivityChartData
} from "../../services/reporting.service";

const ReportingPage = () => {
    return(
        <Section className="">
            <div className="container items-center flex w-full justify-center flex-col">
                <TitleSection className="text-white my-20">Reportes</TitleSection>
            </div>
            <div className="flex flex-col gap-y-10 pb-20 w-full">
                <div className="container flex justify-between gap-x-4 gap-y-10 lg:gap-y-0 flex-col lg:flex-row">
                    <div className="w-full lg:w-3/5 p-4 bg-white rounded-xl overflow-hidden">
                        <ChartAreaComponent title="Insulina" helper="Cantidad de insulina inyectada"
                                            getChartData={getInsulineChartData}></ChartAreaComponent>
                    </div>
                    <div className="w-full lg:w-2/5 p-4 bg-white rounded-xl overflow-hidden">
                        <ChartPieComponent title="Actividades" helper="Duración de ejercicio por actividad"
                                           getChartData={getPhysicalActivityChartData}></ChartPieComponent>
                    </div>
                </div>
                <div className="p-4 bg-white container rounded-xl overflow-hidden">
                    <ChartMultipleLineComponent title="Carga de eventos"
                                                helper="Cantidad de eventos cargados de los distintos tipos"
                                                getComparative={getComparativeQuantityEvents}></ChartMultipleLineComponent>
                </div>

            </div>
        </Section>
    )
}

export default ReportingPage;