import { createSlice } from '@reduxjs/toolkit'

import authService from '../../services/AuthService';

export const authUserInfoSlice = createSlice({
    name: "authUserInfo",
    initialState: { value: authService.getAuthenticationInfo() },
    reducers: {
        setAuthUserInfo: (state, userInfo) => {
            state.value = userInfo.payload;
        }
    }
})

export const { setAuthUserInfo } = authUserInfoSlice.actions;

export default authUserInfoSlice.reducer;