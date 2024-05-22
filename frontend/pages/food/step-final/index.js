import {Section} from "@/components/section";
import {useAIData} from "../../../context";
import {MetricCard} from "@/components/card";
import {OrangeLink} from "../../../components/link";

const StepFinal = () => {
    const { finalCalcCarbos } = useAIData();

    return(
        <Section>
            <div className="container">
                <div
                    className="bg-white rounded-xl w-full flex flex-col flex-wrap text-gray-primary py-20 px-44 my-12 justify-around gap-x-2 gap-y-12 items-center">

                    <h4 className="font-semibold text-3xl text-center text-blue-primary">Resultados</h4>

                    <p className="font-light text-xl text-center text-gray-primary">
                        Estos son los carbohidratos calculados en base a la información proporcionada para cada registro
                        nutricional. Para cada registro, se considera la cantidad consumida, el peso de la porción y los
                        carbohidratos presentes en esa porción. A partir de estos datos, se determina la cantidad de
                        carbohidratos consumidos para cada registro.
                        El total de carbohidratos consumidos se obtiene sumando los carbohidratos calculados para cada
                        registro individual. Este total representa la cantidad total de carbohidratos consumidos.
                    </p>

                    {finalCalcCarbos && finalCalcCarbos[0] && finalCalcCarbos[0].chTotal ?
                        <MetricCard
                            key="chCalculated"
                            textIndicator=""
                            unit="gr"
                            number={finalCalcCarbos && finalCalcCarbos[0] && finalCalcCarbos[0].chTotal}
                            title="Carbohidratos Consumidos"
                            description="Estos son los carbohidratos que se han detectado según el algoritmo de cálculo"
                        />
                        :
                        <span>Ocurrió un error calculando los carbohidratos. Intentá recargar la página</span>
                    }


                        <OrangeLink href="/" label="Ir al home" width="w-1/3"/>


                </div>
            </div>
        </Section>
    )
}

export default StepFinal;