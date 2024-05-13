import '../app/globals.css'
import {Navigation} from "../components/navigation";
import {useRouter} from "next/router";
import {MainContainer} from "../components/mainContainer";

export default function App({ Component, pageProps }) {
    const router = useRouter();
    console.log(router.route)

    const isLogged = !router.route.startsWith('/auth');
    return(
        <>
            {isLogged &&
                <Navigation path={router.route} />
            }
            {isLogged &&
                <MainContainer>
                    <Component {...pageProps} />
                </MainContainer>
            }
            {!isLogged && <Component {...pageProps} />}
        </>
    )
}