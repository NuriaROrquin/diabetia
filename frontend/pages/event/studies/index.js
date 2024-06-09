import {Section} from "../../../components/section";
import {TitleSection} from "../../../components/titles";
import {TYPE_EVENTS, TYPE_REMINDERTIME} from "../../../constants";
import {UploadFileOutlined} from "@mui/icons-material";
import {capitalizeFirstLetter, getEmailFromJwt} from "../../../helpers";
import {useState, useRef} from "react";
import {BlueLink, OrangeLink} from "../../../components/link";
import {InputWithLabel, CustomSwitch} from "../../../components/input";
import {Select} from "../../../components/selector";
import dayjs from "dayjs";
import {ButtonOrange} from "../../../components/button";
import {CustomDatePicker} from "../../../components/pickers";
import {addPhysicalEvent} from "../../../services/api.service";
import {useRouter} from "next/router";
import {useCookies} from "react-cookie";

const ReminderEvent = () => {
    const eventSelected = TYPE_EVENTS.filter((event) => event.id === 7)[0].title;
    const [isOpen, setIsOpen] = useState(false);
    const [selectedOption, setSelectedOption] = useState(null);
    const [reminder, setReminder] = useState(false);
    const [date, setDate] = useState();
    const fileInputRef = useRef(null);

    const router = useRouter();

    const handleUploadClick = () => {
        fileInputRef.current.click();
    };

    const handleFileChange = (event) => {
        const file = event.target.files[0];
        if (file) {
            const reader = new FileReader();
            reader.onloadend = () => {
                const base64String = reader.result.replace('data:', '').replace(/^.+,/, '');
                const newImage = { id: uuidv4(), imageBase64: base64String };
                saveFiles(newImage);
                router.push("/event")
            };
            reader.readAsDataURL(file);
        }
    };

    const handleOptionClick = (option) => {
        setSelectedOption(option);
        setIsOpen(false);
    };

    const handleSubmit = () => {
        const exercise = selectedOption;
        const dateFormatted = date ? date.format('DD-MM-YYYY') : null;
        const notes = document.getElementById("notes").value;
        const email = getEmailFromJwt();

        const data = {
            "email": email,
            "idKindEvent": 7,
            "eventDate": "2024-05-22T23:03:17.219Z",
            "freeNote": notes,
            "physicalActivity": selectedOption.id,
            "iniciateTime": start,
            "finishTime": end
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
                        label="Ingresá la fecha del estudio"
                        value={date}
                        onChange={(e) => setDate(e)}
                        defaultValue={dayjs()}
                        width="w-1/2"
                    />

                    <InputWithLabel label="Ingresa el estudio a guardar" placeholder="¿De qué son los estudios?"  id="" width="w-1/3"/>

                    <div className={`flex flex-wrap w-11/12 ${reminder ? "justify-between": "justify-items-start"}`}>

                     <CustomSwitch label="¿Necesitás recordatorio para repetir estudio?" id="reminder" onChange={() => setReminder(!reminder)}
                                  width="w-1/2"/>

                        {reminder && (
                            <>
                                <Select label="¿En cuanto tiempo?" placeholder=""
                                options={TYPE_REMINDERTIME}
                                selectedOption={selectedOption}
                                handleOptionClick={handleOptionClick}
                                setIsOpen={setIsOpen} isOpen={isOpen}
                                width="w-1/3"/>

                                <div className="bg-white w-full flex flex-col rounded-xl p-6 justify-center items-center"
                                    onClick={handleUploadClick}>
                                        <UploadFileOutlined className="text-orange-primary h-20 w-20"/>
                                        <span className="text-lg text-gray-primary font-semibold">Hacé click para subir un archivo</span>
                                        <span className="text-lg text-gray-primary">desde tu dispositivo</span>
                                </div>

                                <input
                                    type="file"
                                    ref={fileInputRef}
                                    style={{display: 'none'}}
                                    onChange={handleFileChange}
                                    />
                            </>
                        )}

                    </div>


                    <ButtonOrange onClick={handleSubmit} label="Enviar" width="w-1/3"/>

                </div>
            </div>
        </Section>
    )
}

export default ReminderEvent;