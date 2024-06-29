import {Section} from "@/components/section";
import {SubtitleSection, TitleSection} from "@/components/titles";
import {NavLink} from "@/components/link";
import Link from "next/link";
import Image from "next/image";

const FoodPage = () => {

    return(
        <Section className="">
            <div className="bg-blue-primary w-full h-screen bg-opacity-70 flex flex-col items-center gap-12">
                <div>
                    <TitleSection className="text-white pt-20 mb-6">¿Qué querés registrar?</TitleSection>
                </div>

                <div className="flex w-full gap-20 justify-center">
                    <div key="Etiqueta"
                         className="relative w-1/5 h-52 min-w-64 rounded-lg overflow-hidden shadow-lg transform transition-transform duration-300 hover:-translate-y-2">
                        <Link href="/food/foodTag">
                            <Image src="/etiqueta.jpg" alt="Etiqueta" width={500} height={500}
                                   className="w-full h-full object-cover"/>
                            <div
                                className="absolute top-0 h-full w-full p-6 bg-blue-primary bg-opacity-65 text-white text-center text-4xl font-bold flex justify-center items-center">
                                <span>Etiqueta</span>
                            </div>
                        </Link>
                    </div>

                    <div key="Plato"
                         className="relative w-1/5 h-52 min-w-64 rounded-lg overflow-hidden shadow-lg transform transition-transform duration-300 hover:-translate-y-2">
                        <Link href="/food/foodDishes">
                            <Image src="/comida.jpg" alt="Plato" width={500} height={500}
                                   className="w-full h-full object-cover"/>
                            <div
                                className="absolute top-0 h-full w-full p-6 bg-blue-primary bg-opacity-65 text-white text-center text-4xl font-bold flex justify-center items-center">
                                <span>Plato</span>
                            </div>
                        </Link>
                    </div>
                </div>

                <SubtitleSection className="text-white">O cargá tu comida manualmente</SubtitleSection>
                <li className="mr-3 list-none">
                    <NavLink href="/event/food" text="Registrar comida manual"
                             className="rounded-lg !py-2 bg-orange-primary hover:bg-orange-focus transition-all"/>
                </li>


            </div>
        </Section>
    );
};

export default FoodPage;