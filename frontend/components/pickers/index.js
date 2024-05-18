import {DatePicker, TimePicker} from "@mui/x-date-pickers";

export const CustomDatePicker = ({width, defaultDate, onChange, date, label}) => {
    return (
        <div className={`${width} flex items-end`}>
            <DatePicker
                label={label}
                value={date}
                onChange={onChange}
                defaultValue={defaultDate}
                format="DD-MM-YYYY"
            />
        </div>
    )
}

export const CustomTimePicker = ({width, defaultHour, onChange, hour, label}) => {
    return (
        <div className={`${width} flex items-end`}>
            <TimePicker
                label={label}
                value={hour}
                onChange={onChange}
                defaultValue={defaultHour}
            />
        </div>
    )
}