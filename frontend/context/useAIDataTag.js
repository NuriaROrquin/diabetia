import React, {createContext, useContext, useEffect, useState} from "react";
import {useRouter} from "next/router";

const AIDataTagContext = createContext();

export const AIDataTagProvider = ({ children }) => {
    const [imagesUploaded, setImagesUploaded] = useState([]);
    const [finalCalcCarbos, setFinalCalcCarbos] = useState([]);
    const router = useRouter();

    useEffect(() => {
        if (!router.pathname.startsWith("/food")) {
            clearData();
        }
    }, [router.pathname]);
    
    const updateAIDataDetected = (data) => {
        setImagesUploaded(prevImages => {
            data.forEach(newImage => {
                const existingImageIndex = prevImages.findIndex(image => image.id === newImage.id);
                if (existingImageIndex !== -1) {
                    prevImages[existingImageIndex] = { ...prevImages[existingImageIndex], ...newImage };
                } else {
                    prevImages.push(newImage);
                }
            });
            return [...prevImages];
        });
    };

    const saveFiles = (newImage) => {
        setImagesUploaded((prevImages) => [...prevImages, newImage]);
    };

    const updateCarbohydratesConsumed = (data) => {
        setFinalCalcCarbos(data);
    };

    const clearData = () => {
        setImagesUploaded([]);
        setFinalCalcCarbos([]);
    };

    return (
        <AIDataTagContext.Provider value={{ updateAIDataDetected, imagesUploaded, saveFiles, updateCarbohydratesConsumed, finalCalcCarbos }}>
            {children}
        </AIDataTagContext.Provider>
    );
};

export const useAIDataTag = () => useContext(AIDataTagContext);