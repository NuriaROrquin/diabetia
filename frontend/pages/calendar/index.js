import {Section} from "../../components/section";
import CustomCalendar from "../../components/calendar";
import {OrangeLink} from "../../components/link";
import CustomTooltip from "@/components/tooltip";
import {useEffect, useState} from "react";
import {deleteEventById, getAllEvents, getEventsByDate} from "../../services/api.service";
import {Delete, Edit} from "@mui/icons-material";
import Link from "next/link";
import {getEmailFromJwt} from "../../helpers";
import useModal from "../../hooks";
import Modal from "@/components/modal";
import {useRouter} from "next/router";

const registrarEventoTooltipText = "Registrá un nuevo evento: mediciones de glucosa, actividad física, eventos de salud, visitas médicas, insulina, comida manual.";

export const CalendarPage = () => {
    const [eventList, setEventList] = useState();
    const [error, setError] = useState(null);
    const email = getEmailFromJwt();
    const [eventsByDate, setEventsByDate] = useState(null);
    const [eventIdToDelete, setEventIdToDelete] = useState(null)
    const { isVisible, openModal, closeModal } = useModal();
    const router = useRouter();

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
        openModal();
        setEventIdToDelete(idEvento);
    }

    const onCloseModal = () => {
        setEventIdToDelete(null);
        closeModal();
    }

    const onDelete = () => {
        deleteEventById(eventIdToDelete)
            .then((res) => {
                router.reload();
                closeModal()
            })
            .catch((error) => {
                setError(error.response?.data ? error.response.data : "Hubo un error");
            });
    }

    return (
        <Section className="pt-12 pb-6">
            <div className="w-full col-start-2 flex justify-self-center justify-center pb-6 text-white">
                <span className="text-3xl">Tu agenda de bienestar personal, todo en un mismo lugar.</span>
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

            <Modal isVisible={isVisible} closeModal={onCloseModal}>
                <h1 className="text-3xl font-bold my-2">Confirmación</h1>
                <p className="my-6">¿Estás seguro de que deseas eliminar este evento?</p>
                <div className="flex justify-end mt-12">
                    <button onClick={onCloseModal} className="px-4 py-2 bg-gray-500 text-white rounded mr-2 hover:bg-gray-700">
                        Cancelar
                    </button>
                    <button onClick={() => onDelete()} className="px-4 py-2 bg-red-primary text-white rounded hover:bg-red-700">
                        Eliminar
                    </button>
                </div>
            </Modal>

        </Section>
    )
}

export default CalendarPage