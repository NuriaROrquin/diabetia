import axios from "./axios";

export const getInsulineChartData = (dateFrom, dateTo) => {
    return axios
        .get(
            `${process.env.NEXT_PUBLIC_API_URL}/InsulineReport/GetInsulinSummaryDoseReport?dateFrom=${dateFrom}&dateTo=${dateTo}`)
}

export const getComparativeQuantityEvents = (dateFrom, dateTo) => {
    const insuline = axios.get(`${process.env.NEXT_PUBLIC_API_URL}/InsulineReport/GetInsulinSummaryEventReport?dateFrom=${dateFrom}&dateTo=${dateTo}`);
    const glucose = axios.get(`${process.env.NEXT_PUBLIC_API_URL}/GlucoseReport/GetGlucoseSummaryEventReport?dateFrom=${dateFrom}&dateTo=${dateTo}`);
    const physicalActivity = axios.get(`${process.env.NEXT_PUBLIC_API_URL}/PhysicalActivityReport/GetPhysicalActivitySummaryEventReport?dateFrom=${dateFrom}&dateTo=${dateTo}`);
    const food = axios.get(`${process.env.NEXT_PUBLIC_API_URL}/FoodReport/GetFoodSummaryEventReport?dateFrom=${dateFrom}&dateTo=${dateTo}`);

    return axios.all([insuline, glucose, physicalActivity, food]) //TODO: agregar food
        .then(axios.spread((responseInsuline, responseGlucose, responsePhysicalActivity, responseFood) => {
            console.log(responseGlucose)
            return {
                food: responseFood.data,
                insuline: responseInsuline.data,
                glucose: responseGlucose.data,
                physicalActivity: responsePhysicalActivity.data
            };
        }))
        .catch(error => {
            console.error('Error fetching data:', error);
            throw error;
        });
}