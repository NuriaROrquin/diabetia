import {useEffect, useRef, useState} from "react";
import {Section} from "@/components/section";
import {Select} from "@/components/selector";
import {TYPE_PORTIONS} from "../../../constants";
import {InputWithLabel} from "@/components/input";
import {Checkbox, FormControlLabel} from "@mui/material";
import {ButtonGreen, ButtonOrange, ButtonRed} from "@/components/button";
import {v4 as uuidv4} from "uuid";
import {useRouter} from "next/router";
import {useAIData} from "../../../context";

const StepOne = () => {
    const { aiDataDetected } = useAIData();

    console.log("data detectada en step4", aiDataDetected)

    return(
        <Section>
            <div className="container">
                <div
                    className="bg-white rounded-xl w-full flex flex-col flex-wrap text-gray-primary py-20 px-44 my-12 justify-around gap-x-2 gap-y-12">
                    <h4 className="font-semibold text-xl text-center">step 4</h4>
                </div>
            </div>
        </Section>
    )
}

export default StepOne;