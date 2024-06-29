import { useState, useEffect } from "react";
import { TextArea, InputWithLabel } from "@/components/input";
import { ButtonOrange } from "@/components/button";
import { CustomDatePicker, CustomTimePicker } from "@/components/pickers";
import dayjs from "dayjs";
import { getEmailFromJwt } from "../../helpers";
import { useRouter } from "next/router";
import {addInsulinEvent, editInsulinEvent} from "../../services/event.service";

const InsulinEventForm = ({ existingData }) => {
    const [hour, setHour] = useState(dayjs());
    const [date, setDate] = useState(dayjs());
    const [insulinQuantity, setInsulinQuantity] = useState('');
    const [notes, setNotes] = useState('');
    const [error, setError] = useState(null);
    const router = useRouter();

    useEffect(() => {
        if (existingData) {
            const existingDate = dayjs(existingData.dateEvent);
            const existingHour = dayjs(existingData.dateEvent);
            setDate(existingDate);
            setHour(existingHour);
            setInsulinQuantity(existingData.dosage || '');
            setNotes(existingData.freeNote || '');
        }
    }, [existingData]);

    const handleSubmit = () => {
        const email = getEmailFromJwt();
        const dateFormatted = date.format("YYYY-MM-DD") + 'T' + hour.format('HH:mm:ss');

        if (router.query.id) {
            const data = {
                eventDate: dateFormatted,
                freeNote: notes,
                insulinInjected: insulinQuantity,
            };

            editInsulinEvent({ ...data, eventId: router.query.id }).then(() =>
                router.push("/calendar")
            ).catch((error) => {
                error.response.data ? setError(error.response.data) : setError("Hubo un error")            });
        } else {
            const data = {
                kindEventId: 1,
                eventDate: dateFormatted,
                freeNote: notes,
                insulinInjected: insulinQuantity,
            }

            addInsulinEvent(data).then(() =>
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
                label="Hora de administración"
                hour={dayjs(hour)}
                onChange={setHour}
                width="w-1/3"
                defaultHour={hour && hour}
            />
            <div className="flex items-start w-10/12">
                <InputWithLabel
                    label="¿Cuántas unidades de insulina se inyectó?"
                    placeholder="Escribí la cantidad de unidades administradas"
                    id="insulineQuantity"
                    width="w-2/5"
                    value={insulinQuantity}
                    defaultValue={insulinQuantity && insulinQuantity}
                    onChange={(e) => setInsulinQuantity(e.target.value)}
                />
            </div>
            <TextArea
                placeholder="Describí tus sensaciones, estado de ánimo y cualquier otro síntoma que pueda ser de ayuda para los profesionales"
                label="¿Cómo te sentís?"
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

export default InsulinEventForm;
