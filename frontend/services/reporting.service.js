import axios from "./axios";

export const getInsulineChartData = (dateFrom, dateTo) => {
    console.log(dateFrom, dateTo)
    /*const initialData = [
        { time: '2018-12-22', value: 2 },
        { time: '2018-12-23', value: 5 },
        { time: '2018-12-24', value: 7 },
        { time: '2018-12-25', value: 5 },
        { time: '2018-12-26', value: 4 },
        { time: '2018-12-27', value: 4 },
        { time: '2018-12-28', value: 4 },
        { time: '2018-12-29', value: 5 },
        { time: '2018-12-30', value: 1 },
        { time: '2018-12-31', value: 2 },
    ];

    return new Promise((resolve, reject) => {
        setTimeout(() => {
            resolve(initialData);
        }, 1000);
    });*/

    return axios
        .get(
            `${process.env.NEXT_PUBLIC_API_URL}/Reporting/getInsulinReport?dateFrom=${dateFrom}&dateTo=${dateTo}`)
}

export const getComparativeQuantityEvents = (dateFrom, dateTo) => {
    /*const insuline = axios.get(`${process.env.NEXT_PUBLIC_API_URL}/Reporting/insuline?dateFrom=${dateFrom}&dateTo=${dateTo}`);
    const glucose = axios.get(`${process.env.NEXT_PUBLIC_API_URL}/Reporting/glucose?dateFrom=${dateFrom}&dateTo=${dateTo}`);
    const physicalActivity = axios.get(`${process.env.NEXT_PUBLIC_API_URL}/Reporting/physicalActivity?dateFrom=${dateFrom}&dateTo=${dateTo}`);
    const food = axios.get(`${process.env.NEXT_PUBLIC_API_URL}/Reporting/food?dateFrom=${dateFrom}&dateTo=${dateTo}`);

    return axios.all([insuline, glucose, physicalActivity, food])
        .then(axios.spread((responseInsuline, responseGlucose, responsePhysicalActivity, responseFood) => {
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
     */

    const fakeInsulineData = [
        { time: '2023-01-01', value: 4 },
        { time: '2023-01-02', value: 5 },
        { time: '2023-01-03', value: 6 },
    ];

    const fakeGlucoseData = [
        { time: '2023-01-01', value: 4 },
        { time: '2023-01-02', value: 1 },
        { time: '2023-01-03', value: 1 },
    ];

    const fakePhysicalActivityData = [
        { time: '2023-01-01', value: 10 },
        { time: '2023-01-02', value: 5 },
        { time: '2023-01-03', value: 4 },
    ];

    const fakeFoodData = [
        { time: '2023-01-01', value: 1 },
        { time: '2023-01-02', value: 5 },
        { time: '2023-01-03', value: 3 },
    ];

    return new Promise((resolve, reject) => {
        setTimeout(() => {
            resolve({
                insuline: fakeInsulineData,
                glucose: fakeGlucoseData,
                physicalActivity: fakePhysicalActivityData,
                food: fakeFoodData
            });
        }, 1000);
    });


}