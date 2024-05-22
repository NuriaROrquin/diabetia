import {Section} from "../../../components/section";
import {TitleSection} from "../../../components/titles";
import {TYPE_EVENTS, TYPE_DEVICES} from "../../../constants";
import {capitalizeFirstLetter} from "../../../helpers";
import {useState} from "react";
import {BlueLink, OrangeLink} from "../../../components/link";
import {TextArea, InputWithLabel, CustomSwitch} from "../../../components/input";
import {Select} from "../../../components/selector";
import dayjs from "dayjs";
import {ButtonOrange} from "../../../components/button";
import {CustomDatePicker, CustomTimePicker} from "../../../components/pickers";

const InitialFormStep1 = () => {
    const eventSelected = TYPE_EVENTS.filter((event) => event.id === 2)[0].title;
    const [isOpen, setIsOpen] = useState(false);
    const [selectedOption, setSelectedOption] = useState(null);
    const [date, setDate] = useState()

    const handleOptionClick = (option) => {
        setSelectedOption(option);
        setIsOpen(false);
    };

    const handleSubmit = () => {
        const name = document.getElementById("name").value;
        const surname = document.getElementById("surname").value;
        const dateFormatted = date ? date.format('DD-MM-YYYY') : null;
        const weight = document.getElementById("weight").value;
        const email = document.getElementById("email").value;
        const phone = document.getElementById("phone").value;

        console.log("Datos del formulario:", name, surname, dateFormatted, weight, email, phone);
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
                    <InputWithLabel label="Nombre" placeholder="Juan"  id="name" width="w-1/3"/>
                    <InputWithLabel label="Apellido" placeholder="Perez"  id="surname" width="w-1/3"/>
                    <CustomDatePicker
                        label="Fecha de nacimiento"
                        value={date}
                        onChange={(e) => setDate(e)}
                        defaultValue={dayjs()}
                        width="w-1/3"
                    />
                    <InputWithLabel label="Peso" placeholder="Ingresá tu peso"  id="weight" width="w-1/3"/>
                    <InputWithLabel label="Correo Electrónico" placeholder="email@diabetia.com.ar"  id="email" width="w-1/3"/>
                    <InputWithLabel label="Teléfono" placeholder="1234-5678"  id="phone" width="w-1/3"/>

                    <ButtonOrange onClick={handleSubmit} label="Siguiente" width="w-1/3"/>
                </div>
            </div>
        </Section>
    )
}

export default InitialFormStep1;