import Link from "next/link";

export const CustomLink = ({text, href}) => {
    return (
        <Link href={href} className="text-gray-secondary underline text-sm" >{text}</Link>
    )
}

export const NavLink = ({text, href, className}) => {
    return (
        <Link className={`inline-block text-white no-underline hover:no-underline focus:no-underline focus:text-white hover:text-white py-2 px-4 text-xl ${className}`} href={href}>{text}</Link>
)
}

export const OrangeLink = ({label, width, href}) => {
    return (
        <div className={`flex justify-center ${width} min-w-max`}>
            <Link
                href={href}
                className="bg-orange-focus hover:bg-orange-primary hover:no-underline transition-all hover:text-white text-white focus:text-white py-2 px-8 rounded-lg w-full flex justify-center text-xl"
            >
                {label}
            </Link>
        </div>
    )
}

export const BlueLink = ({label, width, href}) => {
    return (
        <div className={`flex justify-center ${width}`}>
            <Link
                href={href}
                className="bg-blue-primary hover:bg-blue-focus transition-all text-white py-2 px-8 rounded-lg w-full flex justify-center text-xl"
            >
                {label}
            </Link>
        </div>
    )
}