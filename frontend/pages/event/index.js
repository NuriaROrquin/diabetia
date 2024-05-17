import {Section} from "../../components/section";
import {EventCard} from "../../components/card";

export const EventsGrid = () => {
    return (
        <Section>
            <div className="container">
                <EventCard />
            </div>
        </Section>
    )
}

export default EventsGrid;