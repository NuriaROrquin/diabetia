import {Section} from "../../../components/section";
import {TitleSection} from "../../../components/titles";
import {TYPE_EVENTS} from "../../../constants";
import {capitalizeFirstLetter, } from "../../../helpers";
import {BlueLink, OrangeLink} from "../../../components/link";
import {FreeNoteEventForm} from "@/components/eventForm";

const FreeNoteEvent = () => {
    const eventSelected = TYPE_EVENTS.filter((event) => event.id === 8)[0].title;

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
                <FreeNoteEventForm existingData={null} />
            </div>
        </Section>
    )
}

export default FreeNoteEvent;