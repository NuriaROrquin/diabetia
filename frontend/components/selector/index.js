import {useState} from "react";
import {ChevronLeft} from "@mui/icons-material";

export const Selector = ({width, selectedOption, options, isOpen, setIsOpen, handleOptionClick}) => {

    return (
        <section className={`${width} flex`}>
            <div className="relative inline-block w-full">
                <button
                    onClick={() => setIsOpen(!isOpen)}
                    className={`flex appearance-none w-full bg-orange-secondary px-4 py-3 pr-8 shadow leading-tight focus:outline-none focus:shadow-outline ${isOpen ? 'rounded-b-0 rounded-t-xl' : 'rounded-xl'}`}
                >
                    {selectedOption}
                    <span
                        className="pointer-events-none absolute inset-y-0 right-0 flex items-center px-2 text-gray-700">
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