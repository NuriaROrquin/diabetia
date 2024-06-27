import {Section} from "../../components/section";
import {EventCard} from "../../components/card";
import {TYPE_EVENTS} from "../../constants";

export const EventsGrid = () => {
    return (
        <>
            <Section className="items-center justify-center">
                <div className="w-full col-start-2 flex justify-self-center justify-center pt-32 pb-16 text-white">
                    <span className="text-3xl font-bold">Elegí un evento para registrar en tu calendario</span>
                </div>
                <div className="container gap-y-12 gap-x-4 flex flex-wrap justify-center md:justify-between items-center pb-16">
                    <EventCard events={TYPE_EVENTS}/>
                </div>
            </Section>
        </>
    )
}

export default EventsGrid;