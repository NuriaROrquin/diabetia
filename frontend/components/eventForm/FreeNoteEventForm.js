import { useState, useEffect } from "react";
import { useRouter } from "next/router";
import { TextArea, InputWithLabel } from "@/components/input";
import { ButtonOrange } from "@/components/button";
import { CustomDatePicker, CustomTimePicker } from "@/components/pickers";
import dayjs from "dayjs";
import { TYPE_EVENTS } from "../../constants";
import { addFreeNoteEvent, editFreeNoteEvent } from "../../services/event.service";

const FreeNoteEventForm = ({ existingData }) => {
    const [hour, setHour] = useState(dayjs());
    const [date, setDate] = useState(dayjs());
    const [notes, setNotes] = useState('');
    const [error, setError] = useState(null);
    const router = useRouter();

    useEffect(() => {
        if (existingData) {
            var existingDate = dayjs(existingData.dateEvent).format("YYYY-MM-DD");
            var existingHour = dayjs(existingData.dateEvent);
            setDate(existingDate);
            setHour(existingHour);
            setNotes(existingData.freeNote || '');
        }
    }, [existingData]);

    const handleSubmit = () => {
        const dateFormatted = date.format("YYYY-MM-DD") + 'T' + hour.format('HH:mm:ss');

        const data = {
            kindEventId: TYPE_EVENTS.find((event) => event.title === "NOTA LIBRE")?.id,
            eventDate: dateFormatted,
            freeNote: notes
        };

        if (router.query.id) {
            editFreeNoteEvent({ ...data, eventId: router.query.id }).then(() =>
                router.push("/calendar")
            ).catch((error) => {
                error.response.data ? setError(error.response.data) : setError("Hubo un error")            });
        } else {
            addFreeNoteEvent(data).then(() =>
                router.push("/calendar")
            ).catch((error) => {
                error.response.data ? setError(error.response.data) : setError("Hubo un error")            });
        }
    };

    return (
        <div className="bg-white rounded-xl w-full flex flex-wrap text-gray-primary py-20 px-44 my-12 justify-around gap-x-2 gap-y-12">
            <CustomDatePicker
                label="Ingresá una fecha"
                date={dayjs(date)}
                onChange={(e) => setDate(e)}
                width="w-1/3"
                defaultDate={date && date}
            />
            <CustomTimePicker
                label="Hora"
                hour={dayjs(hour)}
                onChange={setHour}
                width="w-1/3"
                defaultHour={hour && hour}
            />
            <TextArea
                placeholder="Describí tus sensaciones, estado de ánimo y cualquier otro síntoma que pueda ser de ayuda para los profesionales"
                label="Nota Libre"
                id="notes"
                width="w-10/12"
                value={notes}
                defaultValue={notes && notes}
                onChange={(e) => setNotes(e.target.value)}
            />

            {error && <span className="text-red-500 mb-3">{error}</span>}

            <ButtonOrange onClick={handleSubmit} label="Enviar" width="w-1/3" />
        </div>
    );
};

export default FreeNoteEventForm;
