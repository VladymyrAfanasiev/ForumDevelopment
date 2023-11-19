import { useEffect, useState } from 'react';
import './MainPage.css';

import MainFrame from "../../common/MainFrame";
import LoadingAnimation from "../../common/LoadingAnimation";
import Group from '../../common/Group';

import forumService from '../../../services/ForumService';

function MainPage() {
    const [groupsLoading, setGroupsLoading] = useState(false);
    const [groups, setGroups] = useState([]);

    useEffect(() => {
        async function loadGroups() {
            setGroupsLoading(true);

            const result = await forumService.getGroups();
            if (result.status) {
                setGroups(result.data)
            }
            else {
                alert(result.message);
            }

            setGroupsLoading(false);
        }

        loadGroups();
    }, [])

    return (
        <div className="main_content">
            <div className="main_groups">
                {groupsLoading ? (
                    <LoadingAnimation />
                ) : (
                    groups.map(group => <Group group={group} simpleView={true} />)
                )}
            </div>

            <div className="main_advertising">
                <MainFrame name="Some module 1">
                    <div style={{ padding: "10px" }}>Content...</div>
                </MainFrame>
                <MainFrame name="Some module 2">
                    <div style={{ padding: "10px" }}>Content...</div>
                </MainFrame>
            </div>
        </div>
    );
}

export default MainPage;
