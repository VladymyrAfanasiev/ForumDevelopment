import React from "react";
import { Link } from 'react-router-dom'
import { useNavigate } from "react-router-dom";
import { useTranslation, withTranslation } from 'react-i18next';
import { useState, useEffect } from "react";
import { useParams } from "react-router-dom"

import MaineFrame from '../../common/MainFrame';
import NewComment from './NewComment';
import Comment from './Comment';

import authService from '../../../services/AuthService';
import forumService from '../../../services/ForumService';

import './GroupItemPage.css';

function GroupItemPage() {
    const params = useParams();
    const navigate = useNavigate();
    const { t, i18n } = useTranslation();
    const [post, setPost] = useState({});
    const [comments, setComments] = useState([]);

    useEffect(() => {
        async function loadPost() {
            const result = await forumService.getGroupItemById(params.groupId, params.postId);
            if (result.status) {
                setPost(result.data);
                setComments(result.data.comments);
            }
            else {
                alert(result.message);
            }
        }

        loadPost();
    }, [params])

    const addNewCommentHandler = async function (text) {
        const authenticationInfo = await authService.getAuthenticationInfoAsync();
        if (!authenticationInfo.isAuthenticated) {
            navigate('/login');

            return false;
        }

        const result = await forumService.addComment(params.postId, text);
        if (result.status) {
            setComments([...post.comments, result.data]);
            post.comments.push(result.data);

            return true;
        }

        alert(result.message);

        return false;
    }

    return (
        <div className="groupItemPage_content">
            <MaineFrame name={post.name}>
                <div className="groupItemPage_item">
                    <p className="groupItemPage_text">
                        {
                            post.text
                        }
                    </p>
                    <div className="groupItemPage_author">
                        <p2>{t("Author")}</p2>
                        <Link to={"/user/" + post.authorId}>
                            {
                                post.authorId
                            }
                        </Link>
                    </div>
                </div>
            </MaineFrame>
            <MaineFrame name={t("Comments")}>
                <div className="groupItemPage_comments">
                    {
                        comments.map(item => <Comment comment={item} />)
                    }
                </div>
            </MaineFrame>
            <MaineFrame name={t("Add Commnet")}>
                <div className="groupItemPage_addNewComment">
                    {
                        <NewComment postId={post.postId} addNewCommentHandler={addNewCommentHandler} />
                    }
                </div>
            </MaineFrame>
        </div>
    );
}

export default withTranslation('translation') (GroupItemPage);
