import { useState, useEffect } from "react";
import { useParams } from "react-router-dom"

import "./GroupPage.css";

import Group from "../../common/Group";

import forumService from "../../../services/ForumService";

function GroupPage() {
    const params = useParams();
    const [group, setGroup] = useState(undefined);

    useEffect(() => {
        async function loadGroupItems() {
            const group = await forumService.getGroupById(params.id);
            setGroup(group);
        }

        loadGroupItems();
    }, [params])

    return (
        <div className="groupPage_content">
            {
                group !== undefined ? <Group group={group} simpleView={false} /> : <div></div>
            }
        </div>
    );
}

export default GroupPage;
