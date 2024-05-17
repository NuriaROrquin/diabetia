import {Card} from "../../components/card";
import {Selector} from "../../components/selector";
import Link from "next/link";
import {useState} from "react";
import {DASHBOARD_OPTIONS_FILTER_DAYS, DASHBOARD_INDICATORS} from "../../constants";

export const Home = () => {
    const [isOpen, setIsOpen] = useState(false);
    const [selectedOption, setSelectedOption] = useState(DASHBOARD_OPTIONS_FILTER_DAYS[0]);
    const handleOptionClick = (option) => {
        setSelectedOption(option);
        setIsOpen(false);
    };
    return (
        <section className="w-full min-h-screen">
            <div className="container pt-12">
                <div className="grid grid-cols-3 w-full items-center">
                    <div className="w-full col-start-2 flex justify-self-center justify-center">
                        <Selector width="w-1/2" setIsOpen={setIsOpen} isOpen={isOpen} selectedOption={selectedOption} options={DASHBOARD_OPTIONS_FILTER_DAYS} handleOptionClick={handleOptionClick} />
                    </div>
                    <div className="col-start-3 justify-self-end">
                        <Link className="bg-orange-focus hover:bg-orange-primary transition-all text-white py-2 px-8 rounded-full w-full" href="/event">Registrar evento</Link>
                    </div>
                </div>

                <div className="flex flex-wrap justify-around mt-12 gap-x-1 gap-y-8">
                    {DASHBOARD_INDICATORS.map((data, index) => (
                        <Card
                            key={index}
                            textIndicator={data.textIndicator}
                            color={data.color}
                            number={data.number}
                            title={data.title}
                            description={data.description}
                        />
                    ))}
                </div>
            </div>
        </section>
    )
}

export default Home;