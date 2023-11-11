import { React, useState } from "react";
import { useParams } from "react-router-dom"
import { useTranslation, withTranslation } from 'react-i18next';
import { Trans } from 'react-i18next/icu.macro';

import MainFrame from '../../common/MainFrame';

import './AddNewGroupItemPage.css';

function AddNewGroupItemPage() {
    const params = useParams();
    const { t, i18n } = useTranslation();
    const [isDataFilled, setIsDataFilled] = useState(false);
    
    function handleDataChange() {
        const itemNameText = document.getElementsByClassName("addNewGroupItemPage_itemName")[0].value;
        if (itemNameText == "") {
            setIsDataFilled(false);
            return;
        }

        const contentText = document.getElementsByClassName("addNewGroupItemPage_itemContent")[0].value;
        if (contentText == "") {
            setIsDataFilled(false);
            return;
        }

        setIsDataFilled(true);
    }

    return (
        <div className="addNewGroupItemPage_container">
            <MainFrame name={t("Add new item to ") + params.id}>
                <div className="addNewGroupItemPage_content">
                    <label>
                        <b>
                            <Trans>Item name</Trans>
                        </b>
                    </label>
                    <input className="addNewGroupItemPage_itemName" type="text" placeholder={t("Enter item name")} onChange={handleDataChange}></input>
                    <label>
                        <b>
                            <Trans>Content</Trans>
                        </b>
                    </label>
                    <textarea className="addNewGroupItemPage_itemContent" type="text" placeholder={t("Enter a content")} onChange={handleDataChange}></textarea>

                    <div className="addNewGroupItemPage_buttonContainer">
                        {
                            isDataFilled ? (
                                <button className="root_button">
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

export default withTranslation("translation") (AddNewGroupItemPage);