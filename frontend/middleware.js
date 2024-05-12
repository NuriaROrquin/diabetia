import { NextResponse } from "next/server"

export function middleware(request){
    const currentUser = request.cookies.get("currentUser")?.value;

    //Si entra a una ruta protegida
    //Si no tiene usuario o la cookie expiró, borra la cookie y lo redirije al login. Sino, lo deja pasar.
    if (protectedRoutes.includes(request.nextUrl.pathname)){
        if(!currentUser || Date.now() > JSON.parse(currentUser).expiredAt){
            request.cookies.delete("currentUser");
            const response = NextResponse.redirect(new URL("/auth/login", request.url));
            response.cookies.delete("currentUser");
            return response;
        }else{
            return NextResponse.next()
        }
    }

    //Si entra a una ruta autenticada y tiene usuario, redirije al dashboard y sino, lo deja pasar
    if (authRoutes.includes(request.nextUrl.pathname)) {
        return currentUser
            ? NextResponse.redirect(new URL('/dashboard', request.url))
            : NextResponse.next();
    }

}

const protectedRoutes = ["/dashboard"];
const authRoutes = ["/auth"];