import { useEffect, useState } from 'react';
import { useParams } from "react-router-dom"

import forumService from '../../../services/ForumService';
import MainFrame from "../../common/MainFrame"
import GroupItem from "../../common/Group/GroupItem"
import './SearchPage.css';

function SearchPage() {
    const params = useParams();
    const [searchItems, setSearchItems] = useState([]);

    useEffect(() => {
        async function loadSearchItems() {
            const result = await forumService.getSearchItemsByCriteriaAsync(params.text);
            if (result.status) {
                setSearchItems(result.data)
            }
            else {
                alert("Failed to load search data")
            }
        }

        loadSearchItems();
    }, [params])
    
    return (
        <div className="searchPage_content">
            <MainFrame>
                {
                    searchItems.map(post => <GroupItem post={post} /> )
                }
            </MainFrame>
        </div>)
}

export default SearchPage;