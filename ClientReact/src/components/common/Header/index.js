import { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import { useTranslation } from 'react-i18next';
import { useNavigate } from 'react-router-dom'
import { useSelector, useDispatch } from "react-redux";
import SpeechRecognition, { useSpeechRecognition } from 'react-speech-recognition'
import { setAuthUserInfo } from "../../../redux/slice/authUserInfoSlice"

import authService from '../../../services/AuthService';

import './Header.css';

function Header() {
    const { t, i18n } = useTranslation();
    const navigate = useNavigate();
    const authUserInfo = useSelector(state => state.authUserInfo.value);
    const dispatch = useDispatch();
    const { transcript } = useSpeechRecognition();
    const recognition = new SpeechRecognition.getRecognition();

    recognition.addEventListener("end", () => {
        document.getElementById("header_search_input").value = transcript;
    });

    function searchButtonClick() {
        const searchValue = document.getElementById("header_search_input").value;
        if (searchValue == "") {
            return;
        }

        navigate("search/" + searchValue);
    }

    async function logoutClicked() {
        const success = await authService.logoutAsync();
        if (success) {
            const newUserInfo = await authService.getAuthenticationInfoAsync();
            dispatch(setAuthUserInfo({ newUserInfo }));
            navigate('/')
        }
    }

    function changeLanguage() {
        const index = i18n.options.supportedLngs.indexOf(i18n.language);
        if (index == i18n.options.supportedLngs.length - 2) {
            i18n.changeLanguage(i18n.options.supportedLngs[0]);
        } else {
            i18n.changeLanguage(i18n.options.supportedLngs[index + 1]);
        }
    }

    return (
        <div className="header">
            <div className="header-content">
                {
                    authUserInfo?.isAuthenticated ? (
                        <div className="header_navigation">
                            <div className="header_dropdown">
                                <button className="root_button">
                                    {
                                        t("Menu")
                                    }
                                </button>
                                <ul className="header_menu_dropdown" id="header_menu_dropdown">
                                    <li>
                                        <Link className="root_a_button" to="/requestCreateNewGroup">
                                            {
                                                t("Request to create a new group")
                                            }
                                        </Link>
                                    </li>
                                    {
                                        authUserInfo?.user?.role == 1 &&
                                        <li>
                                            <Link className="root_a_button" to="/admin">
                                                {
                                                    t("Admin")
                                                }
                                            </Link>
                                        </li>
                                    }
                                    <li>
                                        <Link className="root_a_button" to="/about">
                                            {
                                                t("About")
                                            }
                                        </Link>
                                    </li>
                                </ul>
                            </div>
                            <div className="header_home header_indent">
                                <Link className="root_a_button" to="/">
                                    {
                                        t("Forum name")
                                    }
                                </Link>
                            </div>
                        </div>
                    ) : (
                        <div className="header_home">
                            <Link className="root_a_button" to="/">
                                {
                                    t("Forum name")
                                }
                            </Link>
                        </div>
                    )
                }
                {
                    authUserInfo?.isAuthenticated ? (
                        <div className="header_left">

                            <div className="header_search">
                                <input id="header_search_input" autoComplete="off" type="text" placeholder={t("I search...")} />
                                <button onClick={SpeechRecognition.startListening}>
                                    <img src="/img/microphone.svg" alt="" />
                                </button>
                                <button className="root_button" title={t("Search")} onClick={searchButtonClick}>
                                    {
                                        t("Search")
                                    }
                                </button>
                            </div>
                            <div className="header_dropdown header_indent">
                                <button className="root_button">
                                    {
                                        authUserInfo?.user?.userName
                                    }
                                </button>
                                <ul className="header_menu_dropdown" id="header_menu_dropdown">
                                    <li>
                                        <Link className="root_a_button" to={"/user/" + authUserInfo?.user?.id}>
                                            {
                                                t("Profile")
                                            }
                                        </Link>
                                    </li>
                                    <li>
                                        <button className="root_button" to="/" onClick={logoutClicked}>
                                            {
                                                t("Logout")
                                            }
                                        </button>
                                    </li>
                                </ul>
                            </div>
                            <a className="root_a_button header_indent" onClick={changeLanguage}>
                                {
                                    i18n.language
                                }
                            </a>
                        </div>
                    ) : (
                        <div className="header_left">
                            <Link className="root_a_button" to="/login">
                                {
                                    t("Login")
                                }
                            </Link>
                            <Link className="root_a_button header_indent" to="/registerlicence">
                                {
                                    t("Register")
                                }
                            </Link>
                            <button className="root_button header_indent" onClick={changeLanguage}>
                                {
                                    i18n.language
                                }
                            </button>
                        </div>
                    )
                }
            </div>
        </div>
    );
}

export default Header;