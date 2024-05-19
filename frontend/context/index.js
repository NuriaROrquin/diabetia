import React, { createContext, useContext, useState } from "react";

const AIDataContext = createContext();

export const AIDataProvider = ({ children }) => {
    const [imagesUploaded, setImagesUploaded] = useState([]);

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
        console.log("data que llega al save files en context", newImage)
        setImagesUploaded((prevImages) => [...prevImages, newImage]);
    };

    return (
        <AIDataContext.Provider value={{ updateAIDataDetected, imagesUploaded, saveFiles }}>
            {children}
        </AIDataContext.Provider>
    );
};

export const useAIData = () => useContext(AIDataContext);