export class ForumService {
    constructor () {
        this.groupsToTest = [
            {
                groupName: "Group 1",
                groupItems: [
                    {
                        itemName: "Name 1",
                        description: "Description 1...",
                        author: "Author 1"
                    },
                    {
                        itemName: "Name 2",
                        author: "Author 2"
                    }
                ]
            },
            {
                groupName: "Group 2",
                groupItems: [
                    {
                        itemName: "Name 1",
                        author: "Author 1"
                    },
                    {
                        itemName: "Name 2",
                        author: "Author 2"
                    },
                    {
                        itemName: "Name 3",
                        author: "Author 3"
                    },
                    {
                        itemName: "Name 4",
                        author: "Author 4"
                    },
                    {
                        itemName: "Name 5",
                        author: "Author 5"
                    }
                ]
            },
            { 
                groupName: "Group 3",
                groupItems: []
            },
            {
                groupName: "Group 4",
                groupItems: [
                    {
                        itemName: "Name 1",
                        author: "Author 1"
                    },
                    {
                        itemName: "Name 2",
                        author: "Author 2"
                    },
                    {
                        itemName: "Name 3",
                        author: "Author 3"
                    }
                ]
            },
            {
                groupName: "Group 5",
                groupItems: [
                    {
                        itemName: "Name 1",
                        author: "Author 1"
                    },
                    {
                        itemName: "Name 2",
                        author: "Author 2"
                    },
                    {
                        itemName: "Name 3",
                        author: "Author 3"
                    },
                    {
                        itemName: "Name 4",
                        author: "Author 4"
                    },
                    {
                        itemName: "Name 5",
                        author: "Author 5"
                    }
                ]
            }
        ];

        this.groupItemToTest = {
            itemName: "Name 1",
            content: "Here is some content of the group item.",
            author: 
            {
                name: "User 1"
            },
            comments: [
                {
                    author: 
                    {
                        srt: "",
                        name: "User 1"
                    },
                    comment: "Some comment 1"
                },
                {
                    author:
                    {
                        srt: "",
                        name: "User 2"
                    },
                    comment: ".............................. .............................. .............................. ..................................... ....................................... ........................................ ........................................ .............................. ..................................... ....................................... ........................................ ........................................ .............................. ..................................... ....................................... ........................................ ........................................"
                },
                {
                    author:
                    {
                        srt: "",
                        name: "User 3"
                    },
                    comment: "Some comment 3"
                }
            ]
        };

        this.authorToTest = {

        };
    }
    
    async getGroups () {
        return this.groupsToTest;
    }

    async getGroupById(id) {
        return this.groupsToTest.find(g => g.groupName === id)
    }

    async getGroupItemById(id) {
        return this.groupItemToTest;
    }

    async getAuthor(){
        return this.authorToTest;
    }
}

const forumService = new ForumService();

export default forumService;