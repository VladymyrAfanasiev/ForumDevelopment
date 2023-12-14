import { React, useEffect, useState } from 'react';
import { useNavigate } from "react-router-dom";
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
    const [isLoading, setIsLoading] = useState(false);

    useEffect(() => {
        setIsLoginFormEmpty(false);
    })

    function handleLoginFormChange(e) {
        setIsLoginFormEmpty(e.target.value == "" ? true : false);
    }

    const loginClick = async function () {
        setIsLoading(true); 

        const email = document.getElementById("loginPage_email").value;
        const password = document.getElementById("loginPage_password").value;
        const result = await authService.loginAsync(email, password);
        if (result.status) {
            const authUserInfo = authService.getAuthenticationInfo();
            dispatch(setAuthUserInfo(authUserInfo));
            navigate(-1);
        }
        else {
            alert(result.message);
        }

        setIsLoading(false);
    }

    return (
        <div className="loginPage_container">
            <label>
                <b>
                    <Trans>Email</Trans>
                </b>
            </label>
            <input id="loginPage_email" type="text" placeholder={t("Enter Username")} onChange={handleLoginFormChange}></input>
            <label>
                <b>
                    <Trans>Password</Trans>
                </b>
            </label>
            <input id="loginPage_password" type="password" placeholder={t("Enter Password")}></input>
            <div className="loginPage_buttonContainer">
                {
                    isLoading ? (
                        <button className="root_button_loading">
                            <span className="root_button_text">
                                <Trans>Login</Trans>
                            </span>
                        </button>
                    ): (
                        isLoginFormEmpty? (
                                <button className = "root_button root_button_disabled" onClick = { loginClick }>
                                    <Trans>Login</Trans>
                                </button>
                            ) : (
                                <button className="root_button" onClick={loginClick}>
                                    <Trans>Login</Trans>
                                </button>
                            )
                   )
                }
            </div>
        </div>
    );
}

export default withTranslation("translation") (LoginPage);