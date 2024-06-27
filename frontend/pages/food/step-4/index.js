import {useEffect, useState} from "react";
import {Section} from "@/components/section";
import {Input} from "@/components/input";
import {ButtonOrange} from "@/components/button";
import {useRouter} from "next/router";
import {useAIData} from "../../../context";
import {OrangeLink} from "../../../components/link";
import {getEmailFromJwt} from "../../../helpers";
import dayjs from "dayjs";
import utc from 'dayjs/plugin/utc';
import timezone from 'dayjs/plugin/timezone';
import {tagRegistration} from "../../../services/tag.service";

dayjs.extend(utc);
dayjs.extend(timezone);

const StepFour = () => {
    const { imagesUploaded, updateAIDataDetected, updateCarbohydratesConsumed } = useAIData();
    const [inputData, setInputData] = useState({});
    const router = useRouter();

    useEffect(() => {
        const initialInputData = {};
        imagesUploaded.forEach(tag => {
            initialInputData[tag.id] = { ...tag };
        });
        setInputData(initialInputData);
    }, [imagesUploaded]);

    const handleSubmit = () => {
        const newData = imagesUploaded.map(tag => ({
            ...tag,
            grPerPortion: parseFloat(document.getElementById(`grPerPortion_${tag.id}`).value),
            chInPortion: parseFloat(document.getElementById(`chPerPortion_${tag.id}`).value),
            portion: parseFloat(document.getElementById(`portion_${tag.id}`).value)
        }));

        updateAIDataDetected(newData);

        const tagsToRegister = imagesUploaded.map(tag => ({
            id: tag.id,
            portion: tag.portion ? parseFloat(document.getElementById(`portion_${tag.id}`).value) : 0,
            grPerPortion: tag.grPerPortion ? parseFloat(document.getElementById(`grPerPortion_${tag.id}`).value) : 0,
            chInPortion: tag.chInPortion ? parseFloat(document.getElementById(`chPerPortion_${tag.id}`).value) : 0
        }));

        const email = getEmailFromJwt();

        const eventDate = dayjs().tz('Etc/GMT+3')

        const requestData = {
            email: email,
            eventDate: eventDate.format('YYYY-MM-DDTHH:mm:ss.SSSZ'),
            tags: tagsToRegister
        };

        tagRegistration(requestData).then((response) => {
            updateCarbohydratesConsumed(response.data)
            router.push("/food/step-final");
        })
    };

    return(
        <Section>
            <div className="container">
                <div
                    className="bg-white rounded-xl w-full flex flex-col flex-wrap text-gray-primary py-20 px-44 my-12 justify-around gap-x-2 gap-y-12">

                    <h4 className="font-semibold text-3xl text-center">Etiquetas detectadas</h4>
                    {imagesUploaded.map((tag) => (
                        <div key={tag.id} className="flex w-full gap-12">
                            <div className="w-56 h-56 flex overflow-hidden rounded-xl">
                                <img
                                    src={`data:image/png;base64,${tag.imageBase64}`}
                                    className="w-full object-cover"
                                    alt="Etiqueta"
                                />
                            </div>
                            <div className="flex flex-col w-full">

                                <div className="flex flex-wrap flex-row items-center gap-2">
                                    <p>Los gramos equivalentes a una porción son </p>
                                    <Input id={`grPerPortion_${tag.id}`} type="number" placeholder="Gramos de una porción"
                                           defaultValue={tag.grPerPortion} width="w-20"/>
                                    <p>gramos</p>
                                </div>
                                <div className="flex flex-wrap flex-row items-center gap-2">
                                    <p>Los carbohidratos por porción detectados son </p>
                                    <Input id={`chPerPortion_${tag.id}`} type="number" placeholder="Carbohidratos de una porción"
                                           defaultValue={tag.chInPortion} width="w-20"/>
                                    <p>gramos</p>
                                </div>
                                <div className="flex flex-wrap flex-row items-center gap-2">
                                    <p>La porción ingerida es de </p>
                                    <Input id={`portion_${tag.id}`} type="number" placeholder="Porcion ingerida"
                                           defaultValue={tag.portion} width="w-20"/>
                                    <p>gramos</p>
                                </div>
                            </div>
                        </div>
                    ))}
                    <div className="flex justify-between">
                        <OrangeLink href="/food/step-3" label="Atrás" width="w-1/4" background="bg-gray-400 hover:bg-gray-600"/>
                        <ButtonOrange onClick={handleSubmit} label="Enviar" width="w-1/4"/>
                    </div>
                </div>
            </div>
        </Section>
    )
}

export default StepFour;