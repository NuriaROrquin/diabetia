import {Section} from "../../../components/section";
import {TitleSection} from "../../../components/titles";
import {TYPE_EVENTS, TYPE_MEDIC} from "../../../constants";
import {capitalizeFirstLetter, getEmailFromJwt} from "../../../helpers";
import {useState} from "react";
import {BlueLink, OrangeLink} from "../../../components/link";
import {TextArea, CustomSwitch} from "../../../components/input";
import dayjs from "dayjs";
import {ButtonOrange} from "../../../components/button";
import {Select} from "../../../components/selector";
import {addInsulinEvent} from "../../../services/user.service";
import {useRouter} from "next/router";
import {CustomDatePicker, CustomTimePicker} from "@/components/pickers";

const MedicalVisitEvent = () => {
    const eventSelected = TYPE_EVENTS.filter((event) => event.id === 6)[0].title;
    const [isOpen, setIsOpen] = useState(false);
    const [selectedOption, setSelectedOption] = useState(null);
    const [hour, setHour] = useState()
    const [date, setDate] = useState()
    const [reminder, setReminder] = useState(false);
    const router = useRouter();

    const handleOptionClick = (option) => {
        setSelectedOption(option);
        setIsOpen(false);
    };

    const handleSubmit = () => {
        const dateFormatted = date && hour ? date.format("YYYY-MM-DD") + 'T' + hour.format('HH:mm:ss') : null;
        const insulineQuantity = document.getElementById("insulineQuantity").value
        const notes = document.getElementById("notes").value;
        const email = getEmailFromJwt();


        const data = {
            "email": email,
            "idKindEvent": 1,
            "eventDate": dateFormatted,
            "freeNote": notes,
            "insulin": insulineQuantity,
        }

        addInsulinEvent(data).then(() =>
            router.push("/calendar")
        )
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
                            value={date}
                            onChange={(e) => setDate(e)}
                            defaultValue={dayjs()}
                            width="w-1/3"
                        />


                        <CustomTimePicker
                            label="Hora del evento"
                            value={hour}
                            onChange={setHour}
                            defaultValue={dayjs()}
                            width="w-1/3"
                        />


                    </div>


                    <div className="flex w-10/12 ">

                        <Select label="Especialista" placeholder="¿Con quién tenes el turno?" 
                            options={TYPE_MEDIC} selectedOption={selectedOption} 
                            handleOptionClick={handleOptionClick} 
                            setIsOpen={setIsOpen} isOpen={isOpen} 
                            width="w-1/2"
                        />
                        
                    </div>

                    <div className="flex flex-wrap justify-between  w-10/12 mt-4">
                                    <CustomSwitch label="¿Querés un recordatorio de aplicación?" id="reminder"
                                                  onChange={() => setReminder(!reminder)} width="w-1/3" checked={reminder}/>
                                    {reminder && (
                                    <CustomTimePicker
                                        label="Hora del recordatorio"
                                        value={hour}
                                        onChange={(e) => setHour(e)}
                                        defaultValue={dayjs()}
                                        width="w-2/5"
                                        className={reminder ? '' : 'hidden'}
                                    />
                                    )}
                    </div>

                    <ButtonOrange onClick={handleSubmit} label="Enviar" width="w-1/3"/>

                </div>
            </div>
        </Section>
    )
}

export default MedicalVisitEvent;