import { React, useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import { Link } from 'react-router-dom';
import { useNavigate } from "react-router-dom";
import dateFormat from "dateformat";
import { Trans, Plural, Select } from 'react-i18next/icu.macro';

import MainFrame from '../MainFrame'

import authService from '../../../services/AuthService';
import forumService from '../../../services/ForumService';

import './Request.css';

function Request(props) {
    const [request, setRequest] = useState({});
    const [userInfo, setUserInfo] = useState({});

    useEffect(() => {
        setRequest(props.request);
        const userInfo = authService.getAuthenticationInfo();

        setUserInfo(userInfo);
    }, [props])

    const approveRequest = async function () {
        const result = await forumService.approveRequest(request.id);
        if (result.status) {
            setRequest(result.data);
        }
        else {
            alert(result.message);
        }
    }

    return (
        <div className="request_item_container">
            <MainFrame>
                <div className="request_item">
                    <img width={40} height={40} src="/img/forum.svg" alt="" />
                    <div className="request_item_name_description">
                        <p>
                            {
                                request?.name
                            }
                        </p>
                        {
                            request?.description !== '' &&
                            <p>
                                {
                                    request?.description
                                }
                            </p>
                        }
                    </div>
                    <div className="request_item_status">
                        {
                            request?.status === 0 ? "New" : (request?.status === 1 ? "Approved" : (request?.status === 2 ? "Declined" : "Undefined"))
                        }
                    </div>
                </div>
                {
                    userInfo?.user?.role === 1 && request?.status === 0 &&
                    <div className="request_item_buttons">
                        <button className="root_a_button" onClick={approveRequest}>
                            <Trans>Approve</Trans>
                        </button>
                        <button className="root_a_button">
                            <Trans>Decline</Trans>
                        </button>
                    </div>
                }
            </MainFrame>
        </div>

    );
}

export default Request;