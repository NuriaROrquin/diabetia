import {Section} from "../../../components/section";
import {TitleSection} from "../../../components/titles";
import {TYPE_EVENTS, TYPE_DEVICES} from "../../../constants";
import {capitalizeFirstLetter} from "../../../helpers";
import {useState} from "react";
import {BlueLink, OrangeLink} from "../../../components/link";
import {TextArea, InputWithLabel} from "../../../components/input";
import {Select} from "../../../components/selector";
import dayjs from "dayjs";
import {ButtonOrange} from "../../../components/button";
import {addGlucoseEvent} from "../../../services/api.service";
import {useCookies} from "react-cookie";
import {useRouter} from "next/router";
import {CustomDatePicker, CustomTimePicker} from "@/components/pickers";

const GlycemiaEvent = () => {
    const eventSelected = TYPE_EVENTS.filter((event) => event.id === 2)[0].title;
    const [isOpen, setIsOpen] = useState(false);
    const [selectedOption, setSelectedOption] = useState(null);
    const [Hour, setHour] = useState()
    const [date, setDate] = useState()
    const [cookies, _setCookie, _removeCookie] = useCookies(['email']);

    const router = useRouter();

    const handleOptionClick = (option) => {
        setSelectedOption(option);
        setIsOpen(false);
    };

    const handleSubmit = () => {
        const device = selectedOption.id;
        const dateFormatted = date ? date.format('YYYY-MM-DD') : null;
        const glycemiaMeasurement = document.getElementById("glycemiaMeasurement").value
        const notes = document.getElementById("notes").value;
        const email = cookies.email;

        const data = {
            "email": email,
            "idKindEvent": 3,
            "eventDate": dateFormatted,
            "freeNote": notes,
            "glucose": glycemiaMeasurement,
            "idDevicePatient": device ?? null,
        }

        addGlucoseEvent(data).then(() =>
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


                    <CustomTimePicker
                        label="Hora de medición"
                        value={Hour}
                        onChange={setHour}
                        defaultValue={dayjs()}
                        width="w-1/3"
                    />

                    <Select label="Dispositivo" placeholder="¿Cómo se tómo la glucosa?" options={TYPE_DEVICES} selectedOption={selectedOption} handleOptionClick={handleOptionClick} setIsOpen={setIsOpen} isOpen={isOpen} width="w-1/3"/>

                    <InputWithLabel label="Cantidad de mg/dL de glucosa" placeholder="Escribí tu medición"  id="glycemiaMeasurement" width="w-1/3"/>


                    <TextArea placeholder="Describí tus sensaciones, estado de ánimo y cualquier otro síntoma que pueda ser de ayuda para los profesionales" label="¿Cómo te sentís?" id="notes" width="w-10/12"/>

                    <ButtonOrange onClick={handleSubmit} label="Enviar" width="w-1/3"/>
                </div>
            </div>
        </Section>
    )
}

export default GlycemiaEvent;