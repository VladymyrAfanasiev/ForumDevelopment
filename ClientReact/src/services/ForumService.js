import axios from 'axios';

class ForumService {
    constructor () {

    }

    async getGroups() {
        try {
            const response = await axios.get('/api/group', {}, {
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

    async getGroupById(id) {
        try {
            const response = await axios.get('/api/group/' + id, {}, {
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

    async getGroupItemById(groupId, postId) {
        try {
            const response = await axios.get('/api/group/' + groupId + '/post/' + postId, {}, {
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

    async addNewGroup(name, description) {
        try {
            const data = {
                Name: name,
                Description: description
            };

            const response = await axios.put('/api/group', data, {
                headers: {
                    'Content-Type': 'application/json'
                }
            });

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

    async addNewGroupItem(groupId, postName, text) {
        try {
            const data = {
                name: postName,
                text: text
            };

            const response = await axios.put('/api/group/' + groupId + '/post', data, {
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

    async addComment(postId, text) {
        try {
            const data = {
                text: text
            };

            const response = await axios.put('/api/group/' + -1 + '/post/' + postId + '/comment', data , {
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
    
    async getSearchItemsByCriteriaAsync(text) {
        return [
            {
                name: "search result 1"
            },
            {
                name: "search result 2"
            },
            {
                name: "search result 3"
            },
            {
                name: "search result 4"
            }
        ];
    }
}

const forumService = new ForumService();

export default forumService;