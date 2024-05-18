import {Section} from "../../components/section";
import CustomCalendar from "../../components/calendar";
import {ButtonOrange} from "../../components/button";
import {CustomLink, OrangeLink} from "../../components/link";

export const CalendarPage = () => {


    return (
        <Section className="pt-12">
            <CustomCalendar />

            <div className="flex items-center justify-center pt-12">
                <OrangeLink href="/event" label="Agregar Evento" width="w-1/12"/>
            </div>
        </Section>
    )
}

export default CalendarPage