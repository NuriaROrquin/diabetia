import {Section} from "@/components/section";
import {TitleSection, SubtitleSection} from "@/components/titles";
import {DASHBOARD_TIMELINE_EVENTS, INFO_PROFILE} from "../../constants";
import {ProfileCard} from "../../components/card";
import {Timeline} from "@/components/timeline";

const ProfilePage = () => {
    return(
        <Section className="">
            <div className="bg-blue-primary w-full h-screen bg-opacity-70 flex flex-col  gap-12">
                <div>
                    <TitleSection className="text-white pt-20 mb-6">Mi perfil</TitleSection>
                </div>
                <div className=" flex w-full">
                    <div className="w-2/5">
                        <ProfileCard editInfo={INFO_PROFILE}/>
                    </div>

                    <div className="w-3/5 ml-4 mr-4 bg-white rounded-lg overflow-hidden shadow-lg h-3/5">

                        <SubtitleSection className="!text-blue-primary pt-10 mb-8">
                            Próximos Recordatorios
                        </SubtitleSection>
                        <Timeline events={DASHBOARD_TIMELINE_EVENTS} limit={8}/>
                    </div>
                </div>
            </div>
        </Section>
    )
}

export default ProfilePage;