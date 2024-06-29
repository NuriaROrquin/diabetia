import { Section } from "../../components/section";
import FeedbackCard from "../../components/feedbackCard";
import { useEffect, useState } from "react";
import { getAllEventToFeedback } from "../../services/feedback.service";

const Feedback = () => {
    const [feedbacks, setFeedbacks] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        getAllEventToFeedback()
            .then((res) => {
                if (res.data && res.data.length > 0) {
                    setFeedbacks(res.data);
                } else {
                    setFeedbacks([]);
                }
                setLoading(false);
            })
            .catch((error) => {
                console.error("Error al obtener eventos para feedback:", error);
                setError("Hubo un error al obtener los eventos para feedback.");
                setLoading(false);
            });
    }, []);

    return (
        <Section className="items-center justify-center">
            <div className="container gap-y-12 gap-x-4 flex flex-wrap justify-center items-center pt-16 pb-20">
                {loading ? (
                    <p>Cargando...</p>
                ) : error ? (
                    <p>{error}</p>
                ) : feedbacks.length > 0 ? (
                    feedbacks.map((feedback) => (
                        <FeedbackCard key={feedback.eventId} feedback={feedback} />
                    ))
                ) : (
                    <p>No hay feedbacks pendientes.</p>
                )}
            </div>
        </Section>
    );
};

export default Feedback;
