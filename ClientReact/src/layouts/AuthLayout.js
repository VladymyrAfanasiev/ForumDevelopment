import { useEffect } from "react";
import { useNavigate, useOutlet } from "react-router-dom";

import { useDispatch } from "react-redux";
import { setAuthUserInfo } from "../redux/slice/authUserInfoSlice"

import authService from '../services/AuthService';

export const AuthLayout = ({ children }) => {
    const navigate = useNavigate();
    const outlet = useOutlet();
    const dispatch = useDispatch();

    useEffect(() => {
        if (authService.authenticationInfo.isAuthenticated) {
            if (authService.checkTokenExpired()) {
                authService.logoutAsync().then(() => {
                    dispatch(setAuthUserInfo(authService.authenticationInfo));
                });
            }
        }

        if (!authService.authenticationInfo.isAuthenticated) {
            return navigate("/login", { replace: true });
        }
    }, [authService])

    return <div style={{ "height": "100%", "width": "100%" }} >{outlet}</div>;
};