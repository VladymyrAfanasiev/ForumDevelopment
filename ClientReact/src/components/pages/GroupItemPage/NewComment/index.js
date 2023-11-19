import React, { useState, useEffect } from 'react';
import { useTranslation, withTranslation } from 'react-i18next';
import { Trans } from 'react-i18next/icu.macro';

import "./NewComment.css";

function NewComment(params) {
    const { t, i18n } = useTranslation();
    const [isNewCommentEmpty, setIsNewCommentEmpty] = useState(true);

    function handleNewCommentChange(e) {
        setIsNewCommentEmpty(e.target.value == "" ? true : false);
    }
    
    const addNewCommentClicked = async function () {
        const text = document.getElementsByClassName("newComment_text")[0].value;
        await params.addNewCommentHandler(text);
    }

    return (
        <div className="newComment_content">
            <div className="newComment_container">
                <textarea className="newComment_text" type="text" placeholder={t("Enter a comment")} onChange={handleNewCommentChange}></textarea>
            </div>
            <div className="newComment_sendButton">
                {
                    isNewCommentEmpty ? (
                        <button className="root_button root_button_disabled">
                            <Trans>Send</Trans>
                        </button>
                    ) : (
                        <button className="root_button" onClick={addNewCommentClicked}>
                            <Trans>Send</Trans>
                        </button>
                    )
                }
            </div>
        </div>
    );
}

export default withTranslation("translation") (NewComment);