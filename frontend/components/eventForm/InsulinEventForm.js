import { useState, useEffect } from "react";
import { TextArea, InputWithLabel } from "@/components/input";
import { ButtonOrange } from "@/components/button";
import { CustomDatePicker, CustomTimePicker } from "@/components/pickers";
import dayjs from "dayjs";
import { addInsulinEvent, editInsulinEvent } from "../../services/api.service";
import { getEmailFromJwt } from "../../helpers";
import { useRouter } from "next/router";

const InsulinEventForm = ({ existingData }) => {
    const [hour, setHour] = useState(dayjs());
    const [date, setDate] = useState(dayjs());
    const [insulinQuantity, setInsulinQuantity] = useState('');
    const [notes, setNotes] = useState('');
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

        const data = {
            email,
            idKindEvent: 1,
            eventDate: dateFormatted,
            freeNote: notes,
            insulin: insulinQuantity,
        };

        if (router.query.id) {
            editInsulinEvent({ ...data, idEvent: router.query.id }).then(() =>
                router.push("/calendar")
            );
        } else {
            addInsulinEvent(data).then(() =>
                router.push("/calendar")
            );
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
            <ButtonOrange onClick={handleSubmit} label="Enviar" width="w-1/3" />
        </div>
    );
};

export default InsulinEventForm;
