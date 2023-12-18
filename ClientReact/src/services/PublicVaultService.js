import axios from 'axios';

class PublicVaultService {
    constructor () {

    }

    async uploadUserImage(file) {
        try {
            let data = new FormData();
            data.append('UserImage', file);

            const response = await axios.post('api/publicvault/userimage', data, {
                headers: {
                    'Content-Type': 'multipart/form-data'
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

    async getUserImageUrl(userId) {
        try {
            let url = '/api/publicvault/userimage';
            if (userId) {
                url += '/' + userId;
            }

            const response = await axios.get(url, {} , {
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
}

const publicVaultService = new PublicVaultService();

export default publicVaultService;