import {jwtDecode} from "jwt-decode";

export function capitalizeFirstLetter(string) {
    return string.toLowerCase().replace(/(?:^|\s)\S/g, function(a) { return a.toUpperCase(); });
}

function setCookie(name, value, days) {
    const expires = new Date();
    expires.setTime(expires.getTime() + (days * 24 * 60 * 60 * 1000));
    const cookie = `${name}=${value};expires=${expires.toUTCString()};path=/`;
    document.cookie = cookie;
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