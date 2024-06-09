import React, { useState } from "react";
import dayjs from "dayjs";
import { useRouter } from "next/router";
import { Section } from "../../../components/section";
import { TitleSection } from "../../../components/titles";
import { TYPE_EVENTS, TYPE_INGREDIENTS, TYPE_UNITOFMEASUREMENT } from "../../../constants";
import {capitalizeFirstLetter, getEmailFromJwt} from "../../../helpers";
import { BlueLink, OrangeLink } from "../../../components/link";
import { InputWithLabel } from "../../../components/input";
import { Select } from "../../../components/selector";
import { ButtonOrange } from "../../../components/button";
import { CustomDatePicker, CustomTimePicker } from "../../../components/pickers";
import {addFoodEvent, addPhysicalEvent} from "../../../services/api.service";

const FoodEvent = () => {
    const eventSelected = TYPE_EVENTS.filter((event) => event.id === 3)[0].title;
    const [isOpenIngredients, setIsOpenIngredients] = useState([false]);
    const [selectedOptionIngredients, setSelectedOptionIngredients] = useState([null]);
    const [isOpenUnit, setIsOpenUnit] = useState([false]);
    const [selectedOptionUnit, setSelectedOptionUnit] = useState([null]);
    const [date, setDate] = useState(dayjs());
    const [Hour, setHour] = useState(dayjs());
    const [ingredients, setIngredients] = useState([{ ingredient: '', quantity: '', unit: '' }]);
    const [ingredientsQuantity, setIngredientsQuantity] = useState(1);

    const router = useRouter();

    const handleOptionClickIngredients = (option, index) => {
        const updatedOptions = [...selectedOptionIngredients];
        updatedOptions[index] = option;
        setSelectedOptionIngredients(updatedOptions);

        const updatedIsOpen = [...isOpenIngredients];
        updatedIsOpen[index] = false;
        setIsOpenIngredients(updatedIsOpen);
    };

    const handleOptionClickUnit = (option, index) => {
        const updatedOptions = [...selectedOptionUnit];
        updatedOptions[index] = option;
        setSelectedOptionUnit(updatedOptions);

        const updatedIsOpen = [...isOpenUnit];
        updatedIsOpen[index] = false;
        setIsOpenUnit(updatedIsOpen);
    };

    const handleAddIngredient = () => {
        setIngredientsQuantity(prevState => prevState + 1);
        setIngredients(prevState => [...prevState, { ingredient: '', quantity: '', unit: '' }]);
        setIsOpenIngredients(prevState => [...prevState, false]);
        setSelectedOptionIngredients(prevState => [...prevState, null]);
        setIsOpenUnit(prevState => [...prevState, false]);
        setSelectedOptionUnit(prevState => [...prevState, null]);
    };

    const handleRemoveIngredient = (index) => {
        if(ingredientsQuantity<=1)
        {return;}
        setIngredientsQuantity(prevState => prevState - 1);
        setIngredients(prevState => prevState.filter((_, i) => i !== index));
        setIsOpenIngredients(prevState => prevState.filter((_, i) => i !== index));
        setSelectedOptionIngredients(prevState => prevState.filter((_, i) => i !== index));
        setIsOpenUnit(prevState => prevState.filter((_, i) => i !== index));
        setSelectedOptionUnit(prevState => prevState.filter((_, i) => i !== index));
    };

    const handleSubmit = () => {
        const email = getEmailFromJwt();
        const data = {
            email: email,
            idKindEvent: 3,
            eventDate: date.format('YYYY-MM-DD'),
            ingredients: ingredients

        };
        addFoodEvent(data).then(() =>
            router.push("/calendar")
        );
    };

    return (
        <Section className="pt-12">
            <div className="container items-center flex w-full justify-center flex-col">
                <TitleSection className="text-white">¿Qué evento querés cargar?</TitleSection>
                <div className="flex w-full flex-wrap gap-y-6 gap-x-24 justify-center mt-8">
                    {TYPE_EVENTS.map((event) => (
                        <>
                            {event.title === eventSelected ?
                                <OrangeLink key={event.title} label={capitalizeFirstLetter(event.title)} width="w-1/6" href={event.link} />
                                :
                                <BlueLink key={event.title} label={capitalizeFirstLetter(event.title)} width="w-1/6" href={event.link} />
                            }
                        </>
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

                    <div className="flex flex-col w-full ">
                        {Array.from({ length: ingredientsQuantity }).map((_, index) => (
                            <div key={index} className="flex justify-between flex-wrap my-4 items-end">
                                <Select
                                    label="Ingrediente"
                                    placeholder="Elegí un ingrediente"
                                    options={TYPE_INGREDIENTS}
                                    selectedOption={selectedOptionIngredients[index]}
                                    handleOptionClick={(option) => handleOptionClickIngredients(option, index)}
                                    setIsOpen={(isOpen) => {
                                        const updatedIsOpen = [...isOpenIngredients];
                                        updatedIsOpen[index] = isOpen;
                                        setIsOpenIngredients(updatedIsOpen);
                                    }}
                                    isOpen={isOpenIngredients[index]}
                                    width="w-1/3"
                                />

                                <InputWithLabel
                                    label="Cantidad del ingrediente"
                                    placeholder="Escribí la cantidad"
                                    id={`IngredientsMeasurement_${index}`}
                                    width="w-1/5"
                                />

                                <Select
                                    label="Unidad de medida"
                                    placeholder="Elegí la unidad de medida"
                                    options={TYPE_UNITOFMEASUREMENT}
                                    selectedOption={selectedOptionUnit[index]}
                                    handleOptionClick={(option) => handleOptionClickUnit(option, index)}
                                    setIsOpen={(isOpen) => {
                                        const updatedIsOpen = [...isOpenUnit];
                                        updatedIsOpen[index] = isOpen;
                                        setIsOpenUnit(updatedIsOpen);
                                    }}
                                    isOpen={isOpenUnit[index]}
                                    width="w-1/5"
                                />

                                <ButtonOrange onClick={() => handleRemoveIngredient(index)} label="Eliminar" width="w-1/5 h-fit"  />
                            </div>
                        ))}
                    </div>

                    <ButtonOrange onClick={handleAddIngredient} label="Agregar Ingrediente" width="w-1/3" />

                    <ButtonOrange onClick={handleSubmit} label="Enviar" width="w-1/3" />

                </div>
            </div>
        </Section>
    )
}

export default FoodEvent;

