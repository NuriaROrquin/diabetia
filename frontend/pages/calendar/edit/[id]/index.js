import {Section} from "@/components/section";
import {useRouter} from "next/router";

const EditEvent = () => {
    const router = useRouter();

    //TODO: UseEffect para ir a buscar los datos del idEvento 19

    return(
        <Section className="pt-12 pb-6">
            <div>
                Edicion de evento del id {router.query.id}
            </div>
        </Section>
    )
}

export default EditEvent;