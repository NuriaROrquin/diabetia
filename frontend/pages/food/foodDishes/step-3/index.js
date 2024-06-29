import { useEffect, useState } from "react";
import { Section } from "@/components/section";
import { Input } from "@/components/input";
import { ButtonOrange } from "@/components/button";
import { useRouter } from "next/router";
import { OrangeLink } from "@/components/link";
import dayjs from "dayjs";
import utc from 'dayjs/plugin/utc';
import timezone from 'dayjs/plugin/timezone';
import { useAIDataDishes } from "../../../../context/useAIDataDishes";
import {confirmIngredients, confirmQuantity} from "../../../../services/dishes.service";

dayjs.extend(utc);
dayjs.extend(timezone);

const StepThree = () => {
    const { ingredientsData, saveFinalResult } = useAIDataDishes();
    const [inputData, setInputData] = useState({});

    const router = useRouter();

    useEffect(() => {
        if (ingredientsData && ingredientsData.ingredients) {
            const initialData = ingredientsData.ingredients.reduce((acc, ingredient) => {
                acc[ingredient.foodItemPosition] = {
                    chPerPortion: ingredient.carbohydratesPerPortion,
                    portion: 1,
                };
                return acc;
            }, {});
            setInputData(initialData);
        }
    }, [ingredientsData]);

    const handleInputChange = (foodItemPosition, field, value) => {
        setInputData((prevData) => ({
            ...prevData,
            [foodItemPosition]: {
                ...prevData[foodItemPosition],
                [field]: value,
            },
        }));
    };

    const handleSubmit = () => {
        const ingredients = Object.keys(inputData).map((key) => ({
            carbohydrates: inputData[key].chPerPortion.toString(),
            quantity: parseInt(inputData[key].portion, 10),
        }));

        const result = {
            ingredients,
        };

        confirmQuantity(result)
            .then((res) => {
                saveFinalResult(res.data);
                router.push("/food/foodDishes/step-final");
            })
            .catch((error) => {
                console.log(error);
                error.response?.data ? setError(error.response.data.Message) : setError("Hubo un error");
            });
    };

    return (
        <Section>
            <div className="container">
                <div className="bg-white rounded-xl w-full flex flex-col flex-wrap text-gray-primary py-20 px-44 my-12 justify-around gap-x-2 gap-y-12">
                    <h4 className="font-semibold text-3xl text-center">Ingredientes con valores detectados</h4>
                    {ingredientsData && ingredientsData.ingredients && ingredientsData.ingredients.map((ingredient) => (
                        <div key={ingredient.foodItemPosition} className="flex w-full gap-12">
                            <div className="flex flex-col w-full">
                                <h5>{ingredient.name[ingredient.foodItemPosition - 1]}</h5>

                                <div className="flex flex-wrap flex-row items-center gap-2">
                                    <p>Los carbohidratos por porción detectados son </p>
                                    <Input
                                        type="number"
                                        placeholder="Carbohidratos de una porción"
                                        defaultValue={ingredient.carbohydratesPerPortion}
                                        width="w-20"
                                        value={inputData[ingredient.foodItemPosition]?.chPerPortion || ''}
                                        onChange={(e) => handleInputChange(ingredient.foodItemPosition, 'chPerPortion', e.target.value)}
                                    />
                                    <p>gramos</p>
                                </div>

                                <div className="flex flex-wrap flex-row items-center gap-2">
                                    <p>Consumí </p>
                                    <Input
                                        type="number"
                                        placeholder="1"
                                        defaultValue={1}
                                        width="w-20"
                                        value={inputData[ingredient.foodItemPosition]?.portion || ''}
                                        onChange={(e) => handleInputChange(ingredient.foodItemPosition, 'portion', e.target.value)}
                                    />
                                    <p> porción</p>
                                    <span>(Porción = {ingredient.grPerPortion}gr)</span>
                                </div>
                            </div>
                        </div>
                    ))}
                    <div className="flex justify-between">
                        <OrangeLink href="/food/foodDishes/step-3" label="Atrás" width="w-1/4" background="bg-gray-400 hover:bg-gray-600" />
                        <ButtonOrange onClick={handleSubmit} label="Enviar" width="w-1/4" />
                    </div>
                </div>
            </div>
        </Section>
    );
};

export default StepThree;
