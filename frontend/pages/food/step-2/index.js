import {useEffect, useState} from "react";
import {Section} from "@/components/section";
import {Select} from "@/components/selector";
import {TYPE_PORTIONS} from "../../../constants";
import {InputWithLabel} from "@/components/input";
import {Checkbox, FormControlLabel} from "@mui/material";
import {ButtonOrange} from "@/components/button";

const StepTwo = () => {

    const [image, setImage] = useState("");
    const [isOpen, setIsOpen] = useState(false);
    const [selectedOption, setSelectedOption] = useState(null);
    const [saveProduct, setSaveProduct] = useState(false);

    const handleChange = (event) => {
        setSaveProduct(event.target.checked);
    };

    useEffect(() => {
        const data = sessionStorage.getItem('imageBase64');
        if (data) {
            setImage(data);
        }
    },[]);

    const handleOptionClick = (option) => {
        setSelectedOption(option);
        setIsOpen(false);
    };

    const handleSubmit = () => {
        console.log("Datos del formulario:", selectedOption, saveProduct, image);
    }

    return(
        <Section>
        <div className="container">
            <div
                className="bg-white rounded-xl w-full flex flex-col flex-wrap text-gray-primary py-20 px-44 my-12 justify-around gap-x-2 gap-y-12">

                <h4 className="font-semibold text-xl">Seleccione la cantidad consumida del producto</h4>

                <div className="flex w-full gap-12">
                    <div className="w-56 h-56 flex overflow-hidden rounded-xl">
                        <img src={`data:image/png;base64, ${image}`} className="w-full object-cover" alt="Red dot"/>
                    </div>

                    <div className="flex flex-col w-full gap-8">
                        <InputWithLabel type="text" placeholder="Nombre" label="Nombre de producto" width="w-2/5" />
                        <Select
                            setIsOpen={setIsOpen}
                            handleOptionClick={handleOptionClick}
                            isOpen={isOpen}
                            selectedOption={selectedOption}
                            label="Porción consumida"
                            placeholder="Seleccioná una porción"
                            options={TYPE_PORTIONS}
                            width="w-2/5"
                        />
                        <div>
                            <FormControlLabel
                                control={
                                    <Checkbox
                                        checked={saveProduct}
                                        onChange={handleChange}
                                        name="exampleCheckbox"
                                        color="primary"
                                    />
                                }
                                label="Quiero guardar el producto para una próxima carga"
                            />
                        </div>
                    </div>

                </div>

                <ButtonOrange onClick={handleSubmit} label="Enviar" width="w-1/3"/>
            </div>
        </div>
        </Section>
    )
}

export default StepTwo;