import {useEffect, useRef, useState} from "react";
import {Section} from "@/components/section";
import {Select} from "@/components/selector";
import {TYPE_PORTIONS} from "../../../constants";
import {InputWithLabel} from "@/components/input";
import {Checkbox, FormControlLabel} from "@mui/material";
import {ButtonGreen, ButtonOrange, ButtonRed} from "@/components/button";
import {v4 as uuidv4} from "uuid";
import {useRouter} from "next/router";

const StepOne = () => {
    const [images, setImages] = useState([]);
    const fileInputRef = useRef(null);
    const router = useRouter();

    useEffect(() => {
        const storedImages = sessionStorage.getItem('imagesBase64');
        const images = storedImages ? JSON.parse(storedImages) : [];
        setImages(images);
    }, []);

    const handleUploadClick = () => {
        fileInputRef.current.click();
    };

    const handleFileChange = (event) => {
        const file = event.target.files[0];
        if (file) {
            const reader = new FileReader();
            reader.onloadend = () => {
                const base64String = reader.result.replace('data:', '').replace(/^.+,/, '');
                const newImage = { id: uuidv4(), imageBase64: base64String };
                const updatedImages = [...images, newImage];
                sessionStorage.setItem('imagesBase64', JSON.stringify(updatedImages));
                setImages(updatedImages);
                router.push("/food/step-1");
            };
            reader.readAsDataURL(file);
        }
    };

    return(
        <Section>
        <div className="container">
            <div
                className="bg-white rounded-xl w-full flex flex-col flex-wrap text-gray-primary py-20 px-44 my-12 justify-around gap-x-2 gap-y-12">

                <h4 className="font-semibold text-xl text-center">¿Querés agregar una etiqueta?</h4>

                <div className="flex flex-col w-full gap-12">
                    <div className="flex gap-12 items-center justify-center">
                    {
                        images.map((image) => {
                            return (
                                <div key={image.id} className="w-56 h-56 flex overflow-hidden rounded-xl">
                                    <img src={`data:image/png;base64, ${image.imageBase64}`}
                                         className="w-full object-cover"
                                         alt="Red dot"/>
                                </div>
                            )
                        })
                    }
                    </div>

                    <input
                        type="file"
                        ref={fileInputRef}
                        style={{display: 'none'}}
                        onChange={handleFileChange}
                    />

                    <div className="w-full flex justify-around">
                        <ButtonGreen onClick={handleUploadClick} label="Si" width="w-1/3"/>
                        <ButtonOrange onClick={() => router.push("/food/step-3")} label="No, continuar" width="w-1/3"/>
                    </div>

                </div>


            </div>
        </div>
        </Section>
    )
}

export default StepOne;