import { useEffect } from "react";
import { useNavigate, useOutlet } from "react-router-dom";

import authService from '../services/AuthService';

export const AuthLayout = ({ children }) => {
    const navigate = useNavigate();
    const outlet = useOutlet();

    useEffect(() => {
      if (!authService.getAuthenticationInfo().isAuthenticated) {
        return navigate("/login", { replace: true });
      }
   }, [authService])

   return <div>{outlet}</div>;
};