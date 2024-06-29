import React, { useEffect, useState } from "react";
import dayjs from "dayjs";
import { useRouter } from "next/router";
import { Section } from "../../../components/section";
import { TitleSection } from "../../../components/titles";
import { TYPE_EVENTS } from "../../../constants";
import { capitalizeFirstLetter, getEmailFromJwt } from "../../../helpers";
import { BlueLink, OrangeLink } from "@/components/link";
import { InputWithLabel, TextArea } from "../../../components/input";
import { SelectSearch } from "../../../components/selector";
import { ButtonOrange } from "../../../components/button";
import { CustomDatePicker, CustomTimePicker } from "../../../components/pickers";
import { AddCircle, Delete } from "@mui/icons-material";
import {addFoodManuallyEvent} from "../../../services/event.service";
import {getIngredients} from "../../../services/ingredients.service";

const FoodEvent = () => {
    const eventSelected = TYPE_EVENTS.filter((event) => event.id === 2)[0].title;
    const [isOpenIngredients, setIsOpenIngredients] = useState([false]);
    const [selectedOptionIngredients, setSelectedOptionIngredients] = useState([null]);
    const [isOpenUnit, setIsOpenUnit] = useState([false]);
    const [selectedOptionUnit, setSelectedOptionUnit] = useState([null]);
    const [date, setDate] = useState(dayjs());
    const [hour, setHour] = useState(dayjs());
    const [ingredients, setIngredients] = useState([{ idIngredient: '', quantity: '', unit: '' }]);
    const [ingredientsQuantity, setIngredientsQuantity] = useState(1);
    const [ingredientsOptions, setIngredientsOptions] = useState([]);

    const router = useRouter();

    const handleOptionClickIngredients = (option, index) => {
        const updatedOptions = [...selectedOptionIngredients];
        updatedOptions[index] = option;
        setSelectedOptionIngredients(updatedOptions);

        const ingredient = ingredientsOptions.find(i => i.id === option.id);

        const updatedIngredients = [...ingredients];
        updatedIngredients[index] = { ...updatedIngredients[index], idIngredient: option.id, unit: ingredient.unit.unitName };
        setIngredients(updatedIngredients);

        const updatedIsOpen = [...isOpenIngredients];
        updatedIsOpen[index] = false;
        setIsOpenIngredients(updatedIsOpen);
    };

    const handleQuantityChange = (e, index) => {
        const updatedIngredients = [...ingredients];
        updatedIngredients[index] = { ...updatedIngredients[index], quantity: e.target.value };
        setIngredients(updatedIngredients);
    };

    const handleAddIngredient = () => {
        setIngredientsQuantity(prevState => prevState + 1);
        setIngredients(prevState => [...prevState, { idIngredient: '', quantity: '', unit: '' }]);
        setIsOpenIngredients(prevState => [...prevState, false]);
        setSelectedOptionIngredients(prevState => [...prevState, null]);
        setIsOpenUnit(prevState => [...prevState, false]);
        setSelectedOptionUnit(prevState => [...prevState, null]);
    };

    const handleRemoveIngredient = (index) => {
        if (ingredientsQuantity <= 1) { return; }
        setIngredientsQuantity(prevState => prevState - 1);
        setIngredients(prevState => prevState.filter((_, i) => i !== index));
        setIsOpenIngredients(prevState => prevState.filter((_, i) => i !== index));
        setSelectedOptionIngredients(prevState => prevState.filter((_, i) => i !== index));
        setIsOpenUnit(prevState => prevState.filter((_, i) => i !== index));
        setSelectedOptionUnit(prevState => prevState.filter((_, i) => i !== index));
    };

    const handleSubmit = () => {
        const email = getEmailFromJwt();
        const notes = document.getElementById("notes").value;
        const dateFormatted = date && hour ? date.format("YYYY-MM-DD") + 'T' + hour.format('HH:mm:ss') : null;

        const data = {
            eventDate: dateFormatted,
            kindEventId: 2,
            ingredients: ingredients,
            freeNote: notes
        };

        addFoodManuallyEvent(data).then((res) => {
            const queryParams = new URLSearchParams();
            queryParams.set('chConsumed', res.data.chConsumed);
            queryParams.set('insulinRecomended', res.data.insulinRecomended);
            router.push({
                pathname: "/event/foodTag/final",
                query: Object.fromEntries(queryParams.entries())
            });
        });
    };


    useEffect(() => {
        getIngredients().then((res) => {
            console.log(res.data)
            const ing = res.data.ingredients.map((i) => ({ id: i.idIngredient, title: i.name, unit: i.unit }));
            setIngredientsOptions(ing);
        });
    }, []);

    return (
        <Section className="pt-12">
            <div className="container items-center flex w-full justify-center flex-col">
                <TitleSection className="text-white mt-12">¿Qué evento querés cargar?</TitleSection>
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
                        value={hour}
                        onChange={setHour}
                        defaultValue={dayjs()}
                        width="w-1/3"
                    />

                    <TextArea placeholder="Describí tus sensaciones, estado de ánimo y cualquier otro síntoma que pueda ser de ayuda para los profesionales" label="¿Cómo te sentís?" id="notes" width="w-10/12" />

                    <div className="flex flex-col w-full items-center">
                        {Array.from({ length: ingredientsQuantity }).map((_, index) => (
                            <div key={index} className="flex justify-between flex-wrap my-4 gap-20 items-end w-10/12">
                                <SelectSearch
                                    label="Ingrediente"
                                    placeholder="Elegí un ingrediente"
                                    options={ingredientsOptions && ingredientsOptions}
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

                                <div className="w-2/5 flex items-end">
                                    <InputWithLabel
                                        label="Cantidad del ingrediente"
                                        placeholder="Escribí la cantidad"
                                        id={`IngredientsMeasurement_${index}`}
                                        width="w-3/4"
                                        onChange={(e) => handleQuantityChange(e, index)}
                                    />
                                    <div className="ml-4 w-1/4">{ingredients[index].unit}</div>
                                </div>

                                <button onClick={() => handleRemoveIngredient(index)} className="w-8">
                                    <Delete className="text-blue-primary" fontSize="large" />
                                </button>
                            </div>
                        ))}

                        <button onClick={handleAddIngredient}>
                            <AddCircle className="text-blue-primary mt-8" fontSize="large" alt="Agregar ingrediente" />
                        </button>
                    </div>

                    <ButtonOrange onClick={handleSubmit} label="Enviar" width="w-1/3" />
                </div>
            </div>
        </Section>
    );
};

export default FoodEvent;
