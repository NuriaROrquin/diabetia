import {Section} from "../../components/section";
import {EventCard} from "../../components/card";
import {TYPE_EVENTS} from "../../constants";

export const EventsGrid = () => {
    return (
        <Section className="items-center justify-center flex">
            <div className="container gap-y-24 gap-x-1 flex flex-wrap justify-around items-center">
                <EventCard events={TYPE_EVENTS} />
            </div>
        </Section>
    )
}

export default EventsGrid;