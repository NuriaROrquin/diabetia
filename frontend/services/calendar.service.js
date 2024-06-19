import axios from './axios';

export const getAllEvents = () => {
    return axios
        .get(
            `${process.env.NEXT_PUBLIC_API_URL}/Calendar/events`
        );
}

export const getEventsByDate = (date) => {
    return axios.get(
        `${process.env.NEXT_PUBLIC_API_URL}/Calendar/eventsByDate?eventDate=${date}`
    );
}