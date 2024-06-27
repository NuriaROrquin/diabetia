import {Section} from "../../../components/section";
import {TitleSection} from "../../../components/titles";
import {TYPE_EVENTS} from "../../../constants";
import {capitalizeFirstLetter} from "../../../helpers";
import {useState} from "react";
import {BlueLink, OrangeLink} from "../../../components/link";
import {useRouter} from "next/router";
import {InsulinEventForm} from "@/components/eventForm";
import {addInsulinEvent} from "../../../services/event.service";

const InsulineEvent = () => {
    const eventSelected = TYPE_EVENTS.filter((event) => event.id === 1)[0].title;
    const [hour, setHour] = useState()
    const [date, setDate] = useState()
    const router = useRouter();

    const onSubmit = () => {
        const dateFormatted = date && hour ? date.format("YYYY-MM-DD") + 'T' + hour.format('HH:mm:ss') : null;
        const insulineQuantity = document.getElementById("insulineQuantity").value
        const notes = document.getElementById("notes").value;

        const data = {
            "kindEventId": 1,
            "eventDate": dateFormatted,
            "freeNote": notes,
            "insulinInjected": insulineQuantity,
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
                <InsulinEventForm existingData={null} />
            </div>
        </Section>
    )
}

export default InsulineEvent;