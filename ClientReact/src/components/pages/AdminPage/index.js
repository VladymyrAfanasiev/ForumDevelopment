import { React, useState, useEffect } from "react";
import { Trans } from 'react-i18next/icu.macro';

import MaineFrame from '../../common/MainFrame';
import Request from '../UserPage/Request';

import authService from '../../../services/AuthService';
import forumService from '../../../services/ForumService';

import "./AdminPage.css";

function AdminPage() {
    const [activeTab, setActiveTab] = useState('Requests');
    const [requests, setRequests] = useState([]);
    const [userInfo, setUserInfo] = useState({});

    useEffect(() => {
        async function loadUserInfo() {
            let result = await authService.getAuthenticationInfoAsync();
            setUserInfo(result.data);
        }

        async function loadRequests() {
            const result = await forumService.getRequests();
            if (result.status) {
                setRequests(result.data);
            }
        }

        loadUserInfo();
        loadRequests();
    })

    const handleTabClick = (e) => {
        if (e.target.className.includes("root_button_active")) {
            return;
        }

        const previousActiveTab = document.getElementsByClassName("adminPage_tabs")[0].getElementsByClassName("root_button_active")[0];
        previousActiveTab.className = "root_button";

        e.target.className = "root_button_active";

        setActiveTab(e.target.id);
    }

    return (
        <div className="adminPage_root">
            <div className="adminPage_content">
                <div className="adminPage_tabs">
                    <button className="root_button_active" id="Requests" onClick={handleTabClick}>
                        <Trans>Requests</Trans>
                    </button>
                    <button className="root_button" id="Settings" onClick={handleTabClick}>
                        <Trans>Settings</Trans>
                    </button>
                </div>
                <div className="adminPage_tabContent">
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
                            case "Settings":
                                return (
                                    <div>
                                        {activeTab + " tab content"}
                                    </div>
                                );
                        }
                    })()}
                </div>
            </div>
        </div>
    );
}

export default AdminPage;