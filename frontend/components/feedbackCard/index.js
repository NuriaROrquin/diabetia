import { useState } from "react";
import { TYPE_EMOJIS } from "../../constants";
import {ButtonBlue} from "../button";

const FeedbackCard = ({ feedback }) => {
    const [selectedEmoji, setSelectedEmoji] = useState(null);
    const [notes, setNotes] = useState("");

    const handleEmojiSelect = (emoji) => {
        setSelectedEmoji(emoji);
    };

    const handleNotesChange = (event) => {
        setNotes(event.target.value);
    };

    const handleSubmit = () => {
        const formData = {
            ...feedback,
            emoji: selectedEmoji,
            notes,
        };
        console.log(formData); // Reemplazar con la lógica para enviar al backend
    };

    return (
            <div className="p-4 border w-3/5 rounded-lg shadow-lg bg-white text-blue-primary flex flex-col items-center justify-center">
                <div className="w-full justify-center items-center flex px-4">
                    <p className="w-1/2">Feedback para evento {feedback.idTipoEvento}</p>
                    <p className="w-1/2 text-right">Fecha: {feedback.fecha}</p>
                </div>
                <div className="my-4 pt-4 items-center justify-center">
                    <p>¿Cómo te sientes acerca de este evento?</p>
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
