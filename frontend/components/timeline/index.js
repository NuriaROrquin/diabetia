import {CircleRounded} from "@mui/icons-material";
import {formatDateTime} from "../../helpers";

export const Timeline = ({events, className = "", limit = events.length}) => {
    return (
        <div className={`flex flex-col self-start ${className}`}>
            {events && events.slice(0, limit).map((event, index) => {
                return (
                <div key={index} className="flex container gap-5">
                    <div className="flex items-center min-w-28">
                        <span className="text-gray-primary font-medium text-base flex">{formatDateTime(event.dateTime)}</span>
                    </div>
                    <div className="relative flex items-center min-h-20">
                        <div className="bg-orange-primary h-8 w-8 rounded-full flex justify-center items-center z-10">
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
    )
}