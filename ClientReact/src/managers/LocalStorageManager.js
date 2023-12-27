class LocalStorageManager {
    constructor() {
        this.AUTHENTICATION_INFO_KEY = "authInfo";
    }

    getAuthenticationInfo() {
        let authenticationInfoJson = localStorage.getItem(this.AUTHENTICATION_INFO_KEY);
        if (authenticationInfoJson) {
            return JSON.parse(authenticationInfoJson)
        }
    }

    setAuthenticationInfo(authenticationInfo) {
        localStorage.setItem(this.AUTHENTICATION_INFO_KEY, JSON.stringify(authenticationInfo));
    }

    removeAuthenticationInfo() {
        localStorage.removeItem(this.AUTHENTICATION_INFO_KEY);
    }
}

const localStorageManager = new LocalStorageManager();

export default localStorageManager;