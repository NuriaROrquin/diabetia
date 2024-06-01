import Link from "next/link";
import Image from "next/image";
import {useEffect, useState} from "react";
import {NavLink} from "../link";
import {PersonOutline} from "@mui/icons-material";
import {useRouter} from "next/router";
import {useCookies} from "react-cookie";
import {logout} from "../../services/api.service";
import { Tooltip } from '@mui/material';

export const Navigation = () => {

    const [scrolling, setScrolling] = useState(false);
    const [openUserMenu, setOpenUserMenu] = useState(false);
    const [userName, setUserName] = useState('');
    const router = useRouter();
    const [_cookies, _setCookie, removeCookie] = useCookies(['cookie-name']);

    useEffect(() => {
        window.addEventListener('scroll', handleScroll);
        return () => window.removeEventListener('scroll', handleScroll);
    }, []);



    useEffect(() => {
        setUserName(_cookies.userName || '');
    }, []);

    const handleScroll = () => {
        if (window.scrollY > 20) {
            setScrolling(true);
        } else {
            setScrolling(false);
        }
    };

    const onHandleUserClick = (e) => {
        setOpenUserMenu(!openUserMenu)
    }

    const handleOnLogout = () =>{
            setOpenUserMenu(false);
            removeCookie("jwt");
            removeCookie("email")
            removeCookie("informationCompleted")
            return router.push("/auth/login")
    }

    return (
        <nav id="header" className={`fixed w-full z-30 top-0 text-white transition-all  ${scrolling && 'bg-blue-primary'} `}>
            <div className="w-full container mx-auto flex items-center justify-between mt-0 py-2">
                <div className="pl-4 flex items-center">
                    <Link className="toggleColour text-white no-underline hover:no-underline font-bold text-2xl lg:text-4xl"
                          href="/">
                        <Image src="/logo-blanco.png" width={48} height={48} alt="logo diabetIA" />
                    </Link>
                    <div className="pl-6 flex items-center">
                        {userName && (
                            <span className="ml-2 text-white text-sm">Bienvenido, {userName}</span>
                        )}
                    </div>
                </div>
                    <div
                        className="w-full flex items-center mt-2 lg:mt-0 bg-transparent text-black p-4 lg:p-0 z-20"
                    id="nav-content">
                    <ul className="lg:flex justify-end flex-1 items-center mb-0">
                        <Tooltip title="Subí una foto de tu comida, contamos los carbohidratos por vos!" arrow>
                            <li className="mr-3">
                                <NavLink href="/food" text="Registrar comida" className="bg-orange-secondary rounded-lg !py-2 hover:bg-orange-focus transition-all"/>
                            </li>
                        </Tooltip>
                        <Tooltip title="Visualizá todos tus eventos en un mismo lugar" arrow>
                            <li className="mr-3">
                                <NavLink href="/calendar" text="Calendario"/>
                            </li>
                        </Tooltip>
                        <li className="mr-3">
                            <NavLink href="/reports" text="Reportes"/>
                        </li>
                    </ul>
                    <div className="flex justify-center items-center relative">
                        <button className="flex items-center text-white" onClick={onHandleUserClick}>
                            <PersonOutline/>
                        </button>

                        <div className={`${openUserMenu ? 'text-opacity-100' : 'opacity-0'} absolute top-10 transition-all delay-0 ease-in-out`}>
                            {openUserMenu &&
                                <div className={`rounded-2xl p-4 bg-white shadow-2xl`}>
                                    <ul className={`flex flex-col`}>
                                        <li className="mb-6 text-sm text-blue-secondary">
                                            <Link href="/profile">Mi perfil</Link>
                                        </li>
                                        <li className="mb-6 text-sm text-blue-secondary">
                                            <Link href="/reminders">Recordatorios</Link>
                                        </li>
                                        <li className="text-sm text-blue-secondary">
                                            <button onClick={() => handleOnLogout()} className="text-blue-secondary">Cerrar
                                                sesión
                                            </button>
                                        </li>
                                    </ul>
                                </div>
                            }
                        </div>
                    </div>
                </div>
            </div>
            <hr className="border-b border-white opacity-25 my-0 py-0"/>
        </nav>
    )
}