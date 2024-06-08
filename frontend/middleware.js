import { NextResponse } from "next/server"
import {cookies} from "next/headers";

export function middleware(req){

    const cookieUserLogged = cookies().get("jwt");
    const userLogged = cookieUserLogged?.value;

    const cookieInformationCompleted = cookies().get("informationCompleted");
    const informationCompleted = cookieInformationCompleted?.value;

    if (authRoutes.some(route => req.nextUrl.pathname.startsWith(route))) {
        return userLogged
            ? NextResponse.redirect(new URL('/dashboard', req.url))
            : NextResponse.next();
    }


    if (protectedRoutes.includes(req.nextUrl.pathname)) {
        if (!userLogged) {
            req.cookies.delete("currentUser");
            const response = NextResponse.redirect(new URL("/auth/login", req.url));
            response.cookies.delete("currentUser");
            return response;
        } else {
            if (JSON && JSON.parse(informationCompleted.toLowerCase()) === false) {
                return NextResponse.redirect(new URL("/initialForm", req.url));
            }
            return NextResponse.next()
        }
    }

}

const protectedRoutes = ["/dashboard", "/food", "/event", "/calendar", "/reports", "/profile"];
const authRoutes = ["/auth"];