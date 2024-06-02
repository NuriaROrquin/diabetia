import {Section} from "@/components/section";
import {TitleSection} from "@/components/titles";
import {useRouter} from "next/router";
import {INFO_PROFILE} from "../../constants";
import {ProfileCard} from "../../components/card";

const ProfilePage = () => {
    return(
        <Section className="">
            <div className="bg-blue-primary w-full h-screen bg-opacity-70 flex flex-col items-center gap-12">
                <div>
                    <TitleSection className="text-white pt-20 mb-6">Mi perfil</TitleSection>
                </div>
                <div className="container gap-y-24 gap-x-1 flex flex-wrap justify-around items-center">
                    <ProfileCard editInfo={INFO_PROFILE}/>
                </div>
            </div>
        </Section>
    )
}

export default ProfilePage;