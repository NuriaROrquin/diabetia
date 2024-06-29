import {Section} from "../../../components/section";
import {TitleSection} from "../../../components/titles";
import {TYPE_EVENTS, TYPE_MEDIC} from "../../../constants";
import {UploadFileOutlined} from "@mui/icons-material";
import {capitalizeFirstLetter} from "../../../helpers";
import {useState, useRef} from "react";
import {BlueLink, OrangeLink} from "../../../components/link";
import {InputWithLabel} from "../../../components/input";
import {Select} from "../../../components/selector";
import dayjs from "dayjs";
import {ButtonOrange} from "../../../components/button";
import {CustomDatePicker} from "../../../components/pickers";
import {useRouter} from "next/router";
import { v4 as uuidv4 } from 'uuid';
import {addMedicalExaminationEvent} from "../../../services/event.service";

const ReminderEvent = () => {
    const eventSelected = TYPE_EVENTS.filter((event) => event.id === 7)[0].title;
    const [isOpen, setIsOpen] = useState(false);
    const [selectedOption, setSelectedOption] = useState(null);
    const [date, setDate] = useState();
    const [selectedFile, setSelectedFile] = useState(null);
    const fileInputRef = useRef(null);
    const [error, setError] = useState(null);

    const router = useRouter();

    const handleUploadClick = () => {
        fileInputRef.current.click();
    };

    const saveFiles = (file) => {
       console.log('Guardando archivo:', file);
        setSelectedFile(file);
    };

    const handleFileChange = (event) => {
        const file = event.target.files[0];
        if (file) {
            if (file.type === "application/pdf" || file.type === "image/jpeg" || file.type === "image/png") {
                const reader = new FileReader();
                reader.onloadend = () => {
                    const base64String = reader.result.replace('data:', '').replace(/^.+,/, '');
                    const newFile = {
                        id: uuidv4(),
                        fileBase64: base64String
                    };
                    saveFiles(newFile);
                };
                reader.readAsDataURL(file);
            } else {
                alert("Solo se permiten archivos PDF, JPG y PNG.");
                // Limpiar la selección del archivo
                event.target.value = null;
            }
        }
    };

    const handleOptionClick = (option) => {
        setSelectedOption(option);
        setIsOpen(false);
    };

    const handleSubmit = () => {
        const dateFormatted = date ? date.format('DD-MM-YYYY') : null;

        const data = {
            "kindEventId": TYPE_EVENTS.find((event) => event.title === "ESTUDIOS")?.id,
            "eventDate": date,
            "file": selectedFile.fileBase64,
            "examinationType": document.getElementById("examinationType").value,
            "idProfessional":  selectedOption?.id
        }

        console.log(data)

        addMedicalExaminationEvent(data).then(() =>
            router.push("/calendar")
        ).catch((error) => {
            error.response.data ? setError(error.response.data) : setError("Hubo un error")            });
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
                    <CustomDatePicker
                        label="Ingresá la fecha del estudio"
                        date={date}
                        onChange={(e) => setDate(e)}
                        width="w-1/3"
                        defaultDate={date && date}
                    />

                    <InputWithLabel
                        label="Ingresa el estudio a guardar"
                        placeholder="¿De qué son los estudios?"
                        id="examinationType"
                        width="w-1/3"
                    />


                    <div className={`flex flex-wrap w-11/12 px-7 "justify-between": "justify-items-start"}`}>
                        <Select
                            label="Especialista"
                            placeholder="¿Con quién te hiciste el estudio?"
                            options={TYPE_MEDIC}
                            selectedOption={selectedOption}
                            handleOptionClick={handleOptionClick}
                            setIsOpen={setIsOpen}
                            isOpen={isOpen}
                            width="w-2/5"
                        />
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


                    </div>

                    {error && <span className="text-red-500 mb-3">{error}</span>}

                    <ButtonOrange onClick={handleSubmit} label="Enviar" width="w-1/3"/>

                </div>
            </div>
        </Section>
    )
}

export default ReminderEvent;