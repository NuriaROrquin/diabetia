import {useState} from "react";
import {Section} from "@/components/section";
import {ButtonGreen} from "@/components/button";
import {useRouter} from "next/router";
import {useAIDataDishes} from "../../../../context/useAIDataDishes";
import {foodDetection} from "../../../../services/dishes.service";

const StepOne = () => {
    const { saveIngredientsDetected, imagesUploaded } = useAIDataDishes();

    const [images, setImages] = useState(imagesUploaded);
    const router = useRouter();

    const handleUploadClick = () => {

        const data = {
            imageBase64: images[0] && images[0].imageBase64,
        }

        /*foodDetection(data)
            .then((res) => {
                saveIngredientsDetected(res.data);
                router.push("/food/foodDishes/step-2");
            })
            .catch((error) => {
                console.log(error);
                error.response?.data ? setError(error.response.data) : setError("Hubo un error");
            });*/

        router.push("/food/foodDishes/step-2");
    };

    return(
        <Section>
        <div className="container">
            <div
                className="bg-white rounded-xl w-full flex flex-col flex-wrap text-gray-primary py-20 px-44 my-12 justify-around gap-x-2 gap-y-12">

                <h4 className="font-semibold text-xl text-center">¿Tu imagen se ve bien?</h4>

                <div className="flex flex-col w-full gap-12">
                    <div className="flex gap-12 items-center justify-center">
                    {
                        images.map((image) => {
                            return (
                                <div key={image.id} className="w-96 h-96 flex overflow-hidden rounded-xl">
                                    <img src={`data:image/png;base64, ${image.imageBase64}`}
                                         className="w-full object-cover"
                                         alt="Red dot"/>
                                </div>
                            )
                        })
                    }
                    </div>

                    <div className="w-full flex justify-around">
                        <ButtonGreen onClick={() => router.push("/food/foodDishes")} label="Rehacer" width="w-1/3" background="!bg-red-primary hover:bg-gray-600"/>
                        <ButtonGreen onClick={handleUploadClick} label="Confirmar" width="w-1/3"/>
                    </div>

                </div>


            </div>
        </div>
        </Section>
    )
}

export default StepOne;