import React from 'react';
import { render, waitFor, fireEvent } from '@testing-library/react';
import Dashboard from "../../pages/dashboard";

jest.mock('../../services/home.service', () => ({
    getMetrics: jest.fn(() =>
        Promise.resolve({
            data: {
                chMetrics: 0,
                glycemia: 99999,
                hyperglycemia: 0,
                hypoglycemia: 0,
                insulin: 0,
                physicalActivity: 0,
            },
        })
    ),
    getTimeline: jest.fn(() =>
        Promise.resolve({
            data: {
                timeline: {
                    items: [
                        { id: 1, title: 'Evento 1' },
                        { id: 2, title: 'Evento 2' },
                    ],
                },
            },
        })
    ),
}));

jest.mock('react-cookie', () => ({
    useCookies: () => [{ email: 'test@example.com' }, jest.fn(), jest.fn()],
}));

const DASHBOARD_OPTIONS_FILTER_DAYS = ['Opción 1', 'Opción 2'];
const DASHBOARD_INDICATORS = [];

describe('Home Component', () => {
    it('renders without crashing', () => {
        render(<Dashboard />);
    });

    it('loads metrics and timeline data on mount', async () => {
        const { getByText } = render(<Dashboard />);

        await waitFor(() => {
            expect(getByText('Tu panel de salud para la gestión de tu diabetes')).toBeInTheDocument();
            expect(getByText('Registros del día')).toBeInTheDocument();
        });
    });

    it('displays timeline events when loaded', async () => {
        const { getByText } = render(<Dashboard />);

        await waitFor(() => {
            expect(getByText('Evento 1')).toBeInTheDocument();
        });
    });
});
