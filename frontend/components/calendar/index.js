import {CustomProvider, Badge, Popover, Whisper} from 'rsuite';

import Calendar from 'rsuite/Calendar';
import 'rsuite/Calendar/styles/index.css';
import "rsuite/dist/rsuite.min.css";
import { format, getDay, getDate, getMonth, getYear } from 'date-fns';
import es_AR from 'rsuite/locales/es_AR';


function getTodoList(date) {
    const todoLists = {
        '2024-05-10': [
            { time: '10:30 am', title: 'Reunión' },
            { time: '12:00 pm', title: 'Almuerzo' }
        ],
        '2024-05-12': [
            { time: '10:30 am', title: 'Reunión' }
        ],
        '2024-05-15': [
            { time: '09:30 pm', title: 'Productos' },
            { time: '12:30 pm', title: 'Cliente' },
            { time: '02:00 pm', title: 'Producto' },
            { time: '05:00 pm', title: 'Producto' },
            { time: '06:30 pm', title: 'Reportando' },
            { time: '10:00 pm', title: 'Irse' }
        ],
    };

    const formattedDate = date.toISOString().split('T')[0];
    return todoLists[formattedDate] || [];
}

export const CustomCalendar = () => {

    function renderCell(date) {
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
    }

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