import { React, useState } from "react";
import { useTranslation, withTranslation } from 'react-i18next';
import { useNavigate } from "react-router-dom";
import { useDispatch } from 'react-redux'
import { Trans, Plural, Select } from 'react-i18next/icu.macro';
import { setAuthUserInfo } from "../../../redux/slice/authUserInfoSlice";

import authService from '../../../services/AuthService';
import publicVaultService from '../../../services/PublicVaultService';

import "./RegisterPage.css";

function RegisterPage() {
    const { t, i18n } = useTranslation();
    const navigate = useNavigate();
    const dispatch = useDispatch();
    const [userImage, setUserImage] = useState(undefined);
    const [isLoading, setIsLoading] = useState(false);

    function imageSelectionHandler(e) {
        setUserImage(e.target.files[0]);

        const imagePreview = document.getElementsByClassName("registerPage_imagePreview")[0];

        e.target.files.length == 0 ? imagePreview.src = "/img/anon.png" : imagePreview.src = URL.createObjectURL(e.target.files[0]);
    }

    const registerClick = async function () {
        setIsLoading(true);

        const password = document.getElementById("registerPage_Password").value;
        const confirmPassword = document.getElementById("registerPage_confirmPassword").value;
        if (password != confirmPassword) {
            alert("Password in not equal to confirm password")
            setIsLoading(false);
            return;
        }

        const userName = document.getElementById("registerPage_userName").value;
        const email = document.getElementById("registerPage_email").value;
        let result = await authService.registerAsync(userName, email, password);
        if (result.status != true) {
            alert(result.message);
            setIsLoading(false);
            return;
        }

        if (userImage) {
            let result = await publicVaultService.uploadUserImage(userImage);
            if (result.status != true) {
                // show just warning
                alert(result.message);
            }
        }

        const authUserInfo = authService.getAuthenticationInfo();
        dispatch(setAuthUserInfo(authUserInfo));
        navigate("/");

        setIsLoading(false);
    }

    return (
        <div className="registerPage_container">
            <label>
                <b>
                    <Trans>Image</Trans>
                </b>
            </label>
            <img className="registerPage_imagePreview" src="/img/anon.png" alt="" />
            <input type="file" onChange={imageSelectionHandler}></input>
            <label>
                <b>
                    <Trans>Username</Trans>
                </b>
            </label>
            <input id="registerPage_userName" type="text" placeholder={t("Enter Username")}></input>
            <label>
                <b>
                    <Trans>Password</Trans>
                </b>
            </label>
            <input id="registerPage_Password" type="password" placeholder={t("Enter Password")}></input>
            <label>
                <b>
                    <Trans>Confirm Password</Trans>
                </b>
            </label>
            <input id="registerPage_confirmPassword" type="password" placeholder={t("Enter Password")}></input>
            <label>
                <b>
                    <Trans>Email Address</Trans>
                </b>
            </label>
            <input id="registerPage_email" type="text" placeholder={t("Enter Email Address")}></input>

            <div className="registerPage_buttonContainer">
                {
                    isLoading ? (
                        <button className="root_button_loading">
                            <span className="root_button_text">
                                <Trans>Register</Trans>
                            </span>
                        </button>
                    ) : (
                        <button className="root_button" onClick={registerClick}>
                            <Trans>Register</Trans>
                        </button>
                    )
                }
            </div>
        </div>
    );
}

export default withTranslation("translation") (RegisterPage);