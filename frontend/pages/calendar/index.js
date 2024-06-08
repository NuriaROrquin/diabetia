import {Section} from "../../components/section";
import CustomCalendar from "../../components/calendar";
import {OrangeLink} from "../../components/link";
import CustomTooltip from "@/components/tooltip";
import {useEffect, useState} from "react";
import {useCookies} from "react-cookie";
import {getAllEvents, getEventsByDate, getEventsByDay} from "../../services/api.service";
import {Delete, Edit} from "@mui/icons-material";
import Link from "next/link";

const registrarEventoTooltipText = "Registrá un nuevo evento: mediciones de glucosa, actividad física, eventos de salud, visitas médicas, insulina, comida manual.";

export const CalendarPage = () => {
    const [eventList, setEventList] = useState();
    const [error, setError] = useState(null);
    const [cookies, _setCookie, _removeCookie] = useCookies(['email']);
    const email = cookies.email;
    const [eventsByDate, setEventsByDate] = useState(null);

    useEffect(() => {
        getAllEvents({email})
            .then((res) => {
                setEventList(res.data);
            })
            .catch((error) => {
                setError(error.response?.data ? error.response.data : "Hubo un error");
            });
    }, [email]);

    const handleOnSelectDay = (e) => {
        setEventsByDate(null);
        getEventsByDate(e.toISOString(), email)
            .then((res) => {
                setEventsByDate(res.data);
            })
            .catch((error) => {
                setError(error.response?.data ? error.response.data : "Hubo un error");
            });
    }

    const onHandleDelete = (idEvento) => {
        console.log(idEvento)
        //TODO: Pegarle al back para borrar todo lo que tiene que ver con este idEvento
    }

    return (
        <Section className="pt-12 pb-6">
            <div className="w-full col-start-2 flex justify-self-center justify-center pb-6 text-white">
                <span className="text-xl">Tu agenda de bienestar personal, todo en un mismo lugar</span>
            </div>

            {eventList && <CustomCalendar events={eventList} handleOnSelectDay={handleOnSelectDay}/>}

            <CustomTooltip title={registrarEventoTooltipText} arrow>
                <div className="flex items-center justify-center pt-12">
                    <OrangeLink href="/event" label="Agregar Evento" width="w-1/10"/>
                </div>
            </CustomTooltip>

            {eventsByDate && eventsByDate.map((event, index) => {
                return (
                    <div className="container bg-white rounded-3xl p-6 !my-12 flex justify-between" key={index}>
                        <div>
                            <span className="text-xl text-blue-secondary font-bold mr-5">{event.time}</span>
                            <span className="text-lg font-bold text-blue-primary">{event.title}</span>
                            {event.additionalInfo && <span className="text-lg text-blue-primary"> - {event.additionalInfo}</span>}
                        </div>
                        <div className="flex items-center justify-center gap-6">
                            <Link href={`/calendar/edit/${event.idEvent}`}>
                                <Edit className="text-blue-primary" />
                            </Link>
                            <button onClick={() => onHandleDelete(event.idEvent)}>
                                <Delete className="text-blue-primary" />
                            </button>
                        </div>
                    </div>
                )
            })}

        </Section>
    )
}

export default CalendarPage