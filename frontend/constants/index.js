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
        "key": "carbohidrates",
        "unit": "G",
        "title": "En total",
        "description": "Cuando cargues una comida, este número se verá afectado.",
        "tooltipContent":"Se visualiza la cantidad de carbohidratos presentes en sus comidas ",
    },
    {
        "textIndicator": "Insulina",
        "color": "blue",
        "key": "insulin",
        "unit": "U",
        "title": "En total",
        "description": "Tu insulina rápida y tu insulina lenta",
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
        "dateTime": "2024-06-10T15:00:00",
        "title": "Glucosa"
    },
    {
        "dateTime": "2024-06-10T13:40:00",
        "title": "Visita Medica"
    },
    {
        "dateTime": "2024-06-10T15:30:00",
        "title": "Insulina"
    },
]

export const TYPE_EVENTS = [
    {
        "id": 1,
        "title": "INSULINA",
        "image": "/insulina.jpg",
        "link": "/event/insulin"
    },
    {
        "id": 2,
        "title": "COMIDA",
        "image": "/comida.jpg",
        "link": "/event/food"
    },
    {
        "id": 3,
        "title": "GLUCOSA",
        "image": "/glucemia.jpg",
        "link": "/event/glycemia"
    },
    {
        "id": 4,
        "title": "ACTIVIDAD FÍSICA",
        "image": "/actividad-fisica.jpg",
        "link": "/event/exercise"
    },
    {
        "id": 5,
        "title": "SALUD",
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

export const ACTIVITY_FREQUENCY = [
    {
        "id": 1,
        "title": "1 vez por semana"
    },
    {
        "id": 2,
        "title": "2 veces por semana"
    },
    {
        "id": 3,
        "title": "3 veces por semana"
    },
    {
        "id": 4,
        "title": "4 veces por semana"
    },
    {
        "id": 5,
        "title": "5 veces por semana"
    }


]

export const ACTIVITY_HOURS_WEEK = [
    {
        "id": 1,
        "title": "1 hora"
    },
    {
        "id": 2,
        "title": "2 horas"
    },
    {
        "id": 3,
        "title": "3 horas"
    },
    {
        "id": 4,
        "title": "4 horas"
    },
    {
        "id": 5,
        "title": "5 horas"
    },
    {
        "id": 6,
        "title": "6 horas"
    },
    {
        "id": 7,
        "title": "7 horas"
    },
    {
        "id": 8,
        "title": "8 horas"
    },
    {
        "id": 9,
        "title": "9 horas"
    },
    {
        "id": 10,
        "title": "10 horas"
    }
]

export const TYPE_ILLNESS = [
    {
        "id": 1,
        "title": "Hipertensión"
    },
    {
        "id": 2,
        "title": "Enfermedad cardíaca"
    },
    {
        "id": 3,
        "title": "Enfermedad renal"
    },
    {
        "id": 4,
        "title": "Neuropatía periférica"
    },
    {
        "id": 5,
        "title": "Retinopatía diabética"
    },
    {
        "id": 6,
        "title": "Pie diabético"
    },
    {
        "id": 7,
        "title": "Asma"
    },
    {
        "id": 8,
        "title": "Artritis"
    }
]

export const MANY_MEASUREMENTS = [
    {
        "id": 1,
        "title": "1 vez"
    },
    {
        "id": 2,
        "title": "2 veces"
    },
    {
        "id": 3,
        "title": "3 veces"
    },
    {
        "id": 4,
        "title": "4 veces"
    },
    {
        "id": 5,
        "title": "5 veces"
    },
    {
        "id": 6,
        "title": "6 veces"
    }
]

export const TYPE_INGREDIENTS = [
    {
        "id": 1,
        "title": "Milanesa"
    },
    {
        "id": 2,
        "title": "Brocoli"
    },
    {
        "id": 3,
        "title": "Arroz"
    },
    {
        "id": 4,
        "title": "Papa"
    },
    {
        "id": 5,
        "title": "Lechuga"
    },
    {
        "id": 6,
        "title": "Tomate"
    },
    {
        "id": 7,
        "title": "Pechuga de pollo"
    }
]

export const TYPE_UNITOFMEASUREMENT = [
    {
        "id": 1,
        "title": "gr"
    },
    {
        "id": 2,
        "title": "ml"
    },
    {
        "id": 3,
        "title": "unidad"
    },
    {
        "id": 4,
        "title": "cucharada"
    },
    {
        "id": 5,
        "title": "taza"
    },
    {
        "id": 6,
        "title": "cc"
    }
]

export const TYPE_REMINDERTIME = [
    {
        "id": 1,
        "title": "1 semana"
    },
    {
        "id": 2,
        "title": "2 semanas"
    },
    {
        "id": 3,
        "title": "3 semanas"
    },
    {
        "id": 4,
        "title": "1 mes"
    },
    {
        "id": 5,
        "title": "2 meses"
    },
    {
        "id": 6,
        "title": "3 meses"
    },
    
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
        url: '/profile/step-1'
    },
    {
        title: 'Datos del paciente',
        url: '/profile/step-2'
    },
    {
        title: 'Actividad física y salud',
        url: '/profile/step-3'
    },
    {
        title: 'Dispositivos y sensores',
        url: '/profile/step-4'
    }
];

export const TYPE_ILLNESS_HEALTH_EVENT = [
    {
        "id": 1,
        "title": "Gripe"
    },
    {
        "id": 2,
        "title": "Faringitis"
    },
    {
        "id": 3,
        "title": "Neumonia"
    },
    {
        "id": 4,
        "title": "Rinitis"
    },
    {
        "id": 5,
        "title": "Conjuntivitis"
    },
    {
        "id": 6,
        "title": "Gastroenterocolitis"
    },
    {
        "id": 7,
        "title": "Gastritis"
    },
    {
        "id": 8,
        "title": "Migraña"
    }
]

export const TYPE_MEDIC = [
    {
        "id": 1,
        "title": "Diabetologo"
    },
    {
        "id": 2,
        "title": "Nutricionista"
    },
    {
        "id": 3,
        "title": "Cardiologo"
    },
    {
        "id": 4,
        "title": "Gastroenterologo"
    },
    {
        "id": 5,
        "title": "Dermatologo"
    },
    {
        "id": 6,
        "title": "Oculista"
    },
    {
        "id": 7,
        "title": "Clinico"
    },
    {
        "id": 8,
        "title": "Pediatra"
    }
]

export const FILTERS = [
    {
        id: 1,
        title: "1D"
    },
    {
        id: 2,
        title: "1S"
    },
    {
        id: 3,
        title: "1M"
    },
    {
        id: 4,
        title: "1A"
    }
]

/*******************************************CHARTS*****************************************/

export const initialMultipleChartOptions = {
    legend: {
        data: ['Insuline', 'Glucose', 'Physical Activity', 'Food']
    },
    grid: {
        left: '3%',
        right: '4%',
        bottom: '3%',
        containLabel: true
    },
    tooltip: {
        trigger: 'axis'
    },
    toolbox: {
        feature: {
            saveAsImage: {}
        }
    },
    xAxis: {
        type: 'category',
        boundaryGap: false,
        data: []
    },
    yAxis: {
        type: 'value',
    },
    series: [
        {
            name: 'Insulina',
            type: 'line',
            data: []
        },
        {
            name: 'Glucosa',
            type: 'line',
            data: []
        },
        {
            name: 'Actividad física',
            type: 'line',
            data: []
        },
        {
            name: 'Comida',
            type: 'line',
            data: []
        }
    ]
};

export const initialSingleChartOptions = {
    grid: {
        left: '3%',
        right: '4%',
        bottom: '3%',
        containLabel: true
    },
    toolbox: {
        feature: {
            saveAsImage: {},
        }
    },
    xAxis: {
        type: 'category',
        data: []
    },
    yAxis: {
        type: 'value'
    },
    series: [
        {
            data: [],
            type: 'line'
        }
    ]
};

export const initialPieChartOptions = {
    tooltip: {
        trigger: 'item'
    },
    legend: {
        top: '5%',
        left: 'center'
    },
    toolbox: {
        feature: {
            saveAsImage: {},
        }
    },
    series: [
        {
            name: 'Actividad',
            type: 'pie',
            radius: ['40%', '70%'],
            avoidLabelOverlap: false,
            itemStyle: {
                borderRadius: 10,
                borderColor: '#fff',
                borderWidth: 2
            },
            label: {
                show: false,
                position: 'center'
            },
            emphasis: {
                label: {
                    show: true,
                    fontSize: 20,
                    fontWeight: 'bold'
                }
            },
            labelLine: {
                show: false
            },
            data: []
        }
    ]
};