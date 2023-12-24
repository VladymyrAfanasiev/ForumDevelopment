import axios from 'axios';

class AuthService {
    constructor() {
        this.authenticationInfo = {
            isAuthenticated: false
        };
    }

    async registerAsync(userName, email, password) {
        try {
            const data = {
                UserName: userName,
                Email: email,
                Password: password
            };

            const response = await axios.put('/api/authorization/register', data, {
                headers: {
                    'Content-Type': 'application/json'
                }
            });

            this.authenticationInfo = {
                isAuthenticated: true,
                user: response.data.user
            };

            // TODO: redux
            axios.defaults.headers.common['Authorization'] = 'Bearer ' + response.data.token.token;

            return {
                status: true,
                message: ''
            };
        }
        catch (error) {
            return {
                status: false,
                message: error.response.data
            };
        }
    }

    async loginAsync(email, password) {
        try {
            let response = await axios.get('/api/authorization/user/' + email + '/salt', {} , {
                headers: {
                    'Content-Type': 'application/json'
                }
            });

            const salt = response.data;
            const CryptoJS = require('crypto-js');
            const keySize = (256 / 8) / 4;
            const key = CryptoJS.PBKDF2(password, CryptoJS.enc.Base64.parse(salt), { hasher: CryptoJS.algo.SHA256, keySize, iterations: 100000 });
            const passwordHash = key.toString(CryptoJS.enc.Base64);
            const data = {
                Email: email,
                PasswordHash: passwordHash
            };

            response = await axios.post('/api/authorization/login', data, {
                headers: {
                    'Content-Type': 'application/json'
                }
            });

            this.authenticationInfo = {
                isAuthenticated: true,
                user: response.data.user
            };

            // TODO: redux
            axios.defaults.headers.common['Authorization'] = 'Bearer ' + response.data.token.token;

            return {
                status: true,
                message: ''
            };
        }
        catch (error) {
            this.authenticationInfo = {
                isAuthenticated: false
            };

            return {
                status: false,
                message: error.response.data
            };
        }
    }

    async logoutAsync() {

        // TODO: redux
        delete axios.defaults.headers.common["Authorization"];

        this.authenticationInfo = {
            isAuthenticated: false
        };

        return {
            status: true,
            message: ''
        };
    }

    // TODO: Add user info caching
    async getUserInfo(id) {
        if (this.authenticationInfo.isAuthenticated && this.authenticationInfo.user.id === id) {
            return {
                status: true,
                data: this.authenticationInfo.user,
                message: ''
            }; 
        }

        try {
            const response = await axios.get('/api/authorization/user/' + id, {}, {
                headers: {
                    'Content-Type': 'application/json'
                }
            });

            return {
                status: true,
                data: response.data,
                message: ''
            };
        }
        catch (error) {
            return {
                status: false,
                message: error.response.data
            };
        }
    }

    async getAuthenticationInfoAsync() {
        return this.authenticationInfo;
    }

    getAuthenticationInfo() {
        return this.authenticationInfo;
    }
}

//https://github.com/bezkoder/react-redux-jwt-auth/blob/master/src/services/auth.service.js
const authService = new AuthService();

export default authService;