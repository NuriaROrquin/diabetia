// Feedback.jsx
import { Section } from "../../components/section";
import FeedbackCard from "../../components/feedbackCard";

const Feedback = () => {
    // Datos iniciales del feedback
    const feedbacks = [
        { id: 1, idTipoEvento: 1, fecha: "2024-07-01" },
        { id: 2, idTipoEvento: 2, fecha: "2024-07-02" },
        { id: 3, idTipoEvento: 3, fecha: "2024-07-03" },
    ];

    return (
        <Section className="items-center justify-center">
            <div className="container gap-y-12 gap-x-4 flex flex-wrap justify-center items-center pt-16">
            {feedbacks.map((feedback) => (
                    <FeedbackCard key={feedback.id} feedback={feedback} />
                ))}
            </div>
        </Section>
    );
};

export default Feedback;
