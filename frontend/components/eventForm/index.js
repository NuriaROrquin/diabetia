import { useState, useEffect } from "react";
import { useRouter } from "next/router";
import { Select } from "@/components/selector";
import { TextArea, InputWithLabel } from "@/components/input";
import { ButtonOrange } from "@/components/button";
import { CustomDatePicker, CustomTimePicker } from "@/components/pickers";
import {GENDER, TYPE_DEVICES} from "../../constants";
import dayjs from "dayjs";
import { addGlucoseEvent, editGlucoseEvent } from "../../services/api.service";
import { getEmailFromJwt } from "../../helpers";

export const GlucoseEventForm = ({ existingData }) => {
    const [isOpen, setIsOpen] = useState(false);
    const [selectedOption, setSelectedOption] = useState(null);
    const [hour, setHour] = useState(dayjs());
    const [date, setDate] = useState(dayjs());
    const [glucoseLevel, setGlucoseLevel] = useState('');
    const [notes, setNotes] = useState('');
    const router = useRouter();

    useEffect(() => {
        console.log("Existing Data: ", existingData);

        if (existingData) {
            var existingDate = dayjs(existingData.dateEvent).format("YYYY-MM-DD");
            var existingHour = dayjs(existingData.dateEvent).format('HH:mm ss');
            var device = TYPE_DEVICES.find(d => d.id === existingData.idDevice)
            setSelectedOption(device);
            setDate(existingDate);
            setHour(existingHour);
            setGlucoseLevel(existingData.glucoseLevel);
            setNotes(existingData.freeNote);
        }

    }, [existingData]);

    const handleOptionClick = (option) => {
        setSelectedOption(option);
        setIsOpen(false);
    };

    const handleSubmit = () => {
        const device = selectedOption ? selectedOption.id : null;
        const email = getEmailFromJwt();
        var dateFormatted;
        if(!router.query.id){
            dateFormatted = date.format("YYYY-MM-DD") + 'T' + hour.format('HH:mm:ss');
        }else{
            dateFormatted = date + 'T' + hour.format('HH:mm:ss')
        }

        const data = {
            "email": email,
            "idKindEvent": 3,
            "eventDate": dateFormatted,
            "freeNote": notes,
            "glucose": glucoseLevel,
            "idDevicePacient": device
        };
        if(router.query.id){
            editGlucoseEvent({...data, idEvent: router.query.id}).then(() =>
                router.push("/calendar")
            );

        }else{
            addGlucoseEvent(data).then(() =>
                router.push("/calendar")
            );
        }

    };

    return (
        <div className="bg-white rounded-xl w-full flex flex-wrap text-gray-primary py-20 px-44 my-12 justify-around gap-x-2 gap-y-12">
            <CustomDatePicker
                label="Ingresá una fecha"
                date={dayjs(date)}
                onChange={(e) => setDate(e)}
                width="w-1/3"
                defaultDate = {date && date}
            />
            <CustomTimePicker
                label="Hora de medición"
                hour={dayjs(hour)}
                onChange={setHour}
                width="w-1/3"
                defaultHour={hour && hour}
            />
            <Select
                label="Dispositivo"
                placeholder="¿Cómo se tomó la glucosa?"
                options={TYPE_DEVICES}
                selectedOption={selectedOption}
                handleOptionClick={handleOptionClick}
                setIsOpen={setIsOpen}
                isOpen={isOpen}
                width="w-1/3"
            />
            <InputWithLabel
                label="Cantidad de mg/dL de glucosa"
                placeholder="Escribí tu medición"
                id="glycemiaMeasurement"
                width="w-1/3"
                value={glucoseLevel}
                defaultValue={glucoseLevel && glucoseLevel}
                onChange={(e) => setGlucoseLevel(e.target.value)}
            />
            <TextArea
                placeholder="Describí tus sensaciones, estado de ánimo y cualquier otro síntoma que pueda ser de ayuda para los profesionales"
                label="¿Cómo te sentís?"
                id="notes"
                width="w-10/12"
                value={notes}
                defaultValue={notes && notes}
                onChange={(e) => setNotes(e.target.value)}
            />
            <ButtonOrange onClick={handleSubmit} label="Enviar" width="w-1/3"/>
        </div>
    );
};
