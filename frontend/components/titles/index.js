export const TitleSection = ({children, className = ""}) => {
    return (
        <div className={`text-center text-gray-primary ${className}`}>
            <h1 className="text-4xl font-bold">{children}</h1>
        </div>
    )
}

export const SubtitleSection = ({children, className = ""}) => {
    return (
        <div className={`text-center text-gray-primary ${className}`}>
            <h2 className="text-3xl font-medium">{children}</h2>
        </div>
    )
}

export const ContainerTitles = ({children}) => {
    return (
        <div className="flex flex-col gap-4 mt-20 mb-12">
            {children}
        </div>
    )
}