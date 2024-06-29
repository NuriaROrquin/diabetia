import Link from "next/link";
import Image from "next/image";

export const Footer = () => {
    return (
        <footer className="bg-blue-primary">
            <div className="container mx-auto px-8 flex justify-between items-center py-8">
                <div className="flex flex-col">
                    <p className="font-bold mb-4 text-white">Seguinos en nuestras redes:</p>
                    <ul className="flex flex-col space-y-2">
                        <li className="inline-block mr-2 md:block md:mr-0">
                            <Link href="https://www.instagram.com/diabetia.app" target="_blank" className="flex items-center no-underline text-white hover:font-bold transition-all">
                                <i className="fab fa-instagram mr-2"></i>
                                Instagram
                            </Link>
                        </li>
                        <li className="inline-block mr-2 md:block md:mr-0">
                            <Link href="https://www.youtube.com/@diabetia" target="_blank" className="flex items-center no-underline text-white hover:font-bold transition-all">
                                <i className="fab fa-youtube mr-2"></i>
                                YouTube
                            </Link>
                        </li>
                        <li className="inline-block mr-2 md:block md:mr-0">
                            <Link href="mailto:app.diabetia@gmail.com" className="flex items-center no-underline text-white hover:font-bold transition-all">
                                <i className="fas fa-envelope mr-2"></i>
                                Correo
                            </Link>
                        </li>
                    </ul>
                </div>
                <div>
                    <Image src="/isologo-blanco.png" width="80" height="80" alt="logo diabetIA" />
                </div>
            </div>
        </footer>
    );
}

export default Footer;
