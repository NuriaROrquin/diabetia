import {Section} from "@/components/section";
import {MetricCard} from "@/components/card";
import {useRouter} from "next/router";
import {OrangeLink} from "@/components/link";

const Final = () => {
    const router = useRouter()

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

                    <div className="flex w-full justify-center gap-8">
                    {router.query && router.query.chConsumed ?
                        <MetricCard
                            key="chCalculated"
                            textIndicator=""
                            unit="gr"
                            number={router.query.chConsumed}
                            title="Carbohidratos Consumidos"
                            description="Estos son los carbohidratos que se han detectado según el algoritmo de cálculo"
                            isWarning={null}
                        />
                        :
                        <span>Ocurrió un error calculando los carbohidratos. Intentá recargar la página</span>
                    }

                    {router.query && router.query.insulinRecomended ?
                        <MetricCard
                            key="insulinRecomended"
                            textIndicator=""
                            unit="u"
                            number={router.query.insulinRecomended}
                            title="Insulina Recomendada"
                            description="Esta es la insulina recomendada en base a tu formulario inicial"
                            isWarning={null}
                        />
                        :
                        <span>Ocurrió un error calculando la insulina. Intentá recargar la página</span>
                    }
                    </div>


                    <OrangeLink href="/" label="Ir al home" width="w-1/3"/>


                </div>
            </div>
        </Section>
    )
}

export default Final;