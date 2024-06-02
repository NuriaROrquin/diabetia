import Link from "next/link";
import Image from "next/image";
import {ErrorOutline, HelpOutline} from "@mui/icons-material";
import CustomTooltip from "@/components/tooltip";

export const MetricCard = ({number, textIndicator, title, description, unit, tooltipContent, selectedOption, loading, isWarning}) => {

    const getTextColor = () => {
        return isWarning === null ? 'text-blue-primary' : isWarning === false ? 'text-green-primary' : 'text-red-primary'
    }

    return (

        <div className="min-w-80 w-full sm:w-1/3 lg:w-1/4 bg-white p-8 rounded-2xl shadow relative">

            <CustomTooltip title={`${tooltipContent}`}  placement="top" arrow>
                <HelpOutline className="text-orange-primary absolute top-4 right-4"/>
            </CustomTooltip>

            {isWarning && <span className="flex absolute h-6 w-6 top-0 left-0 mt-2 ml-2">
                <ErrorOutline className="animate-ping font-bold text-6xl text-red-primary">!</ErrorOutline>
            </span>}

            {!loading &&
                <div className="w-full flex justify-center flex-col items-center mb-4">
                    <div className="flex items-end">
                        <h3 className={`${getTextColor()} text-6xl font-bold`}>{number}</h3>
                        {unit && <span className={`${getTextColor()} font-bold`}>{unit}</span>}
                    </div>
                    <span className={`font-semibold ${getTextColor()}`}>{textIndicator}</span>
                </div>
            }
            {loading &&
                <div className="w-full flex justify-center items-center mb-5">
                    <svg aria-hidden="true"
                         className="inline w-10 h-10 text-blue-secondary animate-spin dark:text-blue-secondary fill-white"
                         viewBox="0 0 100 101" fill="none" xmlns="http://www.w3.org/2000/svg">
                        <path
                            d="M100 50.5908C100 78.2051 77.6142 100.591 50 100.591C22.3858 100.591 0 78.2051 0 50.5908C0 22.9766 22.3858 0.59082 50 0.59082C77.6142 0.59082 100 22.9766 100 50.5908ZM9.08144 50.5908C9.08144 73.1895 27.4013 91.5094 50 91.5094C72.5987 91.5094 90.9186 73.1895 90.9186 50.5908C90.9186 27.9921 72.5987 9.67226 50 9.67226C27.4013 9.67226 9.08144 27.9921 9.08144 50.5908Z"
                            fill="currentColor"/>
                        <path
                            d="M93.9676 39.0409C96.393 38.4038 97.8624 35.9116 97.0079 33.5539C95.2932 28.8227 92.871 24.3692 89.8167 20.348C85.8452 15.1192 80.8826 10.7238 75.2124 7.41289C69.5422 4.10194 63.2754 1.94025 56.7698 1.05124C51.7666 0.367541 46.6976 0.446843 41.7345 1.27873C39.2613 1.69328 37.813 4.19778 38.4501 6.62326C39.0873 9.04874 41.5694 10.4717 44.0505 10.1071C47.8511 9.54855 51.7191 9.52689 55.5402 10.0491C60.8642 10.7766 65.9928 12.5457 70.6331 15.2552C75.2735 17.9648 79.3347 21.5619 82.5849 25.841C84.9175 28.9121 86.7997 32.2913 88.1811 35.8758C89.083 38.2158 91.5421 39.6781 93.9676 39.0409Z"
                            fill="currentFill"/>
                    </svg>
                </div>
            }
            <div className="w-full flex justify-center flex-col items-center gap-2">
                <span className="font-semibold text-gray-primary">{title}</span>
                <span className="text-gray-secondary text-center">{description}</span>
            </div>
        </div>

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