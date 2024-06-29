import { useState, useEffect } from "react";
import { useRouter } from "next/router";
import { TextArea, CustomSwitch } from "@/components/input";
import { ButtonOrange } from "@/components/button";
import { CustomDatePicker, CustomTimePicker } from "@/components/pickers";
import dayjs from "dayjs";
import { Select } from "@/components/selector";
import { addMedicalVisitEvent, editMedicalVisitEvent } from "../../services/event.service";
import { TYPE_EVENTS, TYPE_MEDIC } from "../../constants";

const MedicalVisitEventForm = ({ existingData }) => {
    const [isOpen, setIsOpen] = useState(false);
    const [selectedOption, setSelectedOption] = useState(null);
    const [hour, setHour] = useState(dayjs());
    const [hourRecordatory, setHourRecordatory] = useState(null);
    const [date, setDate] = useState(dayjs());
    const [reminder, setReminder] = useState(false);
    const [notes, setNotes] = useState('');
    const [error, setError] = useState('');
    const router = useRouter();

    useEffect(() => {
        if (existingData) {
            var existingDate = dayjs(existingData.dateEvent).format("YYYY-MM-DD");
            var existingHour = dayjs(existingData.dateEvent);
            const recordatoryHour = existingData.hourRecordatory ? dayjs(existingData.hourRecordatory) : null;
            setDate(existingDate);
            setHour(existingHour);
            setSelectedOption(TYPE_MEDIC.find(medic => medic.id === existingData.professionalId) || null);
            setReminder(existingData.recordatory);
            setHourRecordatory(recordatoryHour);
            setNotes(existingData.description || '');
        }
    }, [existingData]);

    const handleOptionClick = (option) => {
        setSelectedOption(option);
        setIsOpen(false);
    };

    const handleHourRecordatoryChange = (newHour) => {
        if (newHour.isAfter(hour)) {
            setError('La hora del recordatorio no puede ser posterior a la hora del evento.');
        } else {
            setError('');
            setHourRecordatory(newHour);
        }
    };

    const handleSubmit = () => {
        if (reminder && hourRecordatory && hourRecordatory.isAfter(hour)) {
            setError('La hora del recordatorio no puede ser posterior a la hora del evento.');
            return;
        }

        const dateFormatted = date && hour ? date.format("YYYY-MM-DD") + 'T' + hour.format('HH:mm:ss') : null;
        const recordatoryFormatted = date && hourRecordatory ? date.format("YYYY-MM-DD") + 'T' + hourRecordatory.format('HH:mm:ss') : null;

        const data = {
            eventDate: dateFormatted,
            kindEventId: TYPE_EVENTS.find(event => event.title === "VISITA MÉDICA")?.id,
            professionalId: selectedOption?.id,
            recordatory: reminder,
            hourRecordatory: recordatoryFormatted,
            description: notes,
        };

        if (router.query.id) {
            editMedicalVisitEvent({ ...data, eventId: router.query.id }).then(() =>
                router.push("/calendar")
            ).catch((error) => {
                error.response.data ? setError(error.response.data) : setError("Hubo un error")            });
        } else {
            addMedicalVisitEvent({ ...data }).then(() =>
                router.push("/calendar")
            ).catch((error) => {
                error.response.data ? setError(error.response.data) : setError("Hubo un error")            });
        }
    };

    return (
        <div className="bg-white rounded-xl w-full flex flex-wrap text-gray-primary py-20 px-44 my-12 justify-around gap-x-2 gap-y-12">
            <div className="flex justify-between w-10/12">
                <CustomDatePicker
                    label="Ingresá una fecha"
                    date={date}
                    onChange={(e) => setDate(e)}
                    width="w-1/3"
                    defaultDate={date && date}
                />

                <CustomTimePicker
                    label="Hora del evento"
                    hour={hour}
                    onChange={setHour}
                    width="w-1/3"
                    defaultHour={hour && hour}
                />
            </div>

            <div className="flex w-10/12">
                <Select
                    label="Especialista"
                    placeholder="¿Con quién tenes el turno?"
                    options={TYPE_MEDIC}
                    selectedOption={selectedOption}
                    handleOptionClick={handleOptionClick}
                    setIsOpen={setIsOpen}
                    isOpen={isOpen}
                    width="w-1/2"
                />
            </div>

            <div className="flex flex-wrap justify-between w-10/12 mt-4">
                <CustomSwitch
                    label="¿Querés un recordatorio de aplicación?"
                    id="reminder"
                    onChange={() => setReminder(!reminder)}
                    width="w-1/3"
                    checked={reminder}
                />
                {reminder && (
                    <CustomTimePicker
                        label="Hora del recordatorio"
                        value={hourRecordatory}
                        onChange={handleHourRecordatoryChange}
                        defaultHour={hourRecordatory && hourRecordatory}
                        width="w-2/5"
                    />
                )}

                <TextArea
                    placeholder="Describí tus sensaciones, estado de ánimo y cualquier otro síntoma que pueda ser de ayuda para los profesionales"
                    label="¿Cómo te sentís?"
                    id="notes"
                    width="w-10/12"
                    value={notes}
                    onChange={(e) => setNotes(e.target.value)}
                />
            </div>

            {error && <span className="text-red-500 mb-3">{error}</span>}

            <ButtonOrange onClick={handleSubmit} label="Enviar" width="w-1/3" />
        </div>
    );
};

export default MedicalVisitEventForm;
