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