import {CircleRounded} from "@mui/icons-material";
import {formatDateTime} from "../../helpers";
import {OrangeLink} from "../link";

export const Timeline = ({events, className = "", limit = events.length}) => {

    if(events.length == 0){
        return (
        <div data-testid="timeline" className="w-10/12 mx-auto lg:w-full flex flex-col justify-center items-center h-full py-40">
            <div className="bg-white shadow-2xl flex flex-col p-12 rounded-2xl gap-4 items-center">
                <div className="text-gray-primary text-2xl">No se encontraron registros</div>
                <div className="text-gray-primary text-xl">Para que comiences a ver registros, te sugerimos cargar un evento</div>
                <div className="col-start-3 justify-self-end">
                    <OrangeLink href="/event" label="Registrar Evento" width="w-1/10"/>
                </div>
            </div>
        </div>
        )
    }

    return (
        <div className="flex justify-center mb-10">
            <div className={`flex flex-col self-start ${className}`}>
                {events && events.slice(0, limit).map((event, index) => {
                    return (
                    <div key={index} className="flex container gap-5">
                        <div className="flex items-center min-w-28">
                            <span className="text-gray-primary font-medium text-base flex">{formatDateTime(event.dateTime)}</span>
                        </div>
                        <div className="relative flex items-center min-h-20">
                            <div className={` ${event.isWarning ? "bg-red-primary" : "bg-orange-primary"} h-8 w-8 rounded-full flex justify-center items-center z-10`}>
                                <CircleRounded fontSize='small' className="circleRoundedTimeline"/>
                            </div>
                            <div className="h-full w-1 bg-gray-300 absolute left-3.5"></div>
                        </div>
                        <div className="flex items-center min-w-40">
                            <span className="text-gray-primary font-medium text-base">{event.title}</span>
                        </div>
                    </div>
                    )}
                )}
            </div>
        </div>
    )
}