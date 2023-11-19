import { React, useState } from "react";
import { useTranslation, withTranslation } from 'react-i18next';
import { useNavigate } from "react-router-dom";
import { useDispatch } from 'react-redux'
import { Trans, Plural, Select } from 'react-i18next/icu.macro';
import { setAuthUserInfo } from "../../../redux/slice/authUserInfoSlice";

import authService from '../../../services/AuthService';

import "./RegisterPage.css";

function RegisterPage() {
    const { t, i18n } = useTranslation();
    const navigate = useNavigate();
    const dispatch = useDispatch();
    const [userImage, setUserImage] = useState(undefined);

    function imageSelectionHandler(e) {
        setUserImage(e.target.files[0]);

        const imagePreview = document.getElementsByClassName("registerPage_imagePreview")[0];

        e.target.files.length == 0 ? imagePreview.src = "/img/anon.png" : imagePreview.src = URL.createObjectURL(e.target.files[0]);
    }

    const registerClick = async function () {
        const password = document.getElementById("registerPage_Password").value;
        const confirmPassword = document.getElementById("registerPage_confirmPassword").value;
        if (password != confirmPassword) {
            // error

            alert("Password in not equal to Confirm Password")

            return;
        }

        const userName = document.getElementById("registerPage_userName").value;
        const email = document.getElementById("registerPage_email").value;
        const result = await authService.registerAsync(userName, email, password);
        if (result.status) {
            const authUserInfo = authService.getAuthenticationInfo();
            dispatch(setAuthUserInfo(authUserInfo));
            navigate("/");
        }
        else {
            alert(result.message);
        }
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
            <input id="registerPage_Password" type="text" placeholder={t("Enter Password")}></input>
            <label>
                <b>
                    <Trans>Confirm Password</Trans>
                </b>
            </label>
            <input id="registerPage_confirmPassword" type="text" placeholder={t("Enter Password")}></input>
            <label>
                <b>
                    <Trans>Email Address</Trans>
                </b>
            </label>
            <input id="registerPage_email" type="text" placeholder={t("Enter Email Address")}></input>

            <div className="registerPage_buttonContainer">
                <button className="root_button" onClick={registerClick}>
                    <Trans>Register</Trans>
                </button>
            </div>
        </div>
    );
}

export default withTranslation("translation") (RegisterPage);