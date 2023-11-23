import { useEffect, useState } from 'react';
import { useParams } from "react-router-dom"
import SearchItem from "./SearchItem";
import forumService from '../../../services/ForumService';

import './SearchPage.css';

function SearchPage() {
    const params = useParams();
    const [searchItems, setSearchItems] = useState([]);

    useEffect(() => {
        async function loadSearchItems() {
            const searchItems = await forumService.getSearchItemsByCriteriaAsync(params.text);
            setSearchItems(searchItems)
        }

        loadSearchItems();
    }, [params])
    
    return (
        <div className="searchPage_content">
            {
                 searchItems.map(searchItem => <SearchItem item={searchItem} /> )
            }
        </div>)
}

export default SearchPage;