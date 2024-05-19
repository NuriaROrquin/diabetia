import {Section} from "@/components/section";
import {SubtitleSection, TitleSection} from "@/components/titles";
import {CameraAltOutlined, UploadFileOutlined} from "@mui/icons-material";
import {useRef} from "react";
import {useRouter} from "next/router";
import { v4 as uuidv4 } from 'uuid';

const FoodPage = () => {
    const fileInputRef = useRef(null);
    const router = useRouter();

    const handleCameraClick = () => {
        if (navigator.mediaDevices && navigator.mediaDevices.getUserMedia) {
            navigator.mediaDevices.getUserMedia({ video: true })
                .then(stream => {
                    // Lógica para manejar la cámara
                    console.log('Camera opened', stream);
                    // Puedes agregar una vista previa de la cámara aquí o redirigir a otra parte
                })
                .catch(err => {
                    console.error('Error opening camera', err);
                });
        } else {
            alert('Tu dispositivo no soporta la apertura de la cámara');
        }
    };

    const handleUploadClick = () => {
        fileInputRef.current.click();
    };

    const handleFileChange = (event) => {
        const file = event.target.files[0];
        if (file) {
            const reader = new FileReader();
            reader.onloadend = () => {
                const base64String = reader.result.replace('data:', '').replace(/^.+,/, '');
                const images = [{ id: uuidv4(), imageBase64: base64String }];
                sessionStorage.setItem('imagesBase64', JSON.stringify(images));
                router.push("/food/step-1")
            };
            reader.readAsDataURL(file);
        }
    };

    return(
        <Section className="bg-food bg-cover">
            <div className="bg-blue-primary w-full h-screen bg-opacity-70 flex flex-col items-center gap-12">
                <div>
                    <TitleSection className="text-white pt-20 mb-6">¿Qué estás comiendo?</TitleSection>
                    <SubtitleSection className="text-white">Subí tu imagen</SubtitleSection>
                </div>

                <div className="bg-white w-1/5 flex flex-col rounded-xl p-6 justify-center items-center"
                     onClick={handleCameraClick}>
                    <CameraAltOutlined className="text-orange-primary h-20 w-20"/>
                    <span className="text-lg text-gray-primary font-semibold">Hacé click para abrir la cámara</span>
                    <span className="text-lg text-gray-primary">de tu dispositivo</span>
                </div>

                <div className="bg-white w-1/5 flex flex-col rounded-xl p-6 justify-center items-center"
                     onClick={handleUploadClick}>
                    <UploadFileOutlined className="text-orange-primary h-20 w-20"/>
                    <span className="text-lg text-gray-primary font-semibold">Hacé click para subir un archivo</span>
                    <span className="text-lg text-gray-primary">desde tu dispositivo</span>
                </div>

                <input
                    type="file"
                    ref={fileInputRef}
                    style={{display: 'none'}}
                    onChange={handleFileChange}
                />
            </div>
        </Section>
    )
}

export default FoodPage;