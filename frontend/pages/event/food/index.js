import {Section} from "../../../components/section";
import {TitleSection} from "../../../components/titles";
import {TYPE_EVENTS, TYPE_INGREDIENTS, TYPE_UNITOFMEASUREMENT} from "../../../constants";
import {capitalizeFirstLetter} from "../../../helpers";
import React, {useState} from "react";
import {BlueLink, OrangeLink} from "../../../components/link";
import {InputWithLabel} from "../../../components/input";
import {Select} from "../../../components/selector";
import dayjs from "dayjs";
import {ButtonOrange} from "../../../components/button";
import {CustomDatePicker, CustomTimePicker} from "../../../components/pickers";
import {addPhysicalEvent} from "../../../services/api.service";
import {useRouter} from "next/router";
import {useCookies} from "react-cookie";


const ExerciseEvent = () => {
    const eventSelected = TYPE_EVENTS.filter((event) => event.id === 3)[0].title;
    const [isOpenIngredients, setIsOpenIngredients] = useState(false);
    const [selectedOptionIngredients, setSelectedOptionIngredients] = useState(null);
    const [isOpenUnit, setIsOpenUnit] = useState(false);
    const [selectedOptionUnit, setSelectedOptionUnit] = useState(null);
    const [startHour, setStartHour] = useState()
    const [endHour, setEndHour] = useState()
    const [date, setDate] = useState()
    const [Hour, setHour] = useState()
    const [cookies, _setCookie, _removeCookie] = useCookies(['email']);
    const [ingredients, setIngredients] = useState([{ ingredient: '', quantity: '', unit: '' }]);


    const router = useRouter();

    const handleOptionClickIngredients = (option, index) => {
        const newIngredients = [...ingredients];
        newIngredients[index].ingredient = option;
        setIngredients(newIngredients);
    };

    const handleOptionClickUnit = (option, index) => {
        const newIngredients = [...ingredients];
        newIngredients[index].unit = option;
        setIngredients(newIngredients);
    };

    const handleChangeQuantity = (event, index) => {
        const newIngredients = [...ingredients];
        newIngredients[index].quantity = event.target.value;
        setIngredients(newIngredients);
    };

    const handleSubmit = () => {
        const email = cookies.email;
        const data = {
            "email": email,
            "idKindEvent": 4,
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

    const handleAddIngredient = () => {
        setIngredients([...ingredients, { ingredient: '', quantity: '', unit: '' }]);
    };

    return(
        <Section className="pt-12">
            <div className="container items-center flex w-full justify-center flex-col">
                <TitleSection className="text-white">¿Qué evento querés cargar?</TitleSection>
                <div className="flex w-full flex-wrap gap-y-6 gap-x-24 justify-center mt-8">
                    {TYPE_EVENTS.map((event) => (
                        event.title === eventSelected ?
                            <OrangeLink key={event.title} label={capitalizeFirstLetter(event.title)} width="w-1/6" href={event.link} />
                            :
                            <BlueLink key={event.title} label={capitalizeFirstLetter(event.title)} width="w-1/6" href={event.link} />
                    ))}
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
                        label="Elija el horario de la comida"
                        value={Hour}
                        onChange={setHour}
                        defaultValue={dayjs()}
                        width="w-1/3"
                    />

                    {ingredients.map((ingredient, index) => (
                        // eslint-disable-next-line react/jsx-no-undef
                        <React.Fragment key={index}>
                            <Select
                                label="Ingrediente"
                                placeholder="Elegí un ingrediente"
                                options={TYPE_INGREDIENTS}
                                selectedOption={ingredient.ingredient}
                                handleOptionClick={(option) => handleOptionClickIngredients(option, index)}
                                setIsOpen={() => {}} // Si necesitas que el Select se abra/cierre, deberías manejarlo aquí
                                isOpen={false} // Si necesitas que el Select se abra/cierre, deberías manejarlo aquí
                                width="w-1/3"
                            />

                            <InputWithLabel
                                label="Cantidad del ingrediente"
                                placeholder="Escribí la cantidad"
                                value={ingredient.quantity}
                                onChange={(event) => handleChangeQuantity(event, index)}
                                width="w-1/5"
                            />

                            <Select
                                label="Unidad de medida"
                                placeholder="Elegí la unidad de medida"
                                options={TYPE_UNITOFMEASUREMENT}
                                selectedOption={ingredient.unit}
                                handleOptionClick={(option) => handleOptionClickUnit(option, index)}
                                setIsOpen={() => {}} // Si necesitas que el Select se abra/cierre, deberías manejarlo aquí
                                isOpen={false} // Si necesitas que el Select se abra/cierre, deberías manejarlo aquí
                                width="w-1/5"
                            />
                        </React.Fragment>
                    ))}

                    <ButtonOrange onClick={handleSubmit} label="Enviar" width="w-1/3"/>
                    <button type="button" onClick={handleAddIngredient}>Agregar Ingrediente</button>
                </div>
            </div>
        </Section>
    )
}

export default ExerciseEvent;