import {CustomProvider, Badge, Popover, Whisper} from 'rsuite';

import Calendar from 'rsuite/Calendar';
import 'rsuite/Calendar/styles/index.css';
import "rsuite/dist/rsuite.min.css";
import es_AR from 'rsuite/locales/es_AR';
import {useCallback} from "react";


export const CustomCalendar = ({events}) => {

    function getTodoList(date) {
        const todoLists = events;

        const formattedDate = date.toISOString().split('T')[0];
        return todoLists[formattedDate] || [];
    }

    const renderCell = useCallback((date) => {
        const list = getTodoList(date);

        const displayList = list.filter((item, index) => index < 2);

        if (list.length) {
            const moreCount = list.length - displayList.length;

            return (
                <ul className="calendar-todo-list">
                    {displayList.map((item, index) => (
                        <li key={index}>
                            <Badge color='orange' /> <b>{item.time}</b> - {item.title}
                        </li>
                    ))}
                    {moreCount ?
                        <li>
                            <Whisper
                                placement="top"
                                trigger="click"
                                speaker={
                                    <Popover>
                                        {list.map((item, index) => (
                                            <p key={index}>
                                                <b>{item.time}</b> - {item.title}
                                            </p>
                                        ))}
                                    </Popover>
                                }
                            >
                                <a>Ver todos</a>
                            </Whisper>
                        </li>
                        : null}
                </ul>
            );
        }

        return null;
    }, [events]);

    return (
        <div className="container">
            <CustomProvider locale={es_AR}>
                <Calendar
                    renderCell={(e) => renderCell(e)}
                    className="rounded bg-white block w-full text-gray-primary"
                />
            </CustomProvider>
        </div>
    )
}

export default CustomCalendar