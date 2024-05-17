export const TitleSection = ({children}) => {
    return (
        <div className="text-center text-gray-primary">
            <h1 className="text-3xl font-bold">{children}</h1>
        </div>
    )
}

export const SubtitleSection = ({children}) => {
    return (
        <div className="text-center text-gray-primary">
            <h2 className="text-2xl font-bold">{children}</h2>
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