import {ChevronLeft} from "@mui/icons-material";

export const Selector = ({width, selectedOption, options, isOpen, setIsOpen, handleOptionClick}) => {

    return (
        <section className={`${width} flex`}>
            <div className="relative w-full">
                <button
                    onClick={() => setIsOpen(!isOpen)}
                    className={`flex appearance-none text-white w-full bg-orange-secondary px-4 py-3 pr-8 shadow leading-tight focus:outline-none focus:shadow-outline ${isOpen ? 'rounded-b-0 rounded-t-xl' : 'rounded-lg'}`}
                >
                    {selectedOption}
                    <span
                        className="pointer-events-none absolute inset-y-0 right-0 flex items-center px-2 text-white">
                    <ChevronLeft className="color-white fill-white -rotate-90" />
                </span>
                </button>
                {isOpen && (
                    <ul className={`absolute z-10 w-full bg-orange-secondary ${isOpen ? 'rounded-t-0 rounded-b-xl' : 'rounded-xl'} shadow-lg`}>
                        {options.map((option) => (
                            <li
                                key={option}
                                onClick={() => handleOptionClick(option)}
                                className="px-4 py-2 hover:bg-orange-primary cursor-pointer rounded-xl"
                            >
                                {option}
                            </li>
                        ))}
                    </ul>
                )}
            </div>
        </section>
    )
}

export const Select = ({ width, selectedOption, options, isOpen, setIsOpen, handleOptionClick, placeholder, label, index }) => {
    return (
        <section className={`${width} flex flex-col`}>
            <label className="text-base text-blue-primary font-medium" htmlFor={`${label}`}>{label}</label>

            <div className="relative inline-block w-full h-fit">
                <button
                    onClick={() => setIsOpen(!isOpen, index)}
                    className={`flex appearance-none w-full px-4 py-3 mt-2 pr-8 border border-gray-400 rounded-lg leading-tight focus:outline-none ${isOpen ? 'rounded-b-0 rounded-t-xl' : 'rounded-xl'} items-center py-4`}
                >
                    {selectedOption ?
                        selectedOption.title :
                        <span className="text-gray-secondary">{placeholder}</span>
                    }
                    <span
                        className="pointer-events-none absolute inset-y-0 right-0 flex items-center px-2 text-gray-700">
                    <ChevronLeft className="color-gray-primary fill-gray-primary -rotate-90"/>
                </span>
                </button>
                {isOpen && (
                    <ul className={`absolute z-10 w-full bg-white ${isOpen ? 'rounded-t-0 rounded-b-xl' : 'rounded-xl'} shadow-lg`}>
                        {options.map((option) => (
                            <li
                                key={option.id}
                                onClick={() => handleOptionClick(option, index)}
                                className="px-4 py-2 hover:bg-blue-primary hover:text-white cursor-pointer rounded-xl"
                            >
                                {option.title}
                            </li>
                        ))}
                    </ul>
                )}
            </div>
        </section>
    )

}