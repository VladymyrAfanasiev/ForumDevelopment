import { React, useState } from "react";
import { useTranslation, withTranslation } from 'react-i18next';
import { Trans } from 'react-i18next/icu.macro';

import MainFrame from '../../common/MainFrame';

import './RequestCreateNewGroupPage.css';

function RequestCreateNewGroupPage() {
    const { t, i18n } = useTranslation();
    const [isDataFilled, setIsDataFilled] = useState(false);
    
    function handleDataChange() {
        const itemNameText = document.getElementById("requestCreateNewGroupPage_name").value;
        if (itemNameText == "") {
            setIsDataFilled(false);
            return;
        }

        setIsDataFilled(true);
    }

    return (
        <div className="requestCreateNewGroupPage_container">
            <MainFrame name={t("Request to create a new group")}>
                <div className="requestCreateNewGroupPage_content">
                    <label>
                        <b>
                            <Trans>Name</Trans>
                        </b>
                    </label>
                    <input id="requestCreateNewGroupPage_name" type="text" placeholder={t("Enter group name")} onChange={handleDataChange}></input>
                    <label>
                        <b>
                            <Trans>Description</Trans>
                        </b>
                    </label>
                    <input type="text" placeholder={t("Enter group description")} onChange={handleDataChange}></input>


                    <div className="requestCreateNewGroupPage_buttonContainer">
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

export default withTranslation("translation") (RequestCreateNewGroupPage);