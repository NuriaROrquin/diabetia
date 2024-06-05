import {Section} from "../../components/section";
import CustomCalendar from "../../components/calendar";
import {OrangeLink} from "../../components/link";
import CustomTooltip from "@/components/tooltip";
import {useEffect, useState} from "react";
import {useCookies} from "react-cookie";
import {getAllEvents} from "../../services/api.service";

const registrarEventoTooltipText = "Registrá un nuevo evento: mediciones de glucosa, actividad física, eventos de salud, visitas médicas, insulina, comida manual.";

export const CalendarPage = () => {
    const [eventList, setEventList] = useState();
    const [error, setError] = useState(null);
    const [cookies, _setCookie, _removeCookie] = useCookies(['email']);
    const email = cookies.email;

    useEffect(() => {
        getAllEvents({email})
            .then((res) => {
                setEventList(res.data);
            })
            .catch((error) => {
                setError(error.response?.data ? error.response.data : "Hubo un error");
            });
    }, [email]);

    return (
        <Section className="pt-12 pb-6">
            <div className="w-full col-start-2 flex justify-self-center justify-center pb-6 text-white">
                <span className="text-xl">Tu agenda de bienestar personal, todo en un mismo lugar</span>
            </div>

            {eventList && <CustomCalendar events={eventList}/>}

            <CustomTooltip title={registrarEventoTooltipText} arrow>
                <div className="flex items-center justify-center pt-12">
                    <OrangeLink href="/event" label="Agregar Evento" width="w-1/10"/>
                </div>

            </CustomTooltip>
        </Section>
    )
}

export default CalendarPage