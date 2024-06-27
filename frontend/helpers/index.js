import {jwtDecode} from "jwt-decode";

export function capitalizeFirstLetter(string) {
    return string.toLowerCase().replace(/(?:^|\s)\S/g, function(a) { return a.toUpperCase(); });
}

export const formatDateTime = (date) => {
    const dateEvent = new Date(date);

    const day = String(dateEvent.getDate()).padStart(2, '0');
    const month = String(dateEvent.getMonth() + 1).padStart(2, '0');
    const hours = String(dateEvent.getHours()).padStart(2, '0');
    const minutes = String(dateEvent.getMinutes()).padStart(2, '0');

    const formattedDate = `${day}/${month} ${hours}:${minutes}`;

    return formattedDate;
}

export function getEmailFromJwt() {
    const jwt = getJwt();
    let decodedData = null;
    if(jwt){
        decodedData = jwtDecode(jwt).email
    }
    return decodedData;
}

export function getInitialFormCompletedFromJwt() {
    const jwt = getJwt();
    let decodedData = null;
    if(jwt){
        decodedData = jwtDecode(jwt).initialFormCompleted
    }
    return decodedData;
}

export function getUsernameFromJwt() {
    const jwt = getJwt();
    let decodedData = null;
    if(jwt){
        decodedData = jwtDecode(jwt).username
    }
    return decodedData;
}

export function getJwt() {
    return typeof window !== "undefined" ? sessionStorage.getItem("jwt") : undefined;
}

export const calculateDateRange = (selectedOption) => {
    const currentDate = new Date();
    let dateFrom;

    switch (selectedOption) {
        case 'Últimas 24hs':
            dateFrom = new Date(currentDate.setDate(currentDate.getDate() - 1));
            break;
        case 'Últimas 48hs':
            dateFrom = new Date(currentDate.setDate(currentDate.getDate() - 2));
            break;
        case 'Última semana':
            dateFrom = new Date(currentDate.setDate(currentDate.getDate() - 7));
            break;
        case 'Último mes':
            dateFrom = new Date(currentDate.setMonth(currentDate.getMonth() - 1));
            break;
        default:
            dateFrom = currentDate;
    }

    return {
        dateFrom: dateFrom.toISOString(),
        dateTo: new Date().toISOString()
    };
};