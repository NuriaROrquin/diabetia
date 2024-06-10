export const ButtonBlue = ({label, width, onClick, className}) => {
    return (
        <div className={`flex justify-center ${width} ${className}`}>
            <button
                onClick={onClick}
                className="bg-blue-primary hover:bg-blue-focus transition-all text-white py-2 px-8 rounded-lg w-full text-xl"
            >
                {label}
            </button>
        </div>
    )
}

export const ButtonOrange = ({label, width, onClick}) => {
    return (
        <div className={`flex justify-center ${width}`}>
            <button
                onClick={onClick}
                className="bg-orange-focus hover:bg-orange-primary transition-all text-white py-2 px-8 rounded-lg w-full text-xl"
            >
                {label}
            </button>
        </div>
    )
}

export const ButtonRed = ({label, width, onClick}) => {
    return (
        <div className={`flex justify-center ${width}`}>
            <button
                onClick={onClick}
                className="bg-red-600 hover:bg-red-primary transition-all text-white py-2 px-8 rounded-full w-full text-xl"
            >
                {label}
            </button>
        </div>
    )
}

export const ButtonGreen = ({label, width, onClick}) => {
    return (
        <div className={`flex justify-center ${width}`}>
            <button
                onClick={onClick}
                className="bg-green-600 hover:bg-green-primary transition-all text-white py-2 px-8 rounded-full w-full text-xl"
            >
                {label}
            </button>
        </div>
    )
}