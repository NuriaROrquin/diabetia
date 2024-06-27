import React, {useEffect, useState} from 'react';
import {SubtitleSection} from "@/components/titles";
import CustomTooltip from "@/components/tooltip";
import {HelpOutline} from "@mui/icons-material";
import {FILTERS, initialMultipleChartOptions, initialSingleChartOptions} from "../../constants";
import {calculateDateRange} from "../../helpers";
import ReactECharts from 'echarts-for-react';

export const ChartAreaComponent = props => {
    const {
        getChartData,
        title,
        helper
    } = props;

    const [data, setData] = useState([]);
    const [loading, setLoading] = useState(true);
    const [filterSelected, setFilterSelected] = useState(FILTERS[0]);
    const [option, setOption] = useState(initialSingleChartOptions);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const titlesArray = FILTERS.map(filter => filter.title);
                const { dateFrom, dateTo } = calculateDateRange(titlesArray.find(title => filterSelected && title === filterSelected.title));
                const response = await getChartData(dateFrom, dateTo);

                const groupedData = new Map();

                response.data.forEach(item => {
                    const date = item.time;
                    const value = item.value;

                    if (groupedData.has(date)) {
                        groupedData.set(date, groupedData.get(date) + value);
                    } else {
                        groupedData.set(date, value);
                    }
                });

                const xAxisData = Array.from(groupedData.keys());
                const seriesData = xAxisData.map(date => ({
                    time: date,
                    value: groupedData.get(date)
                }));

                const updateOption = {
                    ...initialSingleChartOptions,
                    xAxis: {
                        ...initialSingleChartOptions.xAxis,
                        data: xAxisData
                    },
                    series: [
                        {
                            ...initialSingleChartOptions.series[0],
                            data: seriesData
                        }
                    ]
                };

                console.log(updateOption);

                setOption(updateOption);
                setLoading(false);
            } catch (error) {
                setLoading(false);
                console.error('Error fetching data:', error);
            }
        };

        fetchData();
    }, [filterSelected, getChartData, FILTERS]);

    return (
        <>
            <div data-testId="headerChart" className="flex justify-between px-8 py-4">
                <div className="flex justify-center items-center gap-x-2">
                    <SubtitleSection>{title}</SubtitleSection>
                    {helper && (
                        <CustomTooltip title={`${helper}`} placement="top" arrow>
                            <HelpOutline className="text-orange-primary"/>
                        </CustomTooltip>
                    )}
                </div>
                <div className="flex gap-x-4 my-2 relative">
                    {FILTERS.map((filter) => (
                        <button
                            key={filter.title}
                            className={`${
                                filterSelected && filterSelected.id === filter.id
                                    ? 'bg-orange-primary text-white'
                                    : 'bg-gray-200 text-gray-primary'
                            } p-2 rounded-md`}
                            onClick={() => setFilterSelected(filter)}
                        >
                            {filter.title}
                        </button>
                    ))}
                </div>
            </div>

            {loading && (
                <div className="w-full flex justify-center items-center mb-5" data-testid="loader">
                    <svg
                        aria-hidden="true"
                        className="inline w-10 h-10 text-blue-secondary animate-spin dark:text-blue-secondary fill-white"
                        viewBox="0 0 100 101"
                        fill="none"
                        xmlns="http://www.w3.org/2000/svg"
                    >
                        <path
                            d="M100 50.5908C100 78.2051 77.6142 100.591 50 100.591C22.3858 100.591 0 78.2051 0 50.5908C0 22.9766 22.3858 0.59082 50 0.59082C77.6142 0.59082 100 22.9766 100 50.5908ZM9.08144 50.5908C9.08144 73.1895 27.4013 91.5094 50 91.5094C72.5987 91.5094 90.9186 73.1895 90.9186 50.5908C90.9186 27.9921 72.5987 9.67226 50 9.67226C27.4013 9.67226 9.08144 27.9921 9.08144 50.5908Z"
                            fill="currentColor"
                        />
                        <path
                            d="M93.9676 39.0409C96.393 38.4038 97.8624 35.9116 97.0079 33.5539C95.2932 28.8227 92.871 24.3692 89.8167 20.348C85.8452 15.1192 80.8826 10.7238 75.2124 7.41289C69.5422 4.10194 63.2754 1.94025 56.7698 1.05124C51.7666 0.367541 46.6976 0.446843 41.7345 1.27873C39.2613 1.69328 37.813 4.19778 38.4501 6.62326C39.0873 9.04874 41.5694 10.4717 44.0505 10.1071C47.8511 9.54855 51.7191 9.52689 55.5402 10.0491C60.8642 10.7766 65.9928 12.5457 70.6331 15.2552C75.2735 17.9648 79.3347 21.5619 82.5849 25.841C84.9175 28.9121 86.7997 32.2913 88.1811 35.8758C89.083 38.2158 91.5421 39.6781 93.9676 39.0409Z"
                            fill="currentFill"
                        />
                    </svg>
                </div>
            )}

            <ReactECharts
                option={option}
                style={{ height: '300px', width: '100%' }}
            />
        </>
    );
};

export const ChartMultipleLineComponent = props => {
    const [option, setOption] = useState(initialMultipleChartOptions);
    const [loading, setLoading] = useState(true);
    const [filterSelected, setFilterSelected] = useState(FILTERS[0]);

    const {
        getComparative,
        title,
        helper
    } = props;

    useEffect(() => {
        const fetchData = async () => {
            try {
                const { insuline, glucose, physicalActivity, food } = await getComparative();

                const xAxisData = insuline.map(item => item.time);
                const seriesData = {
                    Insuline: insuline.map(item => item.value),
                    Glucose: glucose.map(item => item.value),
                    'Physical Activity': physicalActivity.map(item => item.value),
                    Food: food.map(item => item.value)
                };

                const updatedOption = {
                    ...initialMultipleChartOptions,
                    legend: {
                        ...initialMultipleChartOptions.legend,
                        data: Object.keys(seriesData)
                    },
                    xAxis: {
                        ...initialMultipleChartOptions.xAxis,
                        data: xAxisData
                    },
                    series: initialMultipleChartOptions.series.map(series => ({
                        ...series,
                        data: seriesData[series.name]
                    }))
                };

                setOption(updatedOption);
                setLoading(false);
            } catch (error) {
                console.error('Error fetching data:', error);
                setLoading(false);
            }
        };

        fetchData();
    }, []);

    if (loading) {
        return <div>Loading...</div>;
    }

    return (
        <div>
            <div data-testId="headerChart" className="flex justify-between px-8 py-4">
                <div className="flex justify-center items-center gap-x-2">
                    <SubtitleSection>{title}</SubtitleSection>
                    {helper && (
                        <CustomTooltip title={`${helper}`} placement="top" arrow>
                            <HelpOutline className="text-orange-primary"/>
                        </CustomTooltip>
                    )}
                </div>
                <div className="flex gap-x-4 my-2 relative">
                    {FILTERS.map((filter) => (
                        <button
                            key={filter.title}
                            className={`${
                                filterSelected && filterSelected.id === filter.id
                                    ? 'bg-orange-primary text-white'
                                    : 'bg-gray-200 text-gray-primary'
                            } p-2 rounded-md`}
                            onClick={() => setFilterSelected(filter)}
                        >
                            {filter.title}
                        </button>
                    ))}
                </div>
            </div>

            {loading && (
                <div className="w-full flex justify-center items-center mb-5" data-testid="loader">
                    <svg
                        aria-hidden="true"
                        className="inline w-10 h-10 text-blue-secondary animate-spin dark:text-blue-secondary fill-white"
                        viewBox="0 0 100 101"
                        fill="none"
                        xmlns="http://www.w3.org/2000/svg"
                    >
                        <path
                            d="M100 50.5908C100 78.2051 77.6142 100.591 50 100.591C22.3858 100.591 0 78.2051 0 50.5908C0 22.9766 22.3858 0.59082 50 0.59082C77.6142 0.59082 100 22.9766 100 50.5908ZM9.08144 50.5908C9.08144 73.1895 27.4013 91.5094 50 91.5094C72.5987 91.5094 90.9186 73.1895 90.9186 50.5908C90.9186 27.9921 72.5987 9.67226 50 9.67226C27.4013 9.67226 9.08144 27.9921 9.08144 50.5908Z"
                            fill="currentColor"
                        />
                        <path
                            d="M93.9676 39.0409C96.393 38.4038 97.8624 35.9116 97.0079 33.5539C95.2932 28.8227 92.871 24.3692 89.8167 20.348C85.8452 15.1192 80.8826 10.7238 75.2124 7.41289C69.5422 4.10194 63.2754 1.94025 56.7698 1.05124C51.7666 0.367541 46.6976 0.446843 41.7345 1.27873C39.2613 1.69328 37.813 4.19778 38.4501 6.62326C39.0873 9.04874 41.5694 10.4717 44.0505 10.1071C47.8511 9.54855 51.7191 9.52689 55.5402 10.0491C60.8642 10.7766 65.9928 12.5457 70.6331 15.2552C75.2735 17.9648 79.3347 21.5619 82.5849 25.841C84.9175 28.9121 86.7997 32.2913 88.1811 35.8758C89.083 38.2158 91.5421 39.6781 93.9676 39.0409Z"
                            fill="currentFill"
                        />
                    </svg>
                </div>
            )}

            <ReactECharts
                option={option}
            />
        </div>
    );
};

