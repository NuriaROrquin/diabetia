export const DASHBOARD_OPTIONS_FILTER_DAYS = ['Últimas 24hs', 'Últimas 48hs', 'Última semana', 'Último mes'];

export const DASHBOARD_INDICATORS = [
    {
        "textIndicator": "Glucemia",
        "color": "green",
        "key": "glycemia",
        "title": "Última medida",
        "description": "Cuando midas tu glucosa en sangre, se mostrará en este apartado.",
        "tooltipContent":"Se visualiza el promedio de glucosa en sangre (mg/dL) basado en tus registros "
    },
    {
        "textIndicator": "Hiperglucemias",
        "color": "green",
        "key": "hyperglycemia",
        "title": "En total",
        "description": "Nivel de glucosa en sangre demasiado alta.",
        "tooltipContent":"Se visualiza la cantidad de mediciones con nivel de glucosa más alto de lo esperado "
    },
    {
        "textIndicator": "Hipoglucemias",
        "color": "red",
        "key": "hypoglycemia",
        "title": "En total",
        "description": "Nivel de glucosa en sangre demasiado baja.",
        "tooltipContent":"Se visualiza la cantidad de mediciones con un nivel bajo de glucosa "
    },
    {
        "textIndicator": "Carbohidratos",
        "color": "blue",
        "key": "chMetrics",
        "unit": "G",
        "title": "En total",
        "description": "Cuando cargues una comida, este número se verá afectado.",
        "tooltipContent":"Se visualiza la cantidad de carbohidratos presentes en sus comidas "
    },
    {
        "textIndicator": "Insulina",
        "color": "blue",
        "key": "insulin",
        "unit": "U",
        "title": "En total",
        "description": "5u insulina rápida y 2u insulina lenta",
        "tooltipContent":"Se visualiza la cantidad de dosis de insulina administrada "
    },
    {
        "textIndicator": "Ejercicio",
        "color": "red",
        "key": "physicalActivity",
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
        "title": "Futbol"
    },
    {
        "id": 2,
        "title": "Basket"
    },
    {
        "id": 3,
        "title": "Tenis"
    },
    {
        "id": 4,
        "title": "Natación"
    },
    {
        "id": 5,
        "title": "Running"
    },
    {
        "id": 6,
        "title": "Ciclismo"
    },
    {
        "id": 7,
        "title": "Yoga"
    }
]

export const TYPE_PORTIONS = [
    {
        "id": 1,
        "title": "1/4 porción",
        "quantity": 0.25
    },
    {
        "id": 2,
        "title": "1/3 porción",
        "quantity": 0.33
    },
    {
        "id": 3,
        "title": "1/2 porción",
        "quantity": 0.5
    },
    {
        "id": 4,
        "title": "1 porción",
        "quantity": 1
    },
    {
        "id": 5,
        "title": "2 porción",
        "quantity": 2
    },
    {
        "id": 6,
        "title": "3 porción",
        "quantity": 3
    },
    {
        "id": 7,
        "title": "4 porción",
        "quantity": 4
    }

]

export const TYPE_DEVICES = [
    {
        "id": 1,
        "title": "Accu-Chek"
    },
    {
        "id": 2,
        "title": "OneTouch"
    },
    {
        "id": 3,
        "title": "Medtronic"
    },
    {
        "id": 4,
        "title": "Dexcom"
    },
    {
        "id": 5,
        "title": "Freestyle"
    }
]

export const TYPE_DIABETES = [
    {
        "id": 1,
        "title": "Tipo I"
    },
    {
        "id": 2,
        "title": "Tipo II"
    },
    {
        "id": 3,
        "title": "Gestacional"
    }
]

export const TYPE_INSULIN = [
    {
        "id": 1,
        "title": "Acción rápida"
    },
    {
        "id": 2,
        "title": "Acción corta"
    },
    {
        "id": 3,
        "title": "Acción intermedia"
    },
    {
        "id": 4,
        "title": "Acción prolongada"
    }
]
export const INSULIN_FREQUENCY = [
    {
        "id": 1,
        "title": "1 vez por día"
    },
    {
        "id": 2,
        "title": "2 veces por día"
    },
    {
        "id": 3,
        "title": "3 veces por día"
    }
]

export const GENDER = [
    {
        "id": 1,
        "title": "Masculino",
        "key": 'M'
    },
    {
        "id": 2,
        "title": "Femenino",
        "key": 'F'
    },
    {
        "id": 3,
        "title": "Prefiero no decir",
        "key": 'X'
    }
]

export const INFO_PROFILE = [
    {
        "title": 'Datos personales',
        "link": '/profile/step-1'
    },
    {
        "title": 'Información del paciente',
        "link": '/profile/step-2'
    },
    {
        "title": 'Datos de actividad física y salud',
        "link": '/profile/step-3'
    },
    {
        "title": 'Dispositivos y sensores',
        "link": '/profile/step-4'
    }
]

export const STEPS = [
    {
        title: 'Datos personales',
        url: 'profile/step-1'
    },
    {
        title: 'Datos del paciente',
        url: 'profile/step-2'
    },
    {
        title: 'Actividad física y salud',
        url: 'profile/step-3'
    },
    {
        title: 'Dispositivos y sensores',
        url: 'profile/step-4'
    }
];