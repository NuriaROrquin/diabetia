import {Section} from "../../../components/section";
import {TitleSection} from "../../../components/titles";
import {GENDER, STEPS} from "../../../constants";
import {InputWithLabel} from "../../../components/input";
import dayjs from "dayjs";
import {ButtonOrange} from "../../../components/button";
import {CustomDatePicker} from "../../../components/pickers";
import {useRouter} from "next/router";
import {Step, StepLabel, Stepper} from "@mui/material";
import {Select} from "@/components/selector";
import {getUserInfo, firstStep} from "../../../services/user.service";
import {useEffect, useState} from "react";
import {NavLink} from "../../../components/link"
import {getEmailFromJwt} from "../../../helpers";

const ProfileFormStep1 = () => {
    const [error, setError] = useState(false);
    const [date, setDate] = useState()
    const router = useRouter()
    const [isOpen, setIsOpen] = useState(false);
    const [selectedOption, setSelectedOption] = useState(null);
    const [userInfo, setUserInfo] = useState(null);

    const email = getEmailFromJwt();

    useEffect(() => {
        email && getUserInfo({email})
            .then((res) => {
                const receivedDate = res.data.birthDate;
                console.log('receivedDate' + receivedDate)
                const formattedDate = dayjs(receivedDate, "YYYY-MM-DD");
                const genderTitle = GENDER.find(g => g.key === res.data.gender);

                setUserInfo({
                    ...res.data,
                    birthDate: formattedDate,
                    gender : genderTitle
                });

                setSelectedOption(genderTitle);
                setDate(formattedDate);

                console.log(date)
                console.log('formattedDate' + formattedDate)


            })
            .catch((error) => {
                error.response.data ? setError(error.response.data) : setError("Hubo un error")            });
    }, []);

    const handleOptionClick = (option) => {
        setSelectedOption(option);
        setIsOpen(false);
    };

    const handleSubmit = () => {
        const name = document.getElementById("name").value;
        const lastname = document.getElementById("lastname").value;
        const birthdate = date ? date.format('YYYY-MM-DD') : null;
        const weight = document.getElementById("weight").value;
        const phone = document.getElementById("phone").value;
        const gender = selectedOption.key;


        firstStep({name, birthdate, email, gender, phone, weight, lastname})
            .then((res) => {
                if(res){
                    router.push("/profile/step-1")
                }
            })
            .catch((error) => {
                error.res.data ? setError(error.res.data) : setError("Hubo un error")
            });
    }



    return(
        <Section className="pt-12">
            <div className="container items-center flex w-full justify-center flex-col">
                {/* FORMULARIO */}
                {date && (
                <div
                    className="bg-white rounded-xl w-full flex flex-wrap text-gray-primary py-20 px-44 my-12 justify-around gap-x-2 gap-y-12">
                    <div className="flex flex-col w-full gap-12">
                        <Stepper activeStep={0} alternativeLabel>
                            {STEPS.map((step, index) => (
                                <Step key={index}>

                                        <StepLabel><NavLink href={step.url} text={step.title} className="!no-underline !text-gray-secondary text-sm"/></StepLabel>

                                </Step>
                            ))}
                        </Stepper>
                        <TitleSection className="w-full !text-blue-secondary">Datos Personales</TitleSection>

                    </div>

                    <InputWithLabel label="Nombre" id="name" defaultValue={userInfo && userInfo.name} width="w-1/3"/>
                    <InputWithLabel label="Apellido" defaultValue={userInfo && userInfo.lastName} id="lastname" width="w-1/3"/>
                    <CustomDatePicker
                        label="Fecha de nacimiento"
                        value={date}
                        onChange={(newDate) => setDate(newDate)}
                        defaultDate={date}
                        width="w-1/3"
                    />
                    <InputWithLabel label="Peso" defaultValue={userInfo && userInfo.weight} id="weight" width="w-1/3"/>
                    <Select label="Genero" defaultValue={userInfo && userInfo.gender} id="gender" options={GENDER}
                            selectedOption={selectedOption}
                            handleOptionClick={handleOptionClick} setIsOpen={setIsOpen}
                            isOpen={isOpen} width="w-1/3"/>
                    <InputWithLabel label="Teléfono" defaultValue={userInfo && userInfo.phone} id="phone" width="w-1/3"/>

                    {error && <span className="text-red-500 mb-3">{error}</span>}

                    <ButtonOrange onClick={handleSubmit} label="Actualizar" width="w-1/3"/>
                </div>
                )}
            </div>
        </Section>
    )
}

export default ProfileFormStep1;