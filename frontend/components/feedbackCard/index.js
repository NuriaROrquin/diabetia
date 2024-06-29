import { useState } from "react";
import { TYPE_EMOJIS } from "../../constants";
import { ButtonBlue } from "../button";
import {addFeedback} from "../../services/feedback.service";
import { useRouter } from "next/router";

const FeedbackCard = ({ feedback }) => {
    const [selectedEmoji, setSelectedEmoji] = useState(null);
    const [notes, setNotes] = useState("");
    const [error, setError] = useState(null);

    const handleEmojiSelect = (emoji) => {
        setSelectedEmoji(emoji);
    };

    const handleNotesChange = (event) => {
        setNotes(event.target.value);
    };

    const handleSubmit = () => {
        const formData = {
            eventId: feedback.eventId,
            sentimentalId: selectedEmoji.id,
            freeNote: notes,
        };
        console.log(formData)
        addFeedback(formData).then(() =>
            router.push("/feedback")
        ).catch((error) => {
            error.response ? setError(error.response) : setError("Hubo un error")            });
    };

    return (
        <div className="p-4 border w-3/5 rounded-lg shadow-lg bg-white text-blue-primary flex flex-col items-center justify-center">
            <div className="w-full justify-center items-center flex px-4">
                {feedback.kindEventId === 4 && (
                    <p className="w-1/2">Actividad Realizada: {feedback.activityName}</p>
                )}
                {feedback.kindEventId === 2 && (
                    <p className="w-1/2">Carbohidratos Ingeridos: {feedback.carbohydrates}</p>
                )}
                <p className="w-1/2 text-right">Fecha: {feedback.eventDate}</p>
            </div>
            <div className="my-4 pt-4 items-center justify-cente">

                <p>¿Cómo te sentiste post evento?</p>
                <div className="flex gap-2 pt-2 justify-center">
                    {TYPE_EMOJIS.map((emoji) => (
                        <button
                            key={emoji.index}
                            className={`rounded-full bg-white text-blue-primary hover:bg-blue-secondary hover:text-white transition-colors duration-300 ${selectedEmoji === emoji ? 'text-white bg-blue-500' : 'text-blue-primary'}`}
                            onClick={() => handleEmojiSelect(emoji)}
                        >
                            <img src={emoji.emoji} alt={emoji.title} className="w-8 h-8" />
                        </button>
                    ))}
                </div>
            </div>
            <div className="mb-4 w-full px-4">
                <label htmlFor={`notes-${feedback.id}`} className="block">
                    Notas:
                </label>
                <textarea
                    id={`notes-${feedback.id}`}
                    className="w-full p-2 border rounded-lg"
                    value={notes}
                    onChange={handleNotesChange}
                />
            </div>
            <ButtonBlue
                label="Enviar"
                width="w-1/2 text-md"
                onClick={handleSubmit}
                className="mb-3"
            />
        </div>
    );
};

export default FeedbackCard;
