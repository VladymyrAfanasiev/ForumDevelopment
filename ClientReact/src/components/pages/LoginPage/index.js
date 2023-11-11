import { React, useState } from 'react';
import { useNavigate  } from "react-router-dom";
import { useTranslation, withTranslation } from 'react-i18next';
import { Trans, Plural, Select } from 'react-i18next/icu.macro';
import { useDispatch } from 'react-redux'
import { setAuthUserInfo } from "../../../redux/slice/authUserInfoSlice";

import authService from '../../../services/AuthService';

import './LoginPage.css';

function LoginPage() {
    const { t, i18n } = useTranslation();
    const navigate = useNavigate();
    const dispatch = useDispatch();
    const [isLoginFormEmpty, setIsLoginFormEmpty] = useState(true);

    function handleLoginFormChange(e) {
        setIsLoginFormEmpty(e.target.value == "" ? true : false);
    }

    const loginClick = async function () {
        const userName = document.getElementById("loginPage_userName").value;
        const success = await authService.loginAsync(userName);
        if (success) {
            const authUserInfo = authService.getAuthenticationInfo();
            dispatch(setAuthUserInfo(authUserInfo));
            navigate("/");
        }
    }

    return (
        <div className="loginPage_container">
            <label>
                <b>
                    <Trans>Username</Trans>
                </b>
            </label>
            <input id="loginPage_userName" type="text" placeholder={t("Enter Username")} onChange={handleLoginFormChange}></input>
            <label>
                <b>
                    <Trans>Password</Trans>
                </b>
            </label>
            <input type="text" placeholder={t("Enter Password")}></input>
            <div className="loginPage_buttonContainer">
                {
                    isLoginFormEmpty ? (
                        <button className="root_button root_button_disabled" onClick={loginClick}>
                            <Trans>Login</Trans>
                        </button>
                    ) : (
                        <button className="root_button" onClick={loginClick}>
                            <Trans>Login</Trans>
                        </button>
                    )
                }
            </div>
        </div>
    );
}

export default withTranslation("translation") (LoginPage);