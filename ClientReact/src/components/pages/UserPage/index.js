import { React, useState, useEffect } from "react";
import { useParams, useNavigate } from "react-router-dom"
import { withTranslation } from 'react-i18next';
import { Trans } from 'react-i18next/icu.macro';
import dateFormat from "dateformat";

import MaineFrame from '../../common/MainFrame';
import MainFrameSeparator from '../../common/MainFrameSeparator';
import Request from './Request';

import authService from '../../../services/AuthService';
import forumService from '../../../services/ForumService';
import publicVaultService from '../../../services/PublicVaultService';

import "./UserPage.css";

function UserPage() {
    const params = useParams();
    const navigate = useNavigate();
    const [userInfo, setUserInfo] = useState({});
    const [userImageUrl, setUserImageUrl] = useState('/img/anon.png');
    const [currentUserInfo, setCurrentUserInfo] = useState({});
    const [requests, setRequests] = useState([]);
    const [activeTab, setActiveTab] = useState('Requests');

    useEffect(() => {
        async function loadUserInfo() {
            let result = await authService.getUserInfo(params.id);
            if (result.status !== true) {
                navigate('/');
                return;
            }

            setUserInfo(result.data);

            result = await publicVaultService.getUserImageUrl(params.id);
            if (result.status) {
                setUserImageUrl(result.data);
            }

            const authenticationInfo = authService.getAuthenticationInfo();
            setCurrentUserInfo(authenticationInfo.user);
        }

        async function loadRequests() {
            const result = await forumService.getRequests();
            if (result.status) {
                setRequests(result.data);
            }
        }

        loadUserInfo();
        loadRequests();
    }, [params])

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
            <MaineFrame name={userInfo.userName} title="User name">
                <div className="userPage_userGeneralInfo">
                    <div className="userPage_img">
                        <img src={userImageUrl} />
                    </div>
                    <div className="userPage_shortDescription">
                        <p>User name: {userInfo.name}</p>
                        <p>Email: {userInfo.email}</p>
                        <p>Joined date: {dateFormat(userInfo.joinedOn, "mmmm dS, yyyy")}</p>
                    </div>
                </div>
                {
                    userInfo.id === currentUserInfo.id &&
                    <div>
                        <MainFrameSeparator />
                        <div className="userPage_tabs">
                            <button className="root_button_active" id="Requests" onClick={handleTabClick}>
                                <Trans>Requests</Trans>
                            </button>
                            <button className="root_button" id="Profile" onClick={handleTabClick}>
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
                                    case "Requests":
                                        return (
                                            <div>
                                                <ol>
                                                    {
                                                        requests.map(request => <li><Request request={request} userInfo={userInfo} /></li>)
                                                    }
                                                </ol>
                                            </div>
                                        );
                                    case "Profile":
                                        return (
                                            <div>
                                                {activeTab + " tab content"}
                                            </div>
                                        );
                                    case "Activity":
                                        return (
                                            <div>
                                                {activeTab + " tab content"}
                                            </div>
                                        );
                                }
                            })()}
                        </div>
                    </div>
                }
            </MaineFrame>
        </div>
    );
}

export default withTranslation("translation") (UserPage);