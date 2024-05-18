import {Badge, Popover, Whisper} from 'rsuite';

import Calendar from 'rsuite/Calendar';
import 'rsuite/Calendar/styles/index.css';
import "rsuite/dist/rsuite.min.css";


function getTodoList(date) {
    const todoLists = {
        '2024-05-10': [
            { time: '10:30 am', title: 'Meeting' },
            { time: '12:00 pm', title: 'Lunch' }
        ],
        '2024-05-12': [
            { time: '10:30 am', title: 'Meeting' }
        ],
        '2024-05-15': [
            { time: '09:30 pm', title: 'Products ' },
            { time: '12:30 pm', title: 'Client ' },
            { time: '02:00 pm', title: 'Product ' },
            { time: '05:00 pm', title: 'Product ' },
            { time: '06:30 pm', title: 'Reporting' },
            { time: '10:00 pm', title: 'Going ' }
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
            <Calendar
                renderCell={(e) => renderCell(e)}
                className="rounded bg-white block w-full text-gray-primary"
            />
        </div>
    )
}

export default CustomCalendar