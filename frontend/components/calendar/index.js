import {CustomProvider, Badge, Popover, Whisper} from 'rsuite';
import jwt from 'jsonwebtoken';
import Calendar from 'rsuite/Calendar';
import 'rsuite/Calendar/styles/index.css';
import "rsuite/dist/rsuite.min.css";
import { format, getDay, getDate, getMonth, getYear } from 'date-fns';
import es_AR from 'rsuite/locales/es_AR';
import {getCalendarEvents} from "../../services/api.service";
import {useCookies} from "react-cookie";




export const CustomCalendar = () => {

    const [cookies] = useCookies(['jwt']);
    const token = cookies.jwt;

    let username;
    if (token) {
        try {
            const decodedToken = jwt.decode(token);
            username = decodedToken.username;
            console.log('Username:', username);
        } catch (error) {
            console.error('Error al decodificar el token:', error.message);
        }
    } else {
        console.log('La cookie de JWT no está presente');
    }

    function getTodoList(date, email) {

        let eventos;
        getCalendarEvents(username).then((response) => {
            eventos = response;
        })

        const groupedEvents = eventos && eventos.reduce((acc, evento) => {
            const fecha = evento.fechaEvento.split('T')[0]; // Obtener solo la fecha
            if (!acc[fecha]) {
                acc[fecha] = [];
            }
            acc[fecha].push(evento);
            return acc;
        }, {});

        const todoLists = groupedEvents && Object.entries(groupedEvents).reduce((acc, [fecha, eventos]) => {
            acc[fecha] = eventos.map(evento => {
                let title;
                switch (evento.tipoEvento) {
                    case 1:
                        title = 'Reunión';
                        break;
                    case 2:
                        title = 'Almuerzo';
                        break;
                    // Agregar más casos según sea necesario para otros tipos de eventos
                    default:
                        title = 'Evento no especificado';
                }
                return { time: 'Hora no especificada', title };
            });
            return acc;
        }, {});

        console.log(todoLists)

        const formattedDate = date.toISOString().split('T')[0];
        return todoLists[formattedDate] || [];
    }

    function renderCell(date) {
        const list = getTodoList(date);
        const displayList = list && list.filter((item, index) => index < 2);

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