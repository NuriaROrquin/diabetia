import {Section} from "../../components/section";
import {EventCard} from "../../components/card";
import {TYPE_EVENTS} from "../../constants";

export const EventsGrid = () => {
    return (
        <>

            <Section className="items-center justify-center">

                <div className="w-full col-start-2 flex justify-self-center justify-center pt-10 pb-20 text-white">
                    <span className="text-xl">Elegí un evento para registrar en tu calendario</span>
                </div>
                <div className="container gap-y-24 gap-x-1 flex flex-wrap justify-around items-center">
                    <EventCard events={TYPE_EVENTS}/>
                </div>
            </Section>
        </>
    )
}

export default EventsGrid;