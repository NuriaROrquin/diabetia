import '../app/globals.css'
import {Navigation} from "../components/navigation";
import {useRouter} from "next/router";
import {MainContainer} from "../components/mainContainer";
import {Footer} from "../components/footer";
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs'
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import {AIDataProvider} from "../context";


export default function App({ Component, pageProps }) {
    const router = useRouter();

    const isLogged = !router.route.startsWith('/auth');
    return(
        <>
            <AIDataProvider>
                <LocalizationProvider dateAdapter={AdapterDayjs}>
                    {isLogged &&
                        <>
                            <Navigation path={router.route} />
                            <MainContainer>
                                <Component {...pageProps} />
                            </MainContainer>
                        </>
                    }
                    {!isLogged && <Component {...pageProps} />}
                    {isLogged &&
                        <Footer />
                    }
                </LocalizationProvider>
            </AIDataProvider>
        </>
    )
}