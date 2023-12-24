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

import './PostPage.css';

function PostPage() {
    const params = useParams();
    const navigate = useNavigate();
    const { t, i18n } = useTranslation();
    const [post, setPost] = useState({});
    const [comments, setComments] = useState([]);
    const [authorName, setAuthorName] = useState('');

    useEffect(() => {
        async function loadPost() {
            const result = await forumService.getPostById(params.groupId, params.postId);
            if (result.status !== true) {
                alert(result.message);
                return;
            }

            setPost(result.data);
            setComments(result.data.comments);

            const authorResult = await authService.getUserInfo(result.data.authorId);
            if (authorResult.status) {
                setAuthorName(authorResult.data.name);
            }
            else {
                setAuthorName(result.data.authorId);
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
        <div className="postPage_content">
            <MaineFrame name={post.name}>
                <div className="postPage_item">
                    <p className="postPage_text">
                        {
                            post.text
                        }
                    </p>
                    <div className="postPage_author">
                        <p2>{t("Author")}</p2>
                        <Link to={"/user/" + post.authorId}>
                            {
                                authorName
                            }
                        </Link>
                    </div>
                </div>
            </MaineFrame>
            <MaineFrame name={t("Comments")}>
                <div className="postPage_comments">
                    {
                        comments.map(item => <Comment comment={item} />)
                    }
                </div>
            </MaineFrame>
            <MaineFrame name={t("Add Commnet")}>
                <div className="postPage_addNewComment">
                    {
                        <NewComment postId={post.postId} addNewCommentHandler={addNewCommentHandler} />
                    }
                </div>
            </MaineFrame>
        </div>
    );
}

export default withTranslation('translation') (PostPage);
