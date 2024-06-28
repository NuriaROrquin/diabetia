import {Section} from "../../../components/section";
import {TitleSection} from "../../../components/titles";
import {TYPE_EVENTS, TYPE_MEDIC} from "../../../constants";
import {capitalizeFirstLetter} from "../../../helpers";
import {useState} from "react";
import {BlueLink, OrangeLink} from "../../../components/link";
import {CustomSwitch, TextArea} from "../../../components/input";
import dayjs from "dayjs";
import {ButtonOrange} from "../../../components/button";
import {Select} from "../../../components/selector";
import {addMedicalVisitEvent} from "../../../services/event.service";
import {useRouter} from "next/router";
import {CustomDatePicker, CustomTimePicker} from "@/components/pickers";

const MedicalVisitEvent = () => {
    const eventSelected = TYPE_EVENTS.filter((event) => event.id === 6)[0].title;
    const [isOpen, setIsOpen] = useState(false);
    const [selectedOption, setSelectedOption] = useState(null);
    const [hour, setHour] = useState(dayjs());
    const [hourRecordatory, setHourRecordatory] = useState();
    const [date, setDate] = useState(dayjs());
    const [reminder, setReminder] = useState(false);
    const [notes, setNotes] = useState('');
    const [error, setError] = useState('');
    const router = useRouter();

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
        if (reminder && hourRecordatory.isAfter(hour)) {
            setError('La hora del recordatorio no puede ser posterior a la hora del evento.');
            return;
        }

        const dateFormatted = date && hour ? date.format("YYYY-MM-DD") + 'T' + hour.format('HH:mm:ss') : null;
        const dateFormattedRecordatory = date && hourRecordatory ? date.format("YYYY-MM-DD") + 'T' + hourRecordatory.format('HH:mm:ss') : null;
        const notes = document.getElementById("notes").value;

        const data = {
            "eventDate": dateFormatted,
            "kindEventId": TYPE_EVENTS.find((event) => event.title === "VISITA MÉDICA")?.id,
            "professionalId": selectedOption?.id,
            "recordatory": reminder,
            "hourRecordatory": dateFormattedRecordatory,
            "description": notes,
        }

        if (router.query.id) {
            /*editGlucoseEvent({ ...data, eventId: router.query.id }).then(() =>
                router.push("/calendar")*/
            console.log(data)
        } else {
            console.log(data)
            addMedicalVisitEvent({...data}).then(() =>
                router.push("/calendar")
            );
        }
    }

    return(
        <Section className="pt-12">
            <div className="container items-center flex w-full justify-center flex-col">
                <TitleSection className="text-white mt-12">¿Qué evento querés cargar?</TitleSection>
                <div className="flex w-full flex-wrap gap-y-6 gap-x-24 justify-center mt-8">
                    {TYPE_EVENTS.map((event) => {
                        return(
                            <>
                                {event.title === eventSelected ?
                                    <OrangeLink key={event.title} label={capitalizeFirstLetter(event.title)} width="w-1/6" href={event.link} />
                                    :
                                    <BlueLink key={event.title} label={capitalizeFirstLetter(event.title)} width="w-1/6" href={event.link} />
                                }
                            </>
                        )
                    })}
                </div>

                {/* FORMULARIO */}
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
                                className={reminder ? '' : 'hidden'}
                            />
                        )}
                        {error && (
                            <p className="text-red-500">{error}</p>
                        )}
                        <TextArea
                            placeholder="Describí tus sensaciones, estado de ánimo y cualquier otro síntoma que pueda ser de ayuda para los profesionales"
                            label="¿Cómo te sentís?"
                            id="notes"
                            width="w-10/12"
                            value={notes}
                            defaultValue={notes && notes}
                            onChange={(e) => setNotes(e.target.value)}
                        />
                    </div>

                    <ButtonOrange onClick={handleSubmit} label="Enviar" width="w-1/3" />
                </div>
            </div>
        </Section>
    )
}

export default MedicalVisitEvent;
