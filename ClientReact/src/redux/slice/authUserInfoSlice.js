import { createSlice } from '@reduxjs/toolkit'

export const authUserInfoSlice = createSlice({
        name: "authUserInfo",
        initialState: {},
        reducers: {
            setAuthUserInfo: (state, userInfo) => {
                state.value = userInfo.payload;
            }
        }
    }
)

export const { setAuthUserInfo } = authUserInfoSlice.actions;

export default authUserInfoSlice.reducer;