import React, { createContext, useContext, useEffect, useState } from "react";
import { useRouter } from "next/router";

const AIDataDishesContext = createContext();

export const AIDataDishesProvider = ({ children }) => {
    const [imagesUploaded, setImagesUploaded] = useState([]);
    const [resultsDetectionIngredients, setResultsDetectionIngredients] = useState([])
    const router = useRouter();

    useEffect(() => {
        if (!router.pathname.startsWith("/foodDishes")) {
            clearData();
        }
    }, [router.pathname]);

    const saveIngredientsDetected = (data) => {
        setResultsDetectionIngredients(data);
    };

    const saveFile = (newImage) => {
        setImagesUploaded((prevImages) => [...prevImages, newImage]);
    };

    const clearData = () => {
        setImagesUploaded([]);
    };

    return (
        <AIDataDishesContext.Provider value={{ saveIngredientsDetected, imagesUploaded, saveFile, resultsDetectionIngredients }}>
            {children}
        </AIDataDishesContext.Provider>
    );
};

export const useAIDataDishes = () => useContext(AIDataDishesContext);
