import { useEffect, useState } from 'react';
import './MainPage.css';

import MainFrame from "../../common/MainFrame";
import Group from '../../common/Group';

import forumService from '../../../services/ForumService';

function MainPage() {
    const initialValue = [];

    const [groups, setGroups] = useState(initialValue);

    useEffect(() => {
        async function loadGroups() {
            const loadedGroups = await forumService.getGroups();
            setGroups(loadedGroups)
        }

        loadGroups();
    }, [])

    return (
        <div className="main_content">
            <div className="main_groups">
                {
                    groups.map(group => <Group group={group} simpleView={true} /> )
                }
            </div>
            <div className="main_advertising">
                <MainFrame name="Some module 1">
                    <div style={{padding: "10px"}}>Content...</div>
                </MainFrame>
                <MainFrame name="Some module 2">
                    <div style={{padding: "10px"}}>Content...</div>
                </MainFrame>
            </div>
        </div>
    );
}

export default MainPage;
