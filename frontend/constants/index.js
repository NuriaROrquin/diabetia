export const DASHBOARD_OPTIONS_FILTER_DAYS = ['Últimas 24hs', 'Últimas 48hs', 'Última semana', 'Último mes'];

export const DASHBOARD_INDICATORS = [
    {
        "textIndicator": "Glucemia",
        "color": "green",
        "number": 115,
        "title": "Promedio de medidas",
        "description": "Cuando midas tu glucosa en sangre, tu promedio se verá afectado.",
        "tooltipContent":"Se visualiza el promedio de glucosa en sangre (mg/dL) basado en tus registros "
    },
    {
        "textIndicator": "Hiperglucemias",
        "color": "green",
        "number": 0,
        "title": "En total",
        "description": "Nivel de glucosa en sangre demasiado alta.",
        "tooltipContent":"Se visualiza la cantidad de mediciones con un nivel bajo de glucosa "
    },
    {
        "textIndicator": "Hipoglucemias",
        "color": "red",
        "number": 1,
        "title": "En total",
        "description": "Nivel de glucosa en sangre demasiado baja.",
        "tooltipContent":"Se visualiza la cantidad de mediciones con nivel de glucosa más alto de lo esperado "
    },
    {
        "textIndicator": "Carbohidratos",
        "color": "blue",
        "number": 102,
        "unit": "G",
        "title": "En total",
        "description": "Cuando cargues una comida, este número se verá afectado.",
        "tooltipContent":"Se visualiza la cantidad de carbohidratos presentes en sus comidas "
    },
    {
        "textIndicator": "Insulina",
        "color": "blue",
        "number": 7,
        "unit": "U",
        "title": "En total",
        "description": "5u insulina rápida y 2u insulina lenta",
        "tooltipContent":"Se visualiza la cantidad de dosis de insulina administrada "
    },
    {
        "textIndicator": "Ejercicio",
        "color": "red",
        "number": 25,
        "unit": "Min",
        "title": "En total",
        "description": "La recomendación son 30 min de ejercicio diario",
        "tooltipContent":"Se visualiza la cantidad de ejercicio realizado "
    }
]

export const DASHBOARD_TIMELINE_EVENTS = [
    {
        "time": "19:00",
        "title": "Desayuno - Registro de alimentos"
    },
    {
        "time": "18:00",
        "title": "Deporte running 25 min"
    },
    {
        "time": "17:21",
        "title": "Hipogucemia"
    },
    {
        "time": "15:30",
        "title": "Carga merienda"
    },
    {
        "time": "14:02",
        "title": "Carga almuerzo"
    },
    {
        "time": "13:30",
        "title": "Medida de glucemia"
    },
    {
        "time": "12:26",
        "title": "Medida de glucemia"
    },
    {
        "time": "09:39",
        "title": "Carga desayuno"
    }
]

export const TYPE_EVENTS = [
    {
        "id": 1,
        "title": "ACTIVIDAD FÍSICA",
        "image": "/actividad-fisica.jpg",
        "link": "/event/exercise"
    },
    {
        "id": 2,
        "title": "GLUCEMIA",
        "image": "/glucemia.jpg",
        "link": "/event/glycemia"
    },
    {
        "id": 3,
        "title": "COMIDA",
        "image": "/comida.jpg",
        "link": "/event/food"
    },
    {
        "id": 4,
        "title": "INSULINA",
        "image": "/insulina.jpg",
        "link": "/event/insulin"
    },
    {
        "id": 5,
        "title": "EVENTO DE SALUD",
        "image": "/salud.jpg",
        "link": "/event/health-event"
    },
    {
        "id": 6,
        "title": "VISITA MÉDICA",
        "image": "/visita-medico.jpg",
        "link": "/event/medical-visit"
    },
    {
        "id": 7,
        "title": "ESTUDIOS",
        "image": "/examenes.jpg",
        "link": "/event/studies"
    },
    {
        "id": 8,
        "title": "NOTA LIBRE",
        "image": "/nota-libre.jpg",
        "link": "/event/free-note"
    }
];

export const TYPE_EXERCISES = [
    {
        "id": 1,
        "title": "Correr"
    },
    {
        "id": 2,
        "title": "Caminar"
    },
    {
        "id": 3,
        "title": "Nadar"
    },
    {
        "id": 4,
        "title": "Ciclismo"
    },
    {
        "id": 5,
        "title": "Caminata"
    },
    {
        "id": 6,
        "title": "Natación"
    },
    {
        "id": 7,
        "title": "Otra"
    }
]