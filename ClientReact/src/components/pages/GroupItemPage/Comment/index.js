import { React, useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import { Link } from 'react-router-dom';
import { useNavigate } from "react-router-dom";
import dateFormat from "dateformat";

import authService from '../../../../services/AuthService';
import forumService from '../../../../services/ForumService';
import publicVaultService from '../../../../services/PublicVaultService';

import './Comment.css';

function Comment(props) {
    const CommentReactionEnum = {
        NoReaction: 0,
        Like: 1,
        Dislike: 2,
    }

    const params = useParams();
    const navigate = useNavigate();
    const [reactions, setReactions] = useState({});
    const [authorName, setAuthorName] = useState('');
    const [userImageUrl, setUserImageUrl] = useState('/img/anon.png');

    useEffect(() => {
        async function loadReactions() {
            let result = await forumService.getCommentReactions(props.comment.id);
            if (result.status) {
                setReactions(result.data);
            }

            result = await authService.getUserInfo(props.comment.authorId);
            if (result.status) {
                setAuthorName(result.data.name);
            }
            else {
                setAuthorName(props.comment.authorId);
            }

            result = await publicVaultService.getUserImageUrl(props.comment.authorId);
            if (result.status) {
                setUserImageUrl(result.data);
            }
        }

        loadReactions();
    }, [params])

    const commentLikeClick = async function () {
        const authenticationInfo = await authService.getAuthenticationInfoAsync();
        if (!authenticationInfo.isAuthenticated) {
            navigate('/login');

            return;
        }

        const result = await forumService.updateCommentReaction(props.comment.id, CommentReactionEnum.Like);
        if (result.status) {
            setReactions(result.data);
        }
        else {
            alert(result.message);
        }
    }

    const commentDislikeClick = async function () {
        const authenticationInfo = await authService.getAuthenticationInfoAsync();
        if (!authenticationInfo.isAuthenticated) {
            navigate('/login');

            return;
        }

        const result = await forumService.updateCommentReaction(props.comment.id, CommentReactionEnum.Dislike);
        if (result.status) {
            setReactions(result.data);
        }
        else {
            alert(result.message);
        }
    }

    return (
        <div className="comment_content">
            <div className="comment_autor_img">
                <img src={userImageUrl} />
            </div>
            <div className="comment_body">
                <div className="comment_title">
                    <div className="comment_autor">
                        <Link to={"/user/" + props.comment.authorId}>
                            {
                                authorName
                            }
                        </Link>
                           
                    </div>
                    <div className="comment_date">
                        <span >
                            {
                                dateFormat(props.comment.createdOn, "dddd, mmmm dS, yyyy, h:MM:ss TT")
                            }
                        </span>
                    </div>
                </div>
                <div className="comment_text">
                        <span>
                            {
                                props.comment.text
                            }
                        </span>
                </div>
                <div className="comment_votes">
                    <button className="comment_votes_button" onClick={commentLikeClick}>
                        <img className="comment_vote_button_img_like" src="/img/thumb.svg" />
                        <p2>
                            {
                                reactions.likeCount
                            }
                        </p2>
                    </button>
                    <button className="comment_votes_button" onClick={commentDislikeClick}>
                        <img className="comment_vote_button_img_dislike" src="/img/thumb.svg" />
                        <p2>
                            {
                                reactions.dislikeCount
                            }
                        </p2>
                    </button>
                </div>
            </div>
        </div>
    );
}

export default Comment;