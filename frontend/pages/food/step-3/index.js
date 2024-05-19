import { useEffect, useState } from "react";
import { Section } from "@/components/section";
import { Select } from "@/components/selector";
import { TYPE_PORTIONS } from "../../../constants";
import { InputWithLabel } from "@/components/input";
import { Checkbox, FormControlLabel } from "@mui/material";
import {ButtonOrange} from "@/components/button";

const StepThree = () => {
    const [images, setImages] = useState([]);
    const [isOpenStates, setIsOpenStates] = useState({});
    const [selectedOptions, setSelectedOptions] = useState({});
    const [checkboxStates, setCheckboxStates] = useState({});

    useEffect(() => {
        const storedImages = sessionStorage.getItem('imagesBase64');
        if (storedImages) {
            try {
                const parsedImages = JSON.parse(storedImages);
                if (Array.isArray(parsedImages)) {
                    setImages(parsedImages);
                } else {
                    setImages([]);
                }
            } catch (error) {
                console.error("Error parsing stored images:", error);
                setImages([]);
            }
        } else {
            setImages([]);
        }
    }, []);

    const handleOptionClick = (imageId) => (option) => {
        setSelectedOptions({
            ...selectedOptions,
            [imageId]: option,
        });
        setIsOpenStates({
            ...isOpenStates,
            [imageId]: false,
        });
    };

    const handleCheckboxChange = (imageId) => (event) => {
        setCheckboxStates({
            ...checkboxStates,
            [imageId]: event.target.checked,
        });
    };

    const toggleIsOpen = (imageId) => {
        setIsOpenStates({
            ...isOpenStates,
            [imageId]: !isOpenStates[imageId],
        });
    };

    const handleSubmit = () => {
        console.log("Datos del formulario:", selectedOptions, checkboxStates, images);
    };

    return (
        <Section>
            <div className="container">
                <div className="bg-white rounded-xl w-full flex flex-col flex-wrap text-gray-primary py-20 px-44 my-12 justify-around gap-x-2 gap-y-12">
                    <h4 className="font-semibold text-xl text-center">Seleccione la cantidad consumida del producto</h4>

                    {images.map((image) => (
                        <div key={image.id} className="flex w-full gap-12">
                            <div className="w-56 h-56 flex overflow-hidden rounded-xl">
                                <img
                                    src={`data:image/png;base64,${image.imageBase64}`}
                                    className="w-full object-cover"
                                    alt="Etiqueta"
                                />
                            </div>
                            <div className="flex flex-col w-full gap-4">
                                <InputWithLabel
                                    type="text"
                                    placeholder="Nombre"
                                    label="Nombre de producto"
                                    width="w-2/5"
                                />
                                <Select
                                    setIsOpen={() => toggleIsOpen(image.id)}
                                    handleOptionClick={handleOptionClick(image.id)}
                                    isOpen={isOpenStates[image.id] || false}
                                    selectedOption={selectedOptions[image.id]}
                                    label="Porción consumida"
                                    placeholder="Seleccioná una porción"
                                    options={TYPE_PORTIONS}
                                    width="w-2/5"
                                />
                                <div>
                                    <FormControlLabel
                                        control={
                                            <Checkbox
                                                checked={checkboxStates[image.id] || false}
                                                onChange={handleCheckboxChange(image.id)}
                                                name={`checkbox-${image.id}`}
                                                color="primary"
                                            />
                                        }
                                        label="Quiero guardar el producto para una próxima carga"
                                    />
                                </div>
                            </div>
                        </div>
                    ))}

                    <div className="flex justify-end">
                        <ButtonOrange onClick={handleSubmit} label="Enviar" width="w-1/4" />
                    </div>
                </div>
            </div>
        </Section>
    );
};

export default StepThree;
