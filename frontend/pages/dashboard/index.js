import {Card} from "../../components/card";
import {Selector} from "../../components/selector";
import Link from "next/link";

export const Home = () => {
    return (
        <section className="w-full min-h-screen">
            <div className="container pt-12">
                <div className="grid grid-cols-3 w-full">
                    <div className="w-full col-start-2 flex justify-self-center justify-center">
                        <Selector width="w-1/2" />
                    </div>
                    <div className="col-start-3 justify-self-end">
                        <Link className="bg-orange-focus hover:bg-orange-primary transition-all text-white py-2 px-8 rounded-full w-full" href="/event">Registrar evento</Link>
                    </div>
                </div>

                <div className="flex flex-wrap justify-around mt-12 gap-x-1 gap-y-8">
                    <Card color="green" number={115} textIndicator="Glucemia" title="Promedio de medidas" description="Cuando midas tu glucosa en sangre, tu promedio se verá afectado." />
                    <Card color="green" number={0} textIndicator="Hiperglucemias" title="En total" description="Nivel de glucosa en sangre demasiado alta." />
                    <Card color="red" number={1} textIndicator="Hipoglucemias" title="En total" description="Nivel de glucosa en sangre demasiado baja." />
                    <Card color="blue" number={102} unit="G" textIndicator="Carbohidratos" title="En total" description="Cuando cargues una comida, este numero se vera afectado." />
                    <Card color="blue" number={7} unit="U" textIndicator="Insulina" title="En total" description="5u insulina rápida y 2u insulina lenta" />
                    <Card color="red" number={25} unit="Min" textIndicator="Ejercicio" title="En total" description="La recomendación son 30 min de ejercicio diario" />
                </div>
            </div>
        </section>
    )
}

export default Home;