import React, { useEffect, useState } from "react";
import dayjs from "dayjs";
import { TextArea, InputWithLabel } from "@/components/input";
import { SelectSearch } from "@/components/selector";
import { ButtonOrange } from "@/components/button";
import { CustomDatePicker, CustomTimePicker } from "@/components/pickers";
import { addFoodEvent, editFoodEvent, getIngredients } from "../../services/api.service";
import { getEmailFromJwt } from "../../helpers";
import { AddCircle, Delete } from "@mui/icons-material";
import { useRouter } from "next/router";

const FoodEventForm = ({ existingData }) => {
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

    useEffect(() => {
        if (existingData) {
            const existingDate = dayjs(existingData.dateEvent);
            const existingHour = dayjs(existingData.dateEvent);
            setDate(existingDate);
            setHour(existingHour);
            setIngredients(existingData.ingredientName || [{ idIngredient: '', quantity: '', unit: '' }]);
            setIngredientsQuantity(existingData.ingredientName.length || 1);
        }
    }, [existingData]);

    useEffect(() => {
        getIngredients().then((res) => {
            const ing = res.data.ingredients.map((i) => ({ id: i.idIngredient, title: i.name, unit: i.unit }));
            setIngredientsOptions(ing);
        });
    }, []);

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
        const dateFormatted = date.format("YYYY-MM-DD") + 'T' + hour.format('HH:mm:ss');

        const data = {
            email,
            eventDate: dateFormatted,
            idKindEvent: 2,
            ingredients,
            freeNote: notes
        };

        if (router.query.id) {
            editFoodEvent({ ...data, idEvent: router.query.id }).then((res) => {
                console.log(res.data.insulinToCorrect);
                const queryParams = new URLSearchParams();
                queryParams.set('chConsumed', res.data.chConsumed);
                queryParams.set('insulineToCorrect', res.data.insulinToCorrect);
                router.push({
                    pathname: "/event/food/final",
                    query: Object.fromEntries(queryParams.entries())
                });
            });
        } else {
            addFoodEvent(data).then((res) => {
                console.log(res.data.insulinToCorrect);
                const queryParams = new URLSearchParams();
                queryParams.set('chConsumed', res.data.chConsumed);
                queryParams.set('insulineToCorrect', res.data.insulinToCorrect);
                router.push({
                    pathname: "/event/food/final",
                    query: Object.fromEntries(queryParams.entries())
                });
            });
        }
    };

    return (
        <div className="bg-white rounded-xl w-10/12 flex flex-wrap text-gray-primary py-20 px-44 my-12 justify-start gap-x-20 gap-y-12">
            <CustomDatePicker
                label="Ingresá una fecha"
                date={dayjs(date)}
                onChange={(e) => setDate(e)}
                width="w-1/3"
                defaultDate={date && date}
            />
            <CustomTimePicker
                label="Elija el horario de la comida"
                hour={dayjs(hour)}
                onChange={setHour}
                width="w-1/3"
                defaultHour={hour && hour}
            />
            <TextArea
                placeholder="Describí tus sensaciones, estado de ánimo y cualquier otro síntoma que pueda ser de ayuda para los profesionales"
                label="¿Cómo te sentís?"
                id="notes"
                width="w-full"
                defaultValue={existingData ? existingData.freeNote : ''}
            />
            <div className="flex flex-col w-full ">
                {Array.from({ length: ingredientsQuantity }).map((_, index) => (
                    <div key={index} className="flex justify-start flex-wrap my-4 gap-20 items-end">
                        <SelectSearch
                            label="Ingrediente"
                            placeholder="Elegí un ingrediente"
                            options={ingredientsOptions}
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
                        <div className="w-1/3 flex items-end">
                            <InputWithLabel
                                label="Cantidad del ingrediente"
                                placeholder="Escribí la cantidad"
                                id={`IngredientsMeasurement_${index}`}
                                width="w-3/4"
                                value={ingredients[index].quantity}
                                onChange={(e) => handleQuantityChange(e, index)}
                            />
                            <div className="ml-4 w-1/4">{ingredients[index].unit}</div>
                        </div>
                        <button onClick={() => handleRemoveIngredient(index)}>
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
    );
};

export default FoodEventForm;