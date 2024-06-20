import {ChevronLeft} from "@mui/icons-material";
import {useEffect, useState} from 'react';

export const Selector = ({width, selectedOption, options, isOpen, setIsOpen, handleOptionClick, dataTestId}) => {

    return (
        <section className={`${width} flex  min-w-max`}>
            <div className="relative w-full">
                <button
                    data-testid={dataTestId}
                    onClick={() => setIsOpen(!isOpen)}
                    className={`flex appearance-none text-xl text-white w-full bg-blue-secondary px-4 py-3 pr-8 shadow leading-tight focus:outline-none focus:shadow-outline ${isOpen ? 'rounded-b-0 rounded-t-xl' : 'rounded-lg'}`}
                >
                    {selectedOption}
                    <span
                        className="pointer-events-none absolute inset-y-0 right-0 flex items-center px-2 text-white">
                    <ChevronLeft className="color-white fill-white -rotate-90" />
                </span>
                </button>
                {isOpen && (
                    <ul className={`absolute z-10 w-full bg-blue-secondary ${isOpen ? 'rounded-t-0 rounded-b-xl' : 'rounded-xl'} shadow-lg`}>
                        {options.map((option) => (
                            <li
                                key={option}
                                onClick={() => handleOptionClick(option)}
                                data-testid={`option-${option}`}
                                className="px-4 py-2 hover:bg-orange-focus cursor-pointer rounded-xl text-xl text-white z-50"
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
                    <ul className={`absolute z-10 w-full bg-white ${isOpen ? 'rounded-t-0 rounded-b-xl' : 'rounded-xl'} shadow-lg h-44 overflow-scroll`}>
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

export const SelectSearch = ({ width, selectedOption, options, isOpen, setIsOpen, handleOptionClick, placeholder, label, index }) => {
    const [searchTerm, setSearchTerm] = useState("");

    const filteredOptions = options.filter(option =>
        option.title.toLowerCase().includes(searchTerm.toLowerCase())
    );

    useEffect(()=> {
        if(selectedOption){
            setSearchTerm(selectedOption.title)
        }
    }, [selectedOption])


    return (
        <section className={`${width} flex flex-col`}>
            <label className="text-base text-blue-primary font-medium" htmlFor={`${label}`}>{label}</label>

            <div className="relative inline-block w-full h-fit">
                <button
                    onClick={() => setIsOpen(!isOpen, index)}
                    className={`flex appearance-none w-full px-4 py-3 mt-2 pr-8 border border-gray-400 rounded-lg leading-tight focus:outline-none ${isOpen ? 'rounded-b-0 rounded-t-xl' : 'rounded-xl'} items-center py-4`}
                >
                        <input
                            type="text"
                            className="w-full h-full focus:border-0 focus:outline-none"
                            placeholder={placeholder}
                            value={searchTerm}
                            onChange={(e) => setSearchTerm(e.target.value)}
                        />
                    <span
                        className="pointer-events-none absolute inset-y-0 right-0 flex items-center px-2 text-gray-700">
                    <ChevronLeft className="color-gray-primary fill-gray-primary -rotate-90"/>
                </span>
                </button>
                {isOpen && (
                    <div className="absolute z-10 w-full bg-white shadow-lg rounded-xl">
                        <ul className="max-h-52 overflow-scroll">
                            {filteredOptions.map((option) => (
                                <li
                                    key={option.id}
                                    onClick={() => handleOptionClick(option, index)}
                                    className="px-4 py-2 hover:bg-blue-primary hover:text-white cursor-pointer"
                                >
                                    {option.title}
                                </li>
                            ))}
                        </ul>
                    </div>
                )}
            </div>
        </section>
    );
};
