import axios from 'axios';

class AuthService {
    constructor() {
        this.authenticationInfo = {
            isAuthenticated: false,
            user: {
                id: -1000,
                name: ""
            }
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
                user: {
                    id: response.data.id,
                    userName: response.data.userName,
                    email: response.data.email
                }
            };

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
            const data = {
                Email: email,
                Password: password
            };

            const response = await axios.post('/api/authorization/login', data, {
                headers: {
                    'Content-Type': 'application/json'
                }
            });

            this.authenticationInfo = {
                isAuthenticated: true,
                user: {
                    id: response.data.id,
                    userName: response.data.userName,
                    email: response.data.email
                }
            };

            // TODO: redux
            axios.defaults.headers.common['Authorization'] = 'Bearer ' + response.data.token;

            return {
                status: true,
                message: ''
            };
        }
        catch (error) {
            this.authenticationInfo = {
                isAuthenticated: false,
                user: {
                    id: -1000,
                    name: ''
                }
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
            isAuthenticated: false,
            user: {
                id: -1000,
                name: ""
            }
        };

        return {
            status: true,
            message: ''
        };
    }

    async getUserInfo(id) {
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

const authService = new AuthService();

export default authService;