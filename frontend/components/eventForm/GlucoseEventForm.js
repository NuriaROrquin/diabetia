import { useState, useEffect } from "react";
import { useRouter } from "next/router";
import { TextArea, InputWithLabel } from "@/components/input";
import { ButtonOrange } from "@/components/button";
import { CustomDatePicker, CustomTimePicker } from "@/components/pickers";
import dayjs from "dayjs";
import { addGlucoseEvent, editGlucoseEvent } from "../../services/api.service";
import { getEmailFromJwt } from "../../helpers";

const GlucoseEventForm = ({ existingData }) => {
    const [hour, setHour] = useState(dayjs());
    const [date, setDate] = useState(dayjs());
    const [glucoseLevel, setGlucoseLevel] = useState('');
    const [notes, setNotes] = useState('');
    const router = useRouter();

    useEffect(() => {
        if (existingData) {
            var existingDate = dayjs(existingData.dateEvent).format("YYYY-MM-DD");
            var existingHour = dayjs(existingData.dateEvent);
            setDate(existingDate);
            setHour(existingHour);
            setGlucoseLevel(existingData.glucoseLevel || '');
            setNotes(existingData.freeNote || '');
        }
    }, [existingData]);

    const handleSubmit = () => {
        const email = getEmailFromJwt();
        var dateFormatted;
        if(!router.query.id){
            dateFormatted = date.format("YYYY-MM-DD") + 'T' + hour.format('HH:mm:ss');
        }else{
            dateFormatted = date + 'T' + hour.format('HH:mm:ss')
        }

        const data = {
            email,
            idKindEvent: 3,
            eventDate: dateFormatted,
            freeNote: notes,
            glucose: glucoseLevel,
        };

        if (router.query.id) {
            editGlucoseEvent({ ...data, idEvent: router.query.id }).then(() =>
                router.push("/calendar")
            );
        } else {
            addGlucoseEvent(data).then(() =>
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
                defaultDate = {date && date}
            />
            <CustomTimePicker
                label="Hora de medición"
                hour={dayjs(hour)}
                onChange={setHour}
                width="w-1/3"
                defaultHour={hour && hour}
            />
            <div className="flex items-start w-10/12">
                <InputWithLabel
                    label="Cantidad de mg/dL de glucosa"
                    placeholder="Escribí tu medición"
                    id="glycemiaMeasurement"
                    width="w-2/5"
                    value={glucoseLevel}
                    defaultValue={glucoseLevel && glucoseLevel}
                    onChange={(e) => setGlucoseLevel(e.target.value)}
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

export default GlucoseEventForm;
