import { React, useState } from "react";
import { useParams } from "react-router-dom"
import { withTranslation } from 'react-i18next';
import { Trans } from 'react-i18next/icu.macro';

import MaineFrame from '../../common/MainFrame';
import MainFrameSeparator from '../../common/MainFrameSeparator';

import "./UserPage.css";

function UserPage(props) {
    const params = useParams();
    const [activeTab, setActiveTab] = useState('Profile');

    const handleTabClick = (e) => {
        if (e.target.className.includes("root_button_active")) {
            return;
        }

        const previousActiveTab =  document.getElementsByClassName("userPage_tabs")[0].getElementsByClassName("root_button_active")[0];
        previousActiveTab.className = "root_button";

        e.target.className = "root_button_active";

        setActiveTab(e.target.id);
    }

    return (
        <div className="userPage_content">
            <MaineFrame name={params.id} title="User name">
                <div className="userPage_userGeneralInfo">
                    <div className="userPage_img">
                        <img src="/img/anon.png" />
                    </div>
                    <div className="userPage_shortDescription">
                        <p>{params.id}</p>
                        <p>Email</p>
                        <p>Joined date</p>
                    </div>
                </div>
                <div>
                    <MainFrameSeparator />
                    <div className="userPage_tabs">
                        <button className="root_button_active" id="Profile" onClick={handleTabClick}>
                            <Trans>Profile</Trans>
                        </button>
                        <button className="root_button" id="Activity" onClick={handleTabClick}>
                            <Trans>Activity</Trans>
                        </button>
                    </div>
                    <MainFrameSeparator />
                    <div className="userPage_tabContent">
                        {(() => {
                            switch (activeTab) {
                                case "Profile":
                                    return (
                                        <div>
                                            { activeTab + " tab content" }
                                        </div>
                                    ); 
                                case "Activity":
                                    return(
                                        <div>
                                            { activeTab + " tab content" }
                                        </div>
                                    );
                            }
                        })()}
                    </div>
                </div>
            </MaineFrame>
        </div>
    );
}

export default withTranslation("translation") (UserPage);