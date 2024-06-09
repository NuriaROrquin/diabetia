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
import {addGlucoseEvent, addInsulinEvent} from "../../../services/api.service";
import {useCookies} from "react-cookie";
import {useRouter} from "next/router";
import {CustomDatePicker, CustomTimePicker} from "@/components/pickers";

const InsulineEvent = () => {
    const eventSelected = TYPE_EVENTS.filter((event) => event.id === 4)[0].title;
    const [Hour, setHour] = useState()
    const [date, setDate] = useState()
    const [cookies, _setCookie, _removeCookie] = useCookies(['email']);
    const [startHour, setStartHour] = useState()
    const router = useRouter();


    const handleSubmit = () => {
        const dateFormatted = date ? date.format('YYYY-MM-DD') : null;
        const start = startHour ? startHour.format('HH:mm:ss') : null;
        const insulineQuantity = document.getElementById("insulineQuantity").value
        const notes = document.getElementById("notes").value;
        const email = cookies.email;

        const data = {
            "email": email,
            "idKindEvent": 4,
            "eventDate": dateFormatted,
            "freeNote": notes,
            "Insulin": insulineQuantity,
            //"hora": start ?? null
        }

        addInsulinEvent(data).then(() =>
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
                <div className="bg-white rounded-xl w-full flex flex-wrap text-gray-primary py-20 px-44 my-12 justify-around gap-x-2 gap-y-12 items-start">

                    <div className="flex justify-between w-10/12">
                        <CustomDatePicker
                            label="Ingresá una fecha"
                            value={date}
                            onChange={(e) => setDate(e)}
                            defaultValue={dayjs()}
                            width="w-1/3"
                        />


                        <CustomTimePicker
                            label="Hora de administración"
                            value={Hour}
                            onChange={setHour}
                            defaultValue={dayjs()}
                            width="w-1/3"
                        />
                    </div>
                    <div className="flex w-10/12 ">
                        <InputWithLabel label="¿Cuantas unidades de insulina se inyectó?" placeholder="Escribí la cantidad de unidades administración"  id="insulineQuantity" width="w-1/2"/>
                    </div>

                    <TextArea placeholder="Describí tus sensaciones, estado de ánimo y cualquier otro síntoma que pueda ser de ayuda para los profesionales" label="¿Cómo te sentís?" id="notes" width="w-10/12"/>

                    <ButtonOrange onClick={handleSubmit} label="Enviar" width="w-1/3"/>

                </div>
            </div>
        </Section>
    )
}

export default InsulineEvent;