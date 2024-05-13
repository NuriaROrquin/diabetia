export const Card = ({number, textIndicator, title, description, unit, color="blue"}) => {

    const getTextColor = () => {
        if(color){
            return color === 'blue' ? 'text-blue-primary' : color === 'green' ? 'text-green-primary' : 'text-red-primary'
        }
    }

    return (
        <section className="w-1/4 bg-white p-8 rounded-2xl">
            <div className="w-full flex justify-center flex-col items-center mb-4">
                <div className="flex items-end">
                    <h3 className={`${getTextColor()} text-6xl font-bold`}>{number}</h3>
                    {unit && <span className={`${getTextColor()} font-bold`}>{unit}</span>}
                </div>
                <span className={`font-semibold ${getTextColor()}`}>{textIndicator}</span>
            </div>
            <div className="w-full flex justify-center flex-col items-center gap-2">
                <span className="font-semibold text-gray-primary">{title}</span>
                <span className="text-gray-secondary text-center">{description}</span>
            </div>
        </section>
    )
}
