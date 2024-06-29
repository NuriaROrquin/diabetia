import React, {useEffect, useRef, useState} from "react";
import {Section} from "@/components/section";
import {ButtonGreen, ButtonOrange} from "@/components/button";
import {v4 as uuidv4} from "uuid";
import {useRouter} from "next/router";
import {useAIDataTag} from "../../../../context/useAIDataTag";
import {useAIDataDishes} from "../../../../context/useAIDataDishes";
import {InputWithLabel} from "@/components/input";
import {Select, SelectSearch} from "@/components/selector";
import {TYPE_PORTIONS} from "../../../../constants";
import {Checkbox, FormControlLabel} from "@mui/material";
import {OrangeLink} from "@/components/link";
import {tagDetection} from "../../../../services/tag.service";
import {AddCircle, Delete} from "@mui/icons-material";
import {SubtitleSection, TitleSection} from "@/components/titles";
import {confirmIngredients} from "../../../../services/dishes.service";

const StepTwo = () => {
    const { resultsDetectionIngredients } = useAIDataDishes();
    const [selectedValues, setSelectedValues] = useState({});
    const [openSelectIndex, setOpenSelectIndex] = useState(null);

    const router = useRouter();

    const ingredients = {
        "imageId": 1698750,
        "segmentationResults": [
            {
                "foodItemPosition": 1,
                "recognitionResults": [
                    {
                        "id": 1480,
                        "name": "escalope vienés",
                        "prob": 0.5470137
                    },
                    {
                        "id": 1463,
                        "name": "bratwurst",
                        "prob": 0.13065034
                    },
                    {
                        "id": 147,
                        "name": "hamburguesa",
                        "prob": 0.10794803
                    },
                    {
                        "id": 57,
                        "name": "empanadilla frita",
                        "prob": 0.10736177
                    },
                    {
                        "id": 232,
                        "name": "pollo frito",
                        "prob": 0.10702619
                    }
                ]
            },
            {
                "foodItemPosition": 2,
                "recognitionResults": [
                    {
                        "id": 1480,
                        "name": "escalope vienés",
                        "prob": 0.4107467
                    },
                    {
                        "id": 306,
                        "name": "patatas fritas",
                        "prob": 0.18675356
                    },
                    {
                        "id": 394,
                        "name": "poutine",
                        "prob": 0.15721259
                    },
                    {
                        "id": 147,
                        "name": "hamburguesa",
                        "prob": 0.12497035
                    },
                    {
                        "id": 873,
                        "name": "chipirones a la andaluza",
                        "prob": 0.120316796
                    }
                ]
            },
            {
                "foodItemPosition": 3,
                "recognitionResults": [
                    {
                        "id": 1251,
                        "name": "limón",
                        "prob": 0.31338543
                    },
                    {
                        "id": 1250,
                        "name": "lima",
                        "prob": 0.19892104
                    },
                    {
                        "id": 1188,
                        "name": "naranja",
                        "prob": 0.16826895
                    },
                    {
                        "id": 1203,
                        "name": "pomelo",
                        "prob": 0.16115959
                    },
                    {
                        "id": 1187,
                        "name": "mango",
                        "prob": 0.15826498
                    }
                ]
            }
        ]
    }

    const mapNameToTitle = (ingredients) => {
        return {
            ...ingredients,
            segmentationResults: ingredients.segmentationResults.map(segmentationResult => ({
                ...segmentationResult,
                recognitionResults: segmentationResult.recognitionResults.map(recognitionResult => ({
                    id: recognitionResult.id,
                    title: recognitionResult.name,
                    prob: recognitionResult.prob
                }))
            }))
        };
    };

    const handleSelectChange = (index, selectedOption) => {
        setSelectedValues(prevState => ({
            ...prevState,
            [index]: selectedOption
        }));
        setOpenSelectIndex(null);
    };

    const toggleSelect = (index) => {
        setOpenSelectIndex(prevIndex => (prevIndex === index ? null : index));
    };

    const newIngredients = mapNameToTitle(ingredients);

    const handleConfirmIngredients = () => {
        const formattedIngredients = {
            ImageId: newIngredients.imageId,
            SegmentationResults: newIngredients.segmentationResults.map((segment) => ({
                FoodItemPosition: segment.foodItemPosition,
                RecognitionResults: [
                    {
                        id: selectedValues[segment.foodItemPosition - 1]?.id || 0,
                        name: selectedValues[segment.foodItemPosition - 1]?.title || '',
                        prob: selectedValues[segment.foodItemPosition - 1]?.prob || 0,
                    }
                ]
            })),
        };

        confirmIngredients(formattedIngredients)
            .then((res) => {
                saveIngredientsDetected(res.data);
                router.push("/food/foodDishes/step-2");
            })
            .catch((error) => {
                console.log(error);
                error.response?.data ? setError(error.response.data) : setError("Hubo un error");
            });
        console.log("formattedIngredients", formattedIngredients);
    };

    return(
        <Section>
            <div className="container">
                <div
                    className="bg-white rounded-xl w-full flex flex-col flex-wrap text-gray-primary py-20 px-44 my-12 justify-around gap-x-2 gap-y-12">
                    <TitleSection>¿Estos son tus ingredientes?</TitleSection>
                    <SubtitleSection>Estos son los ingredientes que detectó la Inteligencia Artificial</SubtitleSection>

                    {newIngredients.segmentationResults.map((ingredient, index) => (
                        <div key={ingredient.foodItemPosition}>
                            <Select
                                width="w-full"
                                selectedOption={selectedValues[index] || null}
                                options={ingredient.recognitionResults}
                                isOpen={openSelectIndex === index}
                                setIsOpen={() => toggleSelect(index)}
                                handleOptionClick={(option) => handleSelectChange(index, option)}
                                placeholder="Selecciona un ingrediente"
                                label={`Ingrediente ${ingredient.foodItemPosition}`}
                                index={index}
                            />
                        </div>
                    ))}

                    <div className="flex justify-between">
                        <OrangeLink href="/event/food" label="Quiero cargarlo manualmente" width="w-1/4"
                                    background="!bg-gray-400"/>

                        <ButtonOrange onClick={handleConfirmIngredients} label="Confirmar" width="w-1/4"/>
                    </div>
                </div>
            </div>
        </Section>
    )
}

export default StepTwo;