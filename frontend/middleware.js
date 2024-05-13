import { NextResponse } from "next/server"
import {cookies} from "next/headers";

export function middleware(req){

    const cookie = cookies().get("jwt");
    const currentUser = cookie?.value;

    //Si entra a una ruta autenticada y tiene usuario, redirije al dashboard y sino, lo deja pasar
    if (authRoutes.some(route => req.nextUrl.pathname.startsWith(route))) {
        return currentUser
            ? NextResponse.redirect(new URL('/dashboard', req.url))
            : NextResponse.next();
    }

    //Si entra a una ruta protegida
    //Si no tiene usuario o la cookie expiró, borra la cookie y lo redirije al login. Sino, lo deja pasar.
    if (protectedRoutes.includes(req.nextUrl.pathname)) {
        if (!currentUser) {
            req.cookies.delete("currentUser");
            const response = NextResponse.redirect(new URL("/auth/login", req.url));
            response.cookies.delete("currentUser");
            return response;
        } else {
            return NextResponse.next()
        }
    }



}

const protectedRoutes = ["/dashboard"];
const authRoutes = ["/auth"];