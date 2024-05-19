import {Section} from "../../components/section";
import CustomCalendar from "../../components/calendar";
import {ButtonOrange} from "../../components/button";
import {CustomLink, OrangeLink} from "../../components/link";
import { Tooltip } from '@mui/material';

export const CalendarPage = () => {
    const registrarEventoTooltipText = "Registrá un nuevo evento: mediciones de glucosa, actividad física, eventos de salud, visitas médicas, insulina, comida manual.";


    return (
        <Section className="pt-12 pb-6">
            <CustomCalendar />


            <Tooltip title={registrarEventoTooltipText} arrow>
            <div className="flex items-center justify-center pt-12">
                <OrangeLink href="/event" label="Agregar Evento" width="w-1/10"/>
            </div>

            </Tooltip>
        </Section>
    )
}

export default CalendarPage