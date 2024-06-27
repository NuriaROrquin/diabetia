import React from 'react';
import { render } from '@testing-library/react';
import {EventCard, MetricCard, ProfileCard} from "@/components/card";
import {ContactMailOutlined, DirectionsRunOutlined, FolderSharedOutlined} from "@mui/icons-material";
import MedicalInformationOutlinedIcon from "@mui/icons-material/MedicalInformationOutlined";

describe('MetricCard Component', () => {

    it('renders correctly', () => {
        const { getByText } = render(
            <MetricCard
                number={100}
                textIndicator="Indicator Text"
                title="Metric Title"
                description="Metric Description"
                unit="unit"
                tooltipContent="Tooltip Content"
                selectedOption="selected"
                loading={false}
                isWarning={true}
            />
        );

        expect(getByText('Metric Title')).toBeInTheDocument();
        expect(getByText('Metric Description')).toBeInTheDocument();
    });
});

describe('EventCard Component', () => {
    const events = [
        { title: 'Event 1', link: '/event1', image: '/event1.jpg' },
        { title: 'Event 2', link: '/event2', image: '/event2.jpg' }
    ];

    it('renders correctly', () => {
        const { getByText } = render(
            <EventCard events={events} />
        );

        expect(getByText('Event 1')).toBeInTheDocument();
        expect(getByText('Event 2')).toBeInTheDocument();
    });
});

describe('ProfileCard Component', () => {
    const editInfo = [
        { title: 'Datos personales', link: '/personal' },
        { title: 'Información del paciente', link: '/patient' },
        { title: 'Datos de actividad física y salud', link: '/physical-activity' },
        { title: 'Dispositivos y sensores', link: '/devices' },
        { title: 'Titulo default', link: '/default' },
    ];

    it('renders correctly', () => {
        const { getByText, getByTestId } = render(
            <ProfileCard editInfo={editInfo} />
        );

        expect(getByText('Datos personales')).toBeInTheDocument();
        expect(getByText('Información del paciente')).toBeInTheDocument();
        expect(getByText('Datos de actividad física y salud')).toBeInTheDocument();
        expect(getByText('Dispositivos y sensores')).toBeInTheDocument();
        expect(getByText('Titulo default')).toBeInTheDocument();

        const iconPersonal = getByTestId('icon-Datos personales');
        const iconPaciente = getByTestId('icon-Información del paciente');
        const iconActividad = getByTestId('icon-Datos de actividad física y salud');
        const iconDispositivos = getByTestId('icon-Dispositivos y sensores');

        expect(iconPersonal).toBeInTheDocument();
        expect(iconPaciente).toBeInTheDocument();
        expect(iconActividad).toBeInTheDocument();
        expect(iconDispositivos).toBeInTheDocument();
    });
});
