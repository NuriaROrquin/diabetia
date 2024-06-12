import { useState, useEffect } from "react";
import { TextArea } from "@/components/input";
import { ButtonOrange } from "@/components/button";
import { CustomDatePicker, CustomTimePicker } from "@/components/pickers";
import dayjs from "dayjs";
import { Select } from "@/components/selector";
import { addPhysicalEvent, editPhysicalEvent } from "../../services/api.service";
import { getEmailFromJwt } from "../../helpers";
import { useRouter } from "next/router";
import {TYPE_EXERCISES} from "../../constants";

const ExerciseEventForm = ({ existingData }) => {
    const [isOpen, setIsOpen] = useState(false);
    const [startHour, setStartHour] = useState(dayjs());
    const [endHour, setEndHour] = useState(dayjs());
    const [date, setDate] = useState(dayjs());
    const [selectedActivity, setSelectedActivity] = useState(null);
    const [notes, setNotes] = useState('');
    const router = useRouter();

    useEffect(() => {
        if (existingData) {
            const eventDate = dayjs(existingData.dateEvent);
            const startHourFormatted = dayjs(existingData.dateEvent);
            const endHourFormatted = startHour.add(existingData.duration, 'minute');
            console.log(endHour)
            setDate(eventDate);
            setStartHour(startHourFormatted);
            setEndHour(endHourFormatted);
            setSelectedActivity(TYPE_EXERCISES.find(activity => activity.id === existingData.idPhysicalEducationEvent) || null);
            setNotes(existingData.freeNote || '');
        }
    }, [existingData]);

    const handleOptionClick = (option) => {
        setSelectedActivity(option);
        setIsOpen(false);
    };

    const handleSubmit = () => {
        const email = getEmailFromJwt();
        const start = startHour ? startHour.format('HH:mm:ss') : null;
        const end = endHour ? endHour.format('HH:mm:ss') : null;
        const notes = document.getElementById("notes").value;
        const eventDateTime = date && startHour ? date.format('YYYY-MM-DD') + 'T' + startHour.format('HH:mm:ss') : null;

        const data = {
            email : email,
            idKindEvent: 4,
            eventDate : eventDateTime,
            iniciateTime: start,
            finishTime: end,
            freeNote: notes,
            physicalActivity: selectedActivity ? selectedActivity.id : null
        };

        if (router.query.id) {
            editPhysicalEvent({ ...data, idEvent: router.query.id }).then(() =>
                router.push("/calendar")
            );
        } else {
            addPhysicalEvent(data).then(() =>
                router.push("/calendar")
            );
        }
    };

    return (
        <div className="bg-white rounded-xl w-full flex flex-wrap text-gray-primary py-20 px-44 my-12 justify-around gap-x-2 gap-y-12">
            <CustomDatePicker
                label="Ingresá una fecha"
                value={date}
                onChange={(e) => setDate(e)}
                defaultDate={date && date}
                width="w-1/3"
            />

            <Select label="Actividad" placeholder="Elegí una actividad" options={TYPE_EXERCISES} selectedOption={selectedActivity} handleOptionClick={handleOptionClick} setIsOpen={setIsOpen} isOpen={isOpen} width="w-1/3" />

            <CustomTimePicker
                label="Hora de comienzo"
                value={startHour}
                onChange={setStartHour}
                defaultHour={startHour && startHour}
                width="w-1/3"
            />

            <CustomTimePicker
                label="Hora de finalización"
                value={endHour}
                onChange={setEndHour}
                defaultHour={endHour && endHour}
                width="w-1/3"
            />

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

export default ExerciseEventForm;