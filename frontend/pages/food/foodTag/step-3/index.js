import { useEffect, useState } from "react";
import { Section } from "@/components/section";
import { Select } from "@/components/selector";
import { TYPE_PORTIONS } from "../../../../constants";
import { InputWithLabel } from "@/components/input";
import {OrangeLink} from "@/components/link";
import { Checkbox, FormControlLabel } from "@mui/material";
import {ButtonOrange} from "@/components/button";
import {useAIDataTag} from "../../../../context/useAIDataTag";
import {useRouter} from "next/router";
import {tagDetection} from "../../../../services/tag.service";

const StepThree = () => {
    const { updateAIDataDetected, imagesUploaded } = useAIDataTag();
    const router = useRouter();

    const [images, setImages] = useState([]);
    const [isOpenStates, setIsOpenStates] = useState({});
    const [selectedOptions, setSelectedOptions] = useState({});
    const [checkboxStates, setCheckboxStates] = useState({});
    const [error, setError] = useState(false)

    useEffect(() => {
        setImages(imagesUploaded || []);
    }, [imagesUploaded]);

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
        const tagsArray = images.map((image) => ({
            id: image.id,
            portion: selectedOptions[image.id]?.quantity,
            title: document.getElementById(image.id).value,
            //savePreference: checkboxStates[image.id] || false,
            imageBase64: image.imageBase64,
        }));

        tagDetection(tagsArray).then((res) => {
            updateAIDataDetected(res.data);
            router.push("/food/step-4");
        })
        .catch((error) => {
            error.response.data ? setError(error.response.data) : setError("Hubo un error")
        });
    };

    return (
        <Section>
            <div className="container">
                <div className="bg-white rounded-xl w-full flex flex-col flex-wrap text-gray-primary py-20 px-44 my-12 justify-around gap-x-2 gap-y-12">
                    <h4 className="font-semibold text-3xl text-center">Seleccione la cantidad consumida de cada producto</h4>

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
                                    id={image.id}
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
                    <div className="flex justify-between">
                        <OrangeLink href="/food/step-1" label="Atrás" width="w-1/4" background="bg-gray-400 hover:bg-gray-600"/>

                        {error && <span className="text-red-500 mb-3">{error}</span>}
                        <ButtonOrange onClick={handleSubmit} label="Enviar" width="w-1/4" />
                    </div>
                </div>
            </div>
        </Section>
    );
};

export default StepThree;
