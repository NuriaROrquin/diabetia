import Link from "next/link";

export const CustomLink = ({text, href}) => {
    return (
        <Link href={href} className="text-gray-secondary underline text-sm" >{text}</Link>
    )
}

export const NavLink = ({text, href, className}) => {
    return (
        <Link className={`inline-block text-white no-underline py-2 px-4 ${className}`} href={href}>{text}</Link>
    )
}

export const OrangeLink = ({label, width, href}) => {
    return (
        <div className={`flex justify-center ${width}`}>
            <Link
                href={href}
                className="bg-orange-focus hover:bg-orange-primary transition-all text-white py-2 px-8 rounded-full w-full flex justify-center"
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
                className="bg-blue-primary hover:bg-blue-focus transition-all text-white py-2 px-8 rounded-full w-full flex justify-center"
            >
                {label}
            </Link>
        </div>
    )
}