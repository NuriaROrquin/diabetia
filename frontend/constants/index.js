export const DASHBOARD_OPTIONS_FILTER_DAYS = ['Últimas 24hs', 'Últimas 48hs', 'Última semana', 'Último mes'];

export const DASHBOARD_INDICATORS = [
    {
        "textIndicator": "Glucemia",
        "color": "green",
        "number": 115,
        "title": "Promedio de medidas",
        "description": "Cuando midas tu glucosa en sangre, tu promedio se verá afectado."
    },
    {
        "textIndicator": "Hiperglucemias",
        "color": "green",
        "number": 0,
        "title": "En total",
        "description": "Nivel de glucosa en sangre demasiado alta."
    },
    {
        "textIndicator": "Hipoglucemias",
        "color": "red",
        "number": 1,
        "title": "En total",
        "description": "Nivel de glucosa en sangre demasiado baja."
    },
    {
        "textIndicator": "Carbohidratos",
        "color": "blue",
        "number": 102,
        "unit": "G",
        "title": "En total",
        "description": "Cuando cargues una comida, este número se verá afectado."
    },
    {
        "textIndicator": "Insulina",
        "color": "blue",
        "number": 7,
        "unit": "U",
        "title": "En total",
        "description": "5u insulina rápida y 2u insulina lenta"
    },
    {
        "textIndicator": "Ejercicio",
        "color": "red",
        "number": 25,
        "unit": "Min",
        "title": "En total",
        "description": "La recomendación son 30 min de ejercicio diario"
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
        "title": "ACTIVIDAD FÍSICA",
        "image": "/actividad-fisica.jpg",
        "link": "/event/exercise"
    },
    {
        "title": "GLUCEMIA",
        "image": "/glucemia.jpg",
        "link": "/event/glycemia"
    },
    {
        "title": "COMIDA",
        "image": "/comida.jpg",
        "link": "/event/food"
    },
    {
        "title": "INSULINA",
        "image": "/insulina.jpg",
        "link": "/event/insulin"
    },
    {
        "title": "EVENTO DE SALUD",
        "image": "/salud.jpg",
        "link": "/event/health-event"
    },
    {
        "title": "VISITA MÉDICA",
        "image": "/visita-medico.jpg",
        "link": "/event/medical-visit"
    },
    {
        "title": "ESTUDIOS",
        "image": "/examenes.jpg",
        "link": "/event/studies"
    },
    {
        "title": "NOTA LIBRE",
        "image": "/nota-libre.jpg",
        "link": "/event/free-note"
    }
];