import { useEffect, useState } from 'react';
import { useParams } from "react-router-dom"
import { useTranslation, withTranslation } from 'react-i18next';
import { Trans, Plural, Select } from 'react-i18next/icu.macro';

import forumService from '../../../services/ForumService';
import MainFrame from "../../common/MainFrame"
import Post from "../../common/Post"
import './SearchPage.css';

function SearchPage() {
    const params = useParams();
    const { t, i18n } = useTranslation();
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
            <MainFrame name={t("Posts")}>
                {
                    searchItems.map(post => <Post post={post} /> )
                }
            </MainFrame>
        </div>)
}

export default SearchPage;