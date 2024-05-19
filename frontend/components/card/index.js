import {CircleRounded} from "@mui/icons-material";
import Link from "next/link";
import Image from "next/image";
import { Tooltip } from '@mui/material';

export const MetricCard = ({number, textIndicator, title, description, unit, color="blue", tooltipContent, selectedOption}) => {

    const getTextColor = () => {
        if(color){
            return color === 'blue' ? 'text-blue-primary' : color === 'green' ? 'text-green-primary' : 'text-red-primary'
        }
    }

    const getPreposition = (option) => {
        if (option.startsWith('Último')) {
            return 'del ';
        } else if(option.startsWith('Últimas')) {
            return 'de las ';
        } else {
            return 'de la '
        }
    }

    const preposition = getPreposition(selectedOption);

    return (
        <Tooltip title={`${tooltipContent} ${preposition} ${selectedOption}`} arrow>
        <div className="min-w-80 w-full sm:w-1/3 lg:w-1/4 bg-white p-8 rounded-2xl">
            <div className="w-full flex justify-center flex-col items-center mb-4">
                <div className="flex items-end">
                    <h3 className={`${getTextColor()} text-6xl font-bold`}>{number}</h3>
                    {unit && <span className={`${getTextColor()} font-bold`}>{unit}</span>}
                </div>
                <span className={`font-semibold ${getTextColor()}`}>{textIndicator}</span>
            </div>
            <div className="w-full flex justify-center flex-col items-center gap-2">
                <span className="font-semibold text-gray-primary">{title}</span>
                <span className="text-gray-secondary text-center">{description}</span>
            </div>
        </div>
        </Tooltip>
    )
}


export const EventCard = ({events}) => {
    return (
        <>
            {events.map((event) => {
                return (
                    <div key={event.title} className="relative w-1/5 h-52 rounded-lg overflow-hidden shadow-lg transform transition-transform duration-300 hover:-translate-y-2">
                        <Link href={event.link || ""}>
                            <Image src={event.image} alt="Actividad Física" width={500} height={500}
                                 className="w-full h-full object-cover object-bottom"/>
                            <div
                                className="absolute top-0 h-full w-full p-2 bg-blue-primary bg-opacity-55 text-white text-center text-5xl font-bold flex justify-center items-center ">
                                <span>{event.title}</span>
                            </div>
                        </Link>
                    </div>
                )
            })}
        </>
    )
}