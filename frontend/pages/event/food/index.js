import {Section} from "../../../components/section";
import {TitleSection} from "../../../components/titles";
import {TYPE_EVENTS, TYPE_INGREDIENTS, TYPE_UNITOFMEASUREMENT} from "../../../constants";
import {capitalizeFirstLetter} from "../../../helpers";
import {useState} from "react";
import {BlueLink, OrangeLink} from "../../../components/link";
import {InputWithLabel} from "../../../components/input";
import {Select} from "../../../components/selector";
import dayjs from "dayjs";
import {ButtonOrange} from "../../../components/button";
import {CustomDatePicker, CustomTimePicker} from "../../../components/pickers";
import {addPhysicalEvent} from "../../../services/api.service";
import {useRouter} from "next/router";
import {useCookies} from "react-cookie";

const ExerciseEvent = () => {
    const eventSelected = TYPE_EVENTS.filter((event) => event.id === 3)[0].title;
    const [isOpenIngredients, setIsOpenIngredients] = useState(false);
    const [selectedOptionIngredients, setSelectedOptionIngredients] = useState(null);
    const [isOpenUnit, setIsOpenUnit] = useState(false);
    const [selectedOptionUnit, setSelectedOptionUnit] = useState(null);
    const [startHour, setStartHour] = useState()
    const [endHour, setEndHour] = useState()
    const [date, setDate] = useState()
    const [Hour, setHour] = useState()
    const [cookies, _setCookie, _removeCookie] = useCookies(['email']);

    const [ingredientes, setIngredientes] = useState([]);

    const router = useRouter();

    const handleAgregarIngrediente = () => {
        setIngredientes([...ingredientes, { ingrediente: '', cantidad: '', unidad: '' }]);
    };

    const handleChangeIngrediente = (index, campo, valor) => {
        const nuevosIngredientes = [...ingredientes];
        nuevosIngredientes[index][campo] = valor;
        setIngredientes(nuevosIngredientes);
    };

    const handleEliminarIngrediente = (index) => {
        const nuevosIngredientes = [...ingredientes];
        nuevosIngredientes.splice(index, 1);
        setIngredientes(nuevosIngredientes);
    };

    const handleOptionClickIngredients = (option) => {
        setSelectedOptionIngredients(option);
        setIsOpenIngredients(false);
    };

    const handleOptionClickUnit = (option) => {
        setSelectedOptionUnit(option);
        setIsOpenUnit(false);
    };

    const handleSubmit = () => {
        const exercise = selectedOption;
        const dateFormatted = date ? date.format('DD-MM-YYYY') : null;
        const start = startHour ? startHour.format('HH:mm:ss') : null;
        const end = endHour ? endHour.format('HH:mm:ss') : null;
        const notes = document.getElementById("notes").value;
        const email = cookies.email;

        const data = {
            "email": email,
            "idKindEvent": 4,
            "eventDate": "2024-05-22T23:03:17.219Z",
            "freeNote": notes,
            "physicalActivity": selectedOption.id,
            "iniciateTime": start,
            "finishTime": end
        }

        addPhysicalEvent(data).then(() =>
            router.push("/calendar")
        )
    }

    return(
        <Section className="pt-12">
            <div className="container items-center flex w-full justify-center flex-col">
                <TitleSection className="text-white">¿Qué evento querés cargar?</TitleSection>
                <div className="flex w-full flex-wrap gap-y-6 gap-x-24 justify-center mt-8">
                    {TYPE_EVENTS.map((event) => {
                        return(
                            <>
                                {event.title === eventSelected ?
                                    <OrangeLink key={event.title} label={capitalizeFirstLetter(event.title)} width="w-1/6" href={event.link} />
                                    :
                                    <BlueLink key={event.title} label={capitalizeFirstLetter(event.title)} width="w-1/6" href={event.link} />
                                }
                            </>
                        )
                    })}
                </div>

                {/* FORMULARIO */}
                <div className="bg-white rounded-xl w-full flex flex-wrap text-gray-primary py-20 px-44 my-12 justify-around gap-x-2 gap-y-12">
                    <CustomDatePicker
                        label="Ingresá una fecha"
                        value={date}
                        onChange={(e) => setDate(e)}
                        defaultValue={dayjs()}
                        width="w-1/2"
                    />

                    <CustomTimePicker
                        label="Elija el horario de la comida"
                        value={Hour}
                        onChange={setHour}
                        defaultValue={dayjs()}
                        width="w-1/3"
                    />


                    {ingredientes.map((ingrediente, index) => (
                        <div key={index} style={{ display: 'flex', alignItems: 'center', marginBottom: '1rem' }}>
                            <Select
                                label="Ingrediente"
                                placeholder="Elegí un ingrediente"
                                options={TYPE_INGREDIENTS}
                                selectedOption={ingrediente.ingrediente}
                                handleOptionClick={(selected) => handleChangeIngrediente(index, 'ingrediente', selected)}
                                width="w-1/3"
                            />
                            <div style={{ margin: '0 1rem' }} /> {/* Espacio entre elementos */}
                            <InputWithLabel
                                label="Cantidad del ingrediente"
                                placeholder="Escribí la cantidad"
                                value={ingrediente.cantidad}
                                onChange={(event) => handleChangeIngrediente(index, 'cantidad', event.target.value)}
                                width="w-1/5"
                            />
                            <div style={{ margin: '0 1rem' }} /> {/* Espacio entre elementos */}
                            <Select
                                label="Unidad de medida"
                                placeholder="Elegí la unidad de medida"
                                options={TYPE_UNITOFMEASUREMENT}
                                selectedOption={ingrediente.unidad}
                                handleOptionClick={(selected) => handleChangeIngrediente(index, 'unidad', selected)}
                                width="w-1/3"
                            />
                            <div style={{ margin: '0 1rem' }} /> {/* Espacio entre elementos */}
                            <button type="button" onClick={() => handleEliminarIngrediente(index)}>
                                Eliminar
                            </button>
                        </div>
                    ))}
                    <button type="button" onClick={handleAgregarIngrediente}>
                        Agregar Ingrediente
                    </button>

                </div>
            </div>
        </Section>
    )
}

export default ExerciseEvent;