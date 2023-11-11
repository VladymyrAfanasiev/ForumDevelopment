import { configureStore } from "@reduxjs/toolkit";
import authUserInfoReducer from "./slice/authUserInfoSlice";

export default configureStore({
    reducer: {
        authUserInfo: authUserInfoReducer
    }
})