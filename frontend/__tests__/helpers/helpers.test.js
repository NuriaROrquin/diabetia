import {
    capitalizeFirstLetter,
    formatDateTime,
    getEmailFromJwt,
    getInitialFormCompletedFromJwt,
    getUsernameFromJwt,
    getJwt,
    calculateDateRange,
} from "../../helpers";
import {jwtDecode} from "jwt-decode";

const sessionStorageMock = (() => {
    let store = {};

    return {
        getItem(key) {
            return store[key] || null;
        },
        setItem(key, value) {
            store[key] = value.toString();
        },
        removeItem(key) {
            delete store[key];
        },
        clear() {
            store = {};
        }
    };
})();

Object.defineProperty(window, 'sessionStorage', {
    value: sessionStorageMock
});

jest.mock("jwt-decode");
const mockJwtDecode = jest.fn();

beforeEach(() => {
    jest.clearAllMocks();
    jwtDecode.mockImplementation(mockJwtDecode);
});

describe('Helpers Tests', () => {

    describe('capitalizeFirstLetter', () => {
        it('capitalizes the first letter of each word in a string', () => {
            expect(capitalizeFirstLetter('hello world')).toBe('Hello World');
            expect(capitalizeFirstLetter('tHis iS a TeSt')).toBe('This Is A Test');
        });
    });

    describe('formatDateTime', () => {
        it('formats a date object into DD/MM HH:mm format', () => {
            const date = new Date('2024-06-01T10:30:00');
            expect(formatDateTime(date)).toBe('01/06 10:30');
        });
    });

    describe('getEmailFromJwt', () => {
        it('returns email decoded from JWT', () => {
            mockJwtDecode.mockReturnValue({ email: 'test@example.com' });
            window.sessionStorage.setItem('jwt', 'mock_jwt_token');
            expect(getEmailFromJwt()).toBe('test@example.com');
        });

        it('returns null if JWT is not present', () => {
            window.sessionStorage.removeItem('jwt');
            expect(getEmailFromJwt()).toBeNull();
        });
    });

    describe('getInitialFormCompletedFromJwt', () => {
        it('returns initialFormCompleted decoded from JWT', () => {
            mockJwtDecode.mockReturnValue({ initialFormCompleted: true });
            window.sessionStorage.setItem('jwt', 'mock_jwt_token');
            expect(getInitialFormCompletedFromJwt()).toBe(true);
        });

        it('returns null if JWT is not present', () => {
            window.sessionStorage.removeItem('jwt');
            expect(getInitialFormCompletedFromJwt()).toBeNull();
        });

        it('returns null if JWT does not have initialFormCompleted property', () => {
            mockJwtDecode.mockReturnValue({});
            window.sessionStorage.setItem('jwt', 'mock_jwt_token');
            expect(getInitialFormCompletedFromJwt()).toBeUndefined();
        });
    });


    describe('getUsernameFromJwt', () => {
        it('returns username decoded from JWT', () => {
            mockJwtDecode.mockImplementationOnce((token) => {
                return {
                    username: 'testuser'
                };
            });
            window.sessionStorage.setItem('jwt', 'mock_jwt_token');
            expect(getUsernameFromJwt()).toBe('testuser');
        });

        it('returns null if JWT is not present', () => {
            window.sessionStorage.removeItem('jwt');
            expect(getUsernameFromJwt()).toBeNull();
        });
    });

    describe('getJwt', () => {
        it('returns JWT from sessionStorage', () => {
            window.sessionStorage.setItem('jwt', 'mock_jwt_token');
            expect(getJwt()).toBe('mock_jwt_token');
        });

        it('returns undefined if sessionStorage is not available', () => {
            window.sessionStorage.removeItem('jwt');
            expect(getJwt()).toBeNull();
        });
    });

    describe('calculateDateRange', () => {
        it('calculates date range for "Últimas 24hs"', () => {
            const currentDate = new Date();
            const dateRange24h = calculateDateRange('Últimas 24hs');
            const expectedDateFrom = new Date(currentDate);
            expectedDateFrom.setDate(currentDate.getDate() - 1);

            const expectedDateFromISO = expectedDateFrom.toISOString().split('T')[0];
            const expectedDateToISO = new Date().toISOString().split('T')[0];

            expect(dateRange24h.dateFrom.split('T')[0]).toBe(expectedDateFromISO);
            expect(dateRange24h.dateTo.split('T')[0]).toBe(expectedDateToISO);
        });

        it('calculates date range for "Últimas 48hs"', () => {
            const currentDate = new Date();
            const dateRange48h = calculateDateRange('Últimas 48hs');
            const expectedDateFrom = new Date(currentDate);
            expectedDateFrom.setDate(currentDate.getDate() - 2);

            const expectedDateFromISO = expectedDateFrom.toISOString().split('T')[0];
            const expectedDateToISO = new Date().toISOString().split('T')[0];

            expect(dateRange48h.dateFrom.split('T')[0]).toBe(expectedDateFromISO);
            expect(dateRange48h.dateTo.split('T')[0]).toBe(expectedDateToISO);
        });

        it('calculates date range for "Última semana"', () => {
            const currentDate = new Date();
            const dateRangeWeek = calculateDateRange('Última semana');
            const expectedDateFrom = new Date(currentDate);
            expectedDateFrom.setDate(currentDate.getDate() - 7);

            const expectedDateFromISO = expectedDateFrom.toISOString().split('T')[0];
            const expectedDateToISO = new Date().toISOString().split('T')[0];

            expect(dateRangeWeek.dateFrom.split('T')[0]).toBe(expectedDateFromISO);
            expect(dateRangeWeek.dateTo.split('T')[0]).toBe(expectedDateToISO);
        });

        it('calculates date range for "Último mes"', () => {
            const currentDate = new Date();
            const dateRangeMonth = calculateDateRange('Último mes');
            const expectedDateFrom = new Date(currentDate);
            expectedDateFrom.setMonth(currentDate.getMonth() - 1);

            const expectedDateFromISO = expectedDateFrom.toISOString().split('T')[0];
            const expectedDateToISO = new Date().toISOString().split('T')[0];

            expect(dateRangeMonth.dateFrom.split('T')[0]).toBe(expectedDateFromISO);
            expect(dateRangeMonth.dateTo.split('T')[0]).toBe(expectedDateToISO);
        });

        it('returns current date range if no valid option is provided', () => {
            const currentDate = new Date();
            const dateRangeDefault = calculateDateRange('Invalid Option');
            const expectedDateToISO = new Date().toISOString().split('T')[0];

            const currentDateISO = currentDate.toISOString().split('T')[0];

            expect(dateRangeDefault.dateFrom.split('T')[0]).toBe(currentDateISO);
            expect(dateRangeDefault.dateTo.split('T')[0]).toBe(expectedDateToISO);
        });
    });

});
