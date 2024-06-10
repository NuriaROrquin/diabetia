import { useEffect, useState } from "react";
import { useRouter } from "next/router";
import { Section } from "@/components/section";
import { getEventType } from "../../../../services/api.service";
import { GlucoseEventForm, InsulinEventForm } from "@/components/eventForm";
const EditEvent = () => {
    const router = useRouter();
    const [eventData, setEventData] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        if (router.query.id) {
            const fetchEventData = async () => {
                try {
                    const response = await getEventType({ id: router.query.id });
                    setEventData(response.data);
                    console.log(response.data)
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
    if (error) return <div>Error: {error.message}</div>;

    return (
        <Section className="pt-12 pb-6">
            <div>
                Edición de evento del id {router.query.id}
            </div>
            {eventData && (
                <div className="container items-center flex w-full justify-center flex-col">
                    <h2>Detalles del evento</h2>
                    {eventData.typeEvent === 3 && (
                        <GlucoseEventForm existingData={eventData.glucoseEvent} />
                    )}
                    {eventData.typeEvent === 4 && (
                        <InsulinEventForm existingData={eventData.insulinEvent} />
                    )}
                </div>
            )}
        </Section>
    );
};

export default EditEvent;
