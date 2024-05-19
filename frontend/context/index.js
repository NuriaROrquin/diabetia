import React, { createContext, useContext, useState } from "react";

const AIDataContext = createContext();

export const AIDataProvider = ({ children }) => {
    const [aiDataDetected, setAIDataDetected] = useState(null);
    const [imagesUploaded, setImagesUploaded] = useState([]);

    const updateAIData = (data) => {
        console.log("data que llega al update en context", data)
        setAIDataDetected(data);
    };

    const saveFiles = (newImage) => {
        console.log("data que llega al save files en context", newImage)
        setImagesUploaded((prevImages) => [...prevImages, newImage]);
    };

    return (
        <AIDataContext.Provider value={{ aiDataDetected, updateAIData, imagesUploaded, saveFiles }}>
            {children}
        </AIDataContext.Provider>
    );
};

export const useAIData = () => useContext(AIDataContext);