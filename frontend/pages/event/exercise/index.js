import {Section} from "../../../components/section";
import {TitleSection} from "../../../components/titles";
import {ButtonBlue, ButtonOrange} from "../../../components/button";
import {TYPE_EVENTS} from "../../../constants";
import {capitalize} from "@mui/material";
import {capitalizeFirstLetter} from "../../../helpers";
import {useState} from "react";

const ExerciseEvent = () => {
    const eventSelected = TYPE_EVENTS.filter((event) => event.id === 1)[0].title;

    return(
        <Section className="pt-12">
            <div className="container items-center flex w-full justify-center flex-col">
                <TitleSection className="text-white">¿Qué evento querés cargar?</TitleSection>
                <div className="flex w-full flex-wrap gap-y-6 gap-x-2 justify-around my-8">
                    {TYPE_EVENTS.map((event) => {
                        return(
                            <>
                                {event.title === eventSelected ?
                                    <ButtonOrange key={event.title} label={capitalizeFirstLetter(event.title)} width="w-1/5" onClick={() => onHandleClick(`${event.title}`)} />
                                    :
                                    <ButtonBlue key={event.title} label={capitalizeFirstLetter(event.title)} width="w-1/5" onClick={() => onHandleClick(`${event.title}`)} />
                                }
                            </>
                        )
                    })}
                </div>
                <div className="bg-white rounded-xl w-full flex flex-col text-gray-primary p-8 my-12">
                    <span>Card blanca</span><br/>
                    <span>Card blanca</span><br/>
                    <span>Card blanca</span><br/>
                    <span>Card blanca</span><br/>
                    <span>Card blanca</span><br/>
                    <span>Card blanca</span><br/>
                    <span>Card blanca</span><br/>
                    <span>Card blanca</span><br/>
                    <span>Card blanca</span><br/>
                </div>
            </div>
        </Section>
    )
}

export default ExerciseEvent;