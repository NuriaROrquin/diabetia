import Link from "next/link";

export const CustomLink = ({text, href}) => {

    return (
        <Link href={href} className="text-gray-secondary underline text-sm" >{text}</Link>
    )
}