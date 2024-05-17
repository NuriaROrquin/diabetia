import {CircleRounded} from "@mui/icons-material";

export const Timeline = ({events}) => {
    return (
        <div className="flex flex-col self-start">
            {events && events.map((event, index) => (
                <div key={index} className="flex container gap-5">
                    <div className="flex items-center min-w-12">
                        <span className="text-gray-primary font-medium text-base">{event.time}</span>
                    </div>
                    <div className="relative flex items-center min-h-20">
                        <div className="bg-orange-primary h-8 w-8 rounded-full flex justify-center items-center z-10">
                            <CircleRounded fontSize='small' className="circleRoundedTimeline"/>
                        </div>
                        <div className="h-full w-1 bg-gray-300 absolute left-3.5"></div>
                    </div>
                    <div className="flex items-center">
                        <span className="text-gray-primary font-medium text-base">{event.title}</span>
                    </div>
                </div>
                )
            )}
        </div>
    )
}