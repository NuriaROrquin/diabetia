import { useEffect, useState } from "react";
import { useRouter } from "next/router";
import { Section } from "@/components/section";
import { GlucoseEventForm, InsulinEventForm, FreeNoteEventForm, ExerciseEventForm, MedicalVisitEventForm } from "@/components/eventForm/index.js";
import {TYPE_EVENTS} from "../../../../constants";
import {TitleSection} from "@/components/titles";
import {getEventType} from "../../../../services/event.service";

const EditEvent = () => {
    const router = useRouter();
    const [eventData, setEventData] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const [evento, setEvento] = useState(null);

    useEffect(() => {
        if (router.query.id) {
            const fetchEventData = async () => {
                try {
                    const response = await getEventType({ id: router.query.id });
                    setEventData(response.data);
                    console.log(response.data);
                    const eventType = TYPE_EVENTS.find(e => e.id === response.data.typeEvent);
                    setEvento(eventType);

                } catch (err) {
                    setError(err);
                } finally {
                    setLoading(false);
                }
            };

            fetchEventData();
        }
    }, [router.query.id]);

    if (loading) return <div>Loading...</div>;
    if (error) return <div><span className="text-red-500 mb-3">{error}</span></div>;


    return (
        <Section className="pt-12 pb-6">
            <div>

            </div>
            {eventData && (
                <div className="container items-center flex w-full justify-center flex-col">
                    <TitleSection className="text-white mt-12">Detalles del evento {evento ? `${evento.title}` : "Evento no encontrado"}</TitleSection>
                    {eventData.typeEvent === TYPE_EVENTS.find((event) => event.title === "GLUCOSA")?.id && (
                        <GlucoseEventForm existingData={eventData.glucoseEvent} />
                    )}
                    {eventData.typeEvent === TYPE_EVENTS.find((event) => event.title === "INSULINA")?.id && (
                        <InsulinEventForm existingData={eventData.insulinEvent} />
                    )}
                    {eventData.typeEvent === TYPE_EVENTS.find((event) => event.title === "NOTA LIBRE")?.id && (
                        <FreeNoteEventForm existingData={eventData.insulinEvent} />
                    )}
                        {eventData.typeEvent === TYPE_EVENTS.find((event) => event.title === "ACTIVIDAD FÍSICA")?.id && (
                        <ExerciseEventForm existingData={eventData.physicalActivityEvent } />
                    )}
                    {eventData.typeEvent === TYPE_EVENTS.find((event) => event.title === "VISITA MÉDICA")?.id && (
                        <MedicalVisitEventForm existingData={eventData.medicalVisitEvent } />
                    )}
                </div>
            )}
        </Section>
    );
};

export default EditEvent;
