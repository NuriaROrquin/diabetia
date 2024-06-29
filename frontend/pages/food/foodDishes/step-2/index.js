import React, { useState, useEffect } from "react";
import { Section } from "@/components/section";
import { ButtonOrange } from "@/components/button";
import { useRouter } from "next/router";
import { useAIDataDishes } from "../../../../context/useAIDataDishes";
import { Select } from "@/components/selector";
import { OrangeLink } from "@/components/link";
import { confirmIngredients } from "../../../../services/dishes.service";
import { SubtitleSection, TitleSection } from "@/components/titles";

const StepTwo = () => {
    const { resultsDetectionIngredients, saveIngredientsData } = useAIDataDishes();
    const [selectedValues, setSelectedValues] = useState({});
    const [openSelectIndex, setOpenSelectIndex] = useState(null);
    const router = useRouter();

    const mapNameToTitle = (ingredients) => {
        if (!ingredients || !ingredients.segmentationResults) {
            return { imageId: null, segmentationResults: [] };
        }

        return {
            imageId: ingredients.imageId,
            segmentationResults: ingredients.segmentationResults.map(segmentationResult => ({
                foodItemPosition: segmentationResult.foodItemPosition,
                recognitionResults: segmentationResult.recognitionResults.map(recognitionResult => ({
                    id: recognitionResult.id,
                    name: recognitionResult.name,
                    prob: recognitionResult.prob
                }))
            }))
        };
    };

    const newIngredients = mapNameToTitle(resultsDetectionIngredients);

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

    const handleConfirmIngredients = () => {
        confirmIngredients(newIngredients)
            .then((res) => {
                saveIngredientsData(res.data);
                router.push("/food/foodDishes/step-3");
            })
            .catch((error) => {
                console.log(error);
                error.response?.data ? setError(error.response.data.Message) : setError("Hubo un error");
            });
    };

    return (
        <Section>
            <div className="container">
                <div
                    className="bg-white rounded-xl w-full flex flex-col flex-wrap text-gray-primary py-20 px-44 my-12 justify-around gap-x-2 gap-y-12">
                    <TitleSection>¿Estos son tus ingredientes?</TitleSection>
                    <SubtitleSection>Estos son los ingredientes que detectó la Inteligencia Artificial</SubtitleSection>

                    {
                        newIngredients.segmentationResults.map((ingredient, index) => (
                            <div key={ingredient.foodItemPosition}>
                                <Select
                                    width="w-full"
                                    selectedOption={selectedValues[index] || null}
                                    options={ingredient.recognitionResults.map(result => ({
                                        id: result.id,
                                        title: result.name,
                                        prob: result.prob
                                    }))}
                                    isOpen={openSelectIndex === index}
                                    setIsOpen={() => toggleSelect(index)}
                                    handleOptionClick={(option) => handleSelectChange(index, option)}
                                    placeholder="Selecciona un ingrediente"
                                    label={`Ingrediente ${ingredient.foodItemPosition}`}
                                    index={index}
                                />
                            </div>
                        ))
                    }

                    <div className="flex justify-between">
                        <OrangeLink href="/event/food" label="Quiero cargarlo manualmente" width="w-1/4"
                                    background="!bg-gray-400"/>

                        <ButtonOrange onClick={handleConfirmIngredients} label="Confirmar" width="w-1/4"/>
                    </div>
                </div>
            </div>
        </Section>
    );
}

export default StepTwo;
