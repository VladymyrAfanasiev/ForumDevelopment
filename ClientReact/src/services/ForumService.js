import axios from 'axios';

class ForumService {
    constructor () {

    }

    async getRequests() {
        try {
            const response = await axios.get('/api/group/request', {}, {
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

    async getUserRequests() {
        try {
            const response = await axios.get('/api/group/userrequest', {}, {
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

    async requestAddGroup(name, description) {
        try {
            const data = {
                Name: name,
                Description: description
            };

            const response = await axios.put('/api/group/request', data, {
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

    async approveRequest(id) {
        try {
            const response = await axios.post('/api/group/request/' + id, {} , {
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

    async getPostById(groupId, postId) {
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

    async addNewPost(groupId, postName, text) {
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

            const response = await axios.put('/api/group/' + '00000000-0000-0000-0000-000000000000' + '/post/' + postId + '/comment', data , {
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
        try {
            const response = await axios.get('/api/search/' + text, {} , {
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

    async getCommentReactions(commentId) {
        try {
            const response = await axios.get('/api/group/' + '00000000-0000-0000-0000-000000000000' + '/post/' + '00000000-0000-0000-0000-000000000000' + '/comment/' + commentId + '/reaction', {} , {
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

    async updateCommentReaction(commentId, reaction) {
        try {
            const response = await axios.post('/api/group/' + '00000000-0000-0000-0000-000000000000' + '/post/' + '00000000-0000-0000-0000-000000000000' + '/comment/' + commentId + '/reaction', reaction, {
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

    async dislikeComment(commentId) {
        try {
            const response = await axios.post('/api/group/' + '00000000-0000-0000-0000-000000000000' + '/post/' + '00000000-0000-0000-0000-000000000000' + '/comment/' + commentId + '/reaction', 2, {
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

const forumService = new ForumService();

export default forumService;