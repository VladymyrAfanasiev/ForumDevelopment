import { React, useState } from "react";
import { useTranslation, withTranslation } from 'react-i18next';
import { Trans, Plural, Select } from 'react-i18next/icu.macro';

import "./RegisterPage.css";

function RegisterPage() {
    const { t, i18n } = useTranslation();
    const [userImage, setUserImage] = useState(undefined);

    function imageSelectionHandler(e) {
        setUserImage(e.target.files[0]);

        const imagePreview = document.getElementsByClassName("registerPage_imagePreview")[0];

        e.target.files.length == 0 ? imagePreview.src = "/img/anon.png" : imagePreview.src = URL.createObjectURL(e.target.files[0]);
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
            <input type="text" placeholder={t("Enter Username")}></input>
            <label>
                <b>
                    <Trans>Password</Trans>
                </b>
            </label>
            <input type="text" placeholder={t("Enter Password")}></input>
            <label>
                <b>
                    <Trans>Confirm Password</Trans>
                </b>
            </label>
            <input type="text" placeholder={t("Enter Password")}></input>
            <label>
                <b>
                    <Trans>Email Address</Trans>
                </b>
            </label>
            <input type="text" placeholder={t("Enter Email Address")}></input>

            <div className="registerPage_buttonContainer">
                <button className="root_button">
                    <Trans>Register</Trans>
                </button>
            </div>
        </div>
    );
}

export default withTranslation("translation") (RegisterPage);