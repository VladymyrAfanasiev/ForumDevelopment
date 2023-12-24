import { React, useState } from "react";
import { useNavigate } from "react-router-dom";
import { useParams } from "react-router-dom"
import { useTranslation, withTranslation } from 'react-i18next';
import { Trans } from 'react-i18next/icu.macro';

import MainFrame from '../../common/MainFrame';
import forumService from '../../../services/ForumService';

import './AddNewPostPage.css';

function AddNewPostPage() {
    const navigate = useNavigate();
    const params = useParams();
    const { t, i18n } = useTranslation();
    const [isDataFilled, setIsDataFilled] = useState(false);
    
    function handleDataChange() {
        const itemNameText = document.getElementsByClassName("addNewPostPage_itemName")[0].value;
        if (itemNameText == "") {
            setIsDataFilled(false);
            return;
        }

        const contentText = document.getElementsByClassName("addNewPostPage_itemContent")[0].value;
        if (contentText == "") {
            setIsDataFilled(false);
            return;
        }

        setIsDataFilled(true);
    }

    const sendClick = async function () {
        const newPostName = document.getElementsByClassName("addNewPostPage_itemName")[0].value;
        const newPostText = document.getElementsByClassName("addNewPostPage_itemContent")[0].value;

        const result = await forumService.addNewPost(params.id, newPostName, newPostText);
        if (result.status) {
            navigate('/group/' + '00000000-0000-0000-0000-000000000000' + '/post/' + result.data.id);
        }
        else {
            alert(result.message);
        }
    }

    return (
        <div className="addNewPostPage_container">
            <MainFrame name={t("Add new item to ") + params.id}>
                <div className="addNewPostPage_content">
                    <label>
                        <b>
                            <Trans>Item name</Trans>
                        </b>
                    </label>
                    <input className="addNewPostPage_itemName" type="text" placeholder={t("Enter item name")} onChange={handleDataChange}></input>
                    <label>
                        <b>
                            <Trans>Content</Trans>
                        </b>
                    </label>
                    <textarea className="addNewPostPage_itemContent" type="text" placeholder={t("Enter a content")} onChange={handleDataChange}></textarea>

                    <div className="addNewPostPage_buttonContainer">
                        {
                            isDataFilled ? (
                                <button className="root_button" onClick={sendClick}>
                                    <Trans>Send</Trans>
                                </button>
                            ) : (
                                <button className="root_button root_button_disabled">
                                    <Trans>Send</Trans>
                                </button>
                            )
                        }
                        
                    </div>
                </div>
            </MainFrame>
        </div>
    );
}

export default withTranslation("translation") (AddNewPostPage);