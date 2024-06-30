import { NextResponse } from "next/server"
import {cookies} from "next/headers";
import {jwtDecode} from "jwt-decode";

export function middleware(req){

    const jwt = cookies().get("jwt");
    const userLogged = jwt?.value;

    let stepCompleted = null;
    if(userLogged){
        stepCompleted = jwtDecode(jwt.value).stepCompleted;
    }

    if (authRoutes.some(route => req.nextUrl.pathname.startsWith(route))) {
        return userLogged
            ? NextResponse.redirect(new URL('/dashboard', req.url))
            : NextResponse.next();
    }

    if (protectedRoutes.includes(req.nextUrl.pathname)) {
        if (!userLogged) {
            req.cookies.delete("jwt");
            const response = NextResponse.redirect(new URL("/auth/login", req.url));
            response.cookies.delete("jwt");
            return response;
        } else {
            if (JSON && stepCompleted && JSON.parse(stepCompleted?.toLowerCase()) !== 4) {
                return NextResponse.redirect(new URL("/initialForm", req.url));
            }
            return NextResponse.next()
        }
    }
}

const protectedRoutes = ["/dashboard", "/foodTag", "/event", "/calendar", "/reports", "/profile"];
const authRoutes = ["/auth"];