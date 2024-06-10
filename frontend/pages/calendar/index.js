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
import {SubtitleSection, TitleSection} from "@/components/titles";

const registrarEventoTooltipText = "Registrá un nuevo evento: mediciones de glucosa, actividad física, eventos de salud, visitas médicas, insulina, comida manual.";

export const CalendarPage = () => {
    const [eventList, setEventList] = useState();
    const [error, setError] = useState(null);
    const email = getEmailFromJwt();
    const [eventsByDate, setEventsByDate] = useState(null);
    const [eventIdToDelete, setEventIdToDelete] = useState(null)
    const [loadingCalendar, setLoadingCalendar] = useState(true)
    const [loadingEvent, setLoadingEvent] = useState(false)
    const { isVisible, openModal, closeModal } = useModal();
    const router = useRouter();

    useEffect(() => {
        setLoadingCalendar(true)
        getAllEvents({email})
            .then((res) => {
                setEventList(res.data);
                setLoadingCalendar(false)
            })
            .catch((error) => {
                setError(error.response?.data ? error.response.data : "Hubo un error");
            });
    }, [email]);

    const handleOnSelectDay = (e) => {
        setEventsByDate(null);
        setLoadingEvent(true)
        getEventsByDate(e.toISOString(), email)
            .then((res) => {
                setEventsByDate(res.data);
                setLoadingEvent(false)
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
            <div className="w-full col-start-2 flex flex-col justify-self-center justify-center pb-6 text-white gap-4">
                <TitleSection className="text-white">Tu agenda de bienestar personal, todo en un mismo lugar.</TitleSection>
                <SubtitleSection className="text-white mb-8">Visualizá tus eventos cargados. Podés eliminarlos o editarlos.</SubtitleSection>
            </div>
            {loadingCalendar && !eventList &&
                <div className="w-full flex justify-center items-center mb-5">
                    <svg aria-hidden="true"
                         className="inline w-10 h-10 text-blue-secondary animate-spin dark:text-blue-secondary fill-white"
                         viewBox="0 0 100 101" fill="none" xmlns="http://www.w3.org/2000/svg">
                        <path
                            d="M100 50.5908C100 78.2051 77.6142 100.591 50 100.591C22.3858 100.591 0 78.2051 0 50.5908C0 22.9766 22.3858 0.59082 50 0.59082C77.6142 0.59082 100 22.9766 100 50.5908ZM9.08144 50.5908C9.08144 73.1895 27.4013 91.5094 50 91.5094C72.5987 91.5094 90.9186 73.1895 90.9186 50.5908C90.9186 27.9921 72.5987 9.67226 50 9.67226C27.4013 9.67226 9.08144 27.9921 9.08144 50.5908Z"
                            fill="currentColor"/>
                        <path
                            d="M93.9676 39.0409C96.393 38.4038 97.8624 35.9116 97.0079 33.5539C95.2932 28.8227 92.871 24.3692 89.8167 20.348C85.8452 15.1192 80.8826 10.7238 75.2124 7.41289C69.5422 4.10194 63.2754 1.94025 56.7698 1.05124C51.7666 0.367541 46.6976 0.446843 41.7345 1.27873C39.2613 1.69328 37.813 4.19778 38.4501 6.62326C39.0873 9.04874 41.5694 10.4717 44.0505 10.1071C47.8511 9.54855 51.7191 9.52689 55.5402 10.0491C60.8642 10.7766 65.9928 12.5457 70.6331 15.2552C75.2735 17.9648 79.3347 21.5619 82.5849 25.841C84.9175 28.9121 86.7997 32.2913 88.1811 35.8758C89.083 38.2158 91.5421 39.6781 93.9676 39.0409Z"
                            fill="currentFill"/>
                    </svg>
                </div>
            }
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
            {!eventsByDate && loadingEvent &&
                <div className="w-full flex justify-center items-center my-20">
                    <svg aria-hidden="true"
                         className="inline w-10 h-10 text-blue-secondary animate-spin dark:text-blue-secondary fill-white"
                         viewBox="0 0 100 101" fill="none" xmlns="http://www.w3.org/2000/svg">
                        <path
                            d="M100 50.5908C100 78.2051 77.6142 100.591 50 100.591C22.3858 100.591 0 78.2051 0 50.5908C0 22.9766 22.3858 0.59082 50 0.59082C77.6142 0.59082 100 22.9766 100 50.5908ZM9.08144 50.5908C9.08144 73.1895 27.4013 91.5094 50 91.5094C72.5987 91.5094 90.9186 73.1895 90.9186 50.5908C90.9186 27.9921 72.5987 9.67226 50 9.67226C27.4013 9.67226 9.08144 27.9921 9.08144 50.5908Z"
                            fill="currentColor"/>
                        <path
                            d="M93.9676 39.0409C96.393 38.4038 97.8624 35.9116 97.0079 33.5539C95.2932 28.8227 92.871 24.3692 89.8167 20.348C85.8452 15.1192 80.8826 10.7238 75.2124 7.41289C69.5422 4.10194 63.2754 1.94025 56.7698 1.05124C51.7666 0.367541 46.6976 0.446843 41.7345 1.27873C39.2613 1.69328 37.813 4.19778 38.4501 6.62326C39.0873 9.04874 41.5694 10.4717 44.0505 10.1071C47.8511 9.54855 51.7191 9.52689 55.5402 10.0491C60.8642 10.7766 65.9928 12.5457 70.6331 15.2552C75.2735 17.9648 79.3347 21.5619 82.5849 25.841C84.9175 28.9121 86.7997 32.2913 88.1811 35.8758C89.083 38.2158 91.5421 39.6781 93.9676 39.0409Z"
                            fill="currentFill"/>
                    </svg>
                </div>
            }


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