import {Section} from "@/components/section";
import {SubtitleSection, TitleSection} from "@/components/titles";
import {CameraAltOutlined, UploadFileOutlined} from "@mui/icons-material";
import {Button, Modal, Box} from "@mui/material";
import {useEffect, useRef, useState} from 'react'
import {useRouter} from "next/router";
import {v4 as uuidv4 } from 'uuid';
import {useAIData} from "../../context";

const FoodPage = () => {
    const { saveFiles } = useAIData();
    const fileInputRef = useRef(null);
    const router = useRouter();
    const videoRef = useRef(null);
    const canvasRef = useRef(null);
    const [showCameraPreview, setShowCameraPreview] = useState(false);
    const [stream, setStream] = useState(null);

    const verCamara = () => {
        navigator.mediaDevices
            .getUserMedia({
                video: { width: 640, height: 480 }
            })
            .then(stream => {
                setStream(stream);
                let video = videoRef.current;
                video.srcObject = stream;
                video.play();
            }).catch(err => {
            console.log(err);
        });
    };

    const apagarCamara = () => {
        if (stream) {
            stream.getTracks().forEach(track => track.stop());
            setStream(null);
        }
    };

    const tomarFoto = () => {
        const video = videoRef.current;
        const canvas = canvasRef.current;
        const context = canvas.getContext('2d');
        canvas.width = video.videoWidth;
        canvas.height = video.videoHeight;
        context.drawImage(video, 0, 0, canvas.width, canvas.height);
        const base64String = canvas.toDataURL('image/jpeg').replace('data:', '').replace(/^.+,/, '');
        const newImage = { id: uuidv4(), imageBase64: base64String };
        saveFiles(newImage);
        setShowCameraPreview(false);
        apagarCamara();
        router.push("/food/step-1");
    };

    const handleCameraClick = () => {
        setShowCameraPreview(true);
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
                const newImage = { id: uuidv4(), imageBase64: base64String };
                saveFiles(newImage);
                router.push("/food/step-1");
            };
            reader.readAsDataURL(file);
        }
    };

    useEffect(() => {
        if (showCameraPreview) {
            verCamara();
        } else {
            apagarCamara();
        }
    }, [showCameraPreview]);

    return(
        <Section className="">
            <div className="bg-blue-primary w-full h-screen bg-opacity-70 flex flex-col items-center gap-12">
                <div>
                    <TitleSection className="text-white pt-20 mb-6">¿Qué estás comiendo?</TitleSection>
                    <SubtitleSection className="text-white">Subí tu imagen</SubtitleSection>
                </div>

                <div className="bg-white w-1/5 flex flex-col rounded-xl p-6 justify-center items-center cursor-pointer"
                     onClick={handleCameraClick}>
                    <CameraAltOutlined className="text-orange-primary h-20 w-20"/>
                    <span className="text-lg text-gray-primary font-semibold">Hacé click para abrir la cámara</span>
                    <span className="text-lg text-gray-primary">de tu dispositivo</span>
                </div>

                <div className="bg-white w-1/5 flex flex-col rounded-xl p-6 justify-center items-center cursor-pointer"
                     onClick={handleUploadClick}>
                    <UploadFileOutlined className="text-orange-primary h-20 w-20"/>
                    <span className="text-lg text-gray-primary font-semibold">Hacé click para subir un archivo</span>
                    <span className="text-lg text-gray-primary">desde tu dispositivo</span>
                </div>

                <input
                    type="file"
                    ref={fileInputRef}
                    className="hidden"
                    onChange={handleFileChange}
                />

                <Modal
                    open={showCameraPreview}
                    onClose={() => setShowCameraPreview(false)}
                    aria-labelledby="camera-preview-modal"
                    aria-describedby="camera-preview-modal-description"
                    className="flex items-center justify-center"
                >
                    <Box className="flex flex-col items-center p-5 bg-white rounded-lg w-1/2">
                        <video ref={videoRef} className="rounded-lg"></video>
                        <Button variant="contained" color='primary' onClick={tomarFoto} className="mt-4 bg-orange-focus hover:bg-orange-primary">Tomar foto</Button>
                        <canvas ref={canvasRef} className="hidden"></canvas>
                    </Box>
                </Modal>
            </div>
        </Section>
    );
};

export default FoodPage;