import React from 'react';
import {render, fireEvent, getByRole} from '@testing-library/react';
import {Select, Selector, SelectSearch} from "@/components/selector";

describe('Selector Component', () => {
    it('renders correctly with closed dropdown', () => {
        const options = ['Option 1', 'Option 2', 'Option 3'];
        const { getByTestId, getByRole, queryByText } = render(
            <Selector
                width="w-1/3"
                selectedOption="Option 1"
                options={options}
                isOpen={false}
                setIsOpen={() => {}}
                handleOptionClick={() => {}}
                dataTestId="selector"
            />
        );

        const button = getByTestId('selector');

        expect(button).toBeInTheDocument();
        expect(button).toHaveTextContent('Option 1');
    });

    it('handles option click correctly', () => {
        const options = ['Option 1', 'Option 2', 'Option 3'];
        const handleOptionClick = jest.fn();
        const { getByTestId, getByText } = render(
            <Selector
                width="w-1/3"
                selectedOption="Option 1"
                options={options}
                isOpen={true}
                setIsOpen={() => {}}
                handleOptionClick={handleOptionClick}
                dataTestId="selector"
            />
        );

        const option = getByText('Option 2');
        fireEvent.click(option);
        expect(handleOptionClick).toHaveBeenCalledWith('Option 2');
    });

    it('toggles isOpen on button click', () => {
        const options = ['Option 1', 'Option 2', 'Option 3'];
        const setIsOpen = jest.fn(); // Mock de la función setIsOpen

        const {getByTestId} =render(
            <Selector
                width="w-1/3"
                selectedOption="Option 1"
                options={options}
                isOpen={false}
                setIsOpen={setIsOpen}
                handleOptionClick={() => {}}
                dataTestId="selector"
            />
        );

        const button = getByTestId('selector');

        fireEvent.click(button);

        expect(setIsOpen).toHaveBeenCalledTimes(1);
        expect(setIsOpen).toHaveBeenCalledWith(true);
    });
});

describe('Select Component', () => {
    it('renders correctly with closed dropdown', () => {
        const options = [
            { id: 1, title: 'Option 1' },
            { id: 2, title: 'Option 2' },
            { id: 3, title: 'Option 3' },
        ];
        const { getByTestId, queryByText } = render(
            <Select
                width="w-1/3"
                selectedOption={{ title: 'Option 1' }}
                options={options}
                isOpen={false}
                setIsOpen={() => {}}
                handleOptionClick={() => {}}
                placeholder="Select an option"
                label="Select Label"
                index={0}
            />
        );

        const button = getByTestId('select');
        expect(button).toBeInTheDocument();
        expect(button).toHaveTextContent('Option 1');

        fireEvent.click(button);
    });

    it('handles option click correctly', () => {
        const options = [
            { id: 1, title: 'Option 1' },
            { id: 2, title: 'Option 2' },
            { id: 3, title: 'Option 3' },
        ];
        const handleOptionClick = jest.fn();
        const { getByTestId, getByText } = render(
            <Select
                width="w-1/3"
                selectedOption={{ title: 'Option 1' }}
                options={options}
                isOpen={true}
                setIsOpen={() => {}}
                handleOptionClick={handleOptionClick}
                placeholder="Select an option"
                label="Select Label"
                index={0}
            />
        );

        const option = getByText('Option 2');
        fireEvent.click(option);
        expect(handleOptionClick).toHaveBeenCalledWith(options[1], 0);
    });
});

describe('SelectSearch Component', () => {
    it('renders correctly with closed dropdown', () => {
        const options = [
            { id: 1, title: 'Option 1' },
            { id: 2, title: 'Option 2' },
            { id: 3, title: 'Option 3' },
        ];
        const { getByTestId, queryByText } = render(
            <SelectSearch
                width="w-1/3"
                selectedOption={{ title: 'Option 1' }}
                options={options}
                isOpen={false}
                setIsOpen={() => {}}
                handleOptionClick={() => {}}
                placeholder="Search for an option"
                label="Search Label"
                index={0}
            />
        );

        const input = getByTestId('search-input');
        expect(input).toBeInTheDocument();
    });

    it('handles option click correctly', () => {
        const options = [
            { id: 1, title: 'Option 1' },
            { id: 2, title: 'Option 2' },
            { id: 3, title: 'Option 3' },
        ];
        const handleOptionClick = jest.fn();
        const { getByTestId, getByText } = render(
            <SelectSearch
                width="w-1/3"
                selectedOption={{ title: 'Option 1' }}
                options={options}
                isOpen={true}
                setIsOpen={() => {}}
                handleOptionClick={handleOptionClick}
                placeholder="Search for an option"
                label="Search Label"
                index={0}
            />
        );

        const option = getByText('Option 1');
        fireEvent.click(option);
        expect(handleOptionClick).toHaveBeenCalledWith(options[0], 0);
    });

    it('updates searchTerm on input change', () => {
        const options = [
            { id: 1, title: 'Option 1' },
            { id: 2, title: 'Option 2' },
            { id: 3, title: 'Option 3' },
        ];
        const handleOptionClick = jest.fn();
        const { getByPlaceholderText } = render(
            <SelectSearch
                width="w-1/3"
                selectedOption={null}
                options={options}
                isOpen={false}
                setIsOpen={() => {}}
                handleOptionClick={handleOptionClick}
                placeholder="Search..."
                label="Search Options"
                index={0}
            />
        );

        const input = getByPlaceholderText('Search...');
        fireEvent.change(input, { target: { value: 'Option' } });

        expect(input.value).toBe('Option');
    });

    it('toggles isOpen on button click', () => {
        const options = [
            { id: 1, title: 'Option 1' },
            { id: 2, title: 'Option 2' },
            { id: 3, title: 'Option 3' },
        ];
        const handleOptionClick = jest.fn();
        const setIsOpen = jest.fn();
        const index = 0;

        const {getByRole} =render(
            <SelectSearch
                width="w-1/3"
                selectedOption={null}
                options={options}
                isOpen={false}
                setIsOpen={setIsOpen}
                handleOptionClick={handleOptionClick}
                placeholder="Search..."
                label="Search Options"
                index={index}
            />
        );

        const button = getByRole('button');

        fireEvent.click(button);

        expect(setIsOpen).toHaveBeenCalledTimes(1);
        expect(setIsOpen).toHaveBeenCalledWith(true, index);
    });
});

