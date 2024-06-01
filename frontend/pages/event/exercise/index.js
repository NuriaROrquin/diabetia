import {Section} from "../../../components/section";
import {TitleSection} from "../../../components/titles";
import {TYPE_EVENTS, TYPE_EXERCISES} from "../../../constants";
import {capitalizeFirstLetter} from "../../../helpers";
import {useState} from "react";
import {BlueLink, OrangeLink} from "../../../components/link";
import {TextArea} from "../../../components/input";
import {Select} from "../../../components/selector";
import dayjs from "dayjs";
import {ButtonOrange} from "../../../components/button";
import {CustomDatePicker, CustomTimePicker} from "../../../components/pickers";
import {addPhysicalEvent} from "../../../services/api.service";
import {useRouter} from "next/router";
import {useCookies} from "react-cookie";

const ExerciseEvent = () => {
    const eventSelected = TYPE_EVENTS.filter((event) => event.id === 1)[0].title;
    const [isOpen, setIsOpen] = useState(false);
    const [selectedOption, setSelectedOption] = useState(null);
    const [startHour, setStartHour] = useState()
    const [endHour, setEndHour] = useState()
    const [date, setDate] = useState()
    const [cookies, _setCookie, _removeCookie] = useCookies(['email']);


    const router = useRouter();

    const handleOptionClick = (option) => {
        setSelectedOption(option);
        setIsOpen(false);
    };

    const handleSubmit = () => {
        const exercise = selectedOption;
        const dateFormatted = date ? date.format('DD-MM-YYYY') : null;
        const start = startHour ? startHour.format('HH:mm:ss') : null;
        const end = endHour ? endHour.format('HH:mm:ss') : null;
        const notes = document.getElementById("notes").value;
        const email = cookies.email;

        const data = {
            "email": email,
            "idKindEvent": 4,
            "eventDate": "2024-05-22T23:03:17.219Z",
            "freeNote": notes,
            "physicalActivity": selectedOption.id,
            "iniciateTime": start,
            "finishTime": start
        }

        addPhysicalEvent(data).then(() =>
            router.push("/calendar")
        )
    }

    return(
        <Section className="pt-12">
            <div className="container items-center flex w-full justify-center flex-col">
                <TitleSection className="text-white">¿Qué evento querés cargar?</TitleSection>
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
                    <CustomDatePicker
                        label="Ingresá una fecha"
                        value={date}
                        onChange={(e) => setDate(e)}
                        defaultValue={dayjs()}
                        width="w-1/3"
                    />

                    <Select label="Actividad" placeholder="Elegí una actividad" options={TYPE_EXERCISES} selectedOption={selectedOption} handleOptionClick={handleOptionClick} setIsOpen={setIsOpen} isOpen={isOpen} width="w-1/3"/>

                    <CustomTimePicker
                        label="Hora de comienzo"
                        value={startHour}
                        onChange={setStartHour}
                        defaultValue={dayjs()}
                        width="w-1/3"
                    />

                    <CustomTimePicker
                        label="Hora de finalización"
                        value={endHour}
                        onChange={(e) => setEndHour(e)}
                        defaultValue={dayjs()}
                        width="w-1/3"
                    />

                    <TextArea placeholder="¿Cómo te sentiste?" label="Describí tu experiencia" id="notes" width="w-10/12"/>

                    <ButtonOrange onClick={handleSubmit} label="Enviar" width="w-1/3"/>
                </div>
            </div>
        </Section>
    )
}

export default ExerciseEvent;