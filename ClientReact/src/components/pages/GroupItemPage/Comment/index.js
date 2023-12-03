import React from 'react';
import { useState, useEffect } from "react";
import { useParams } from "react-router-dom"
import {Link} from 'react-router-dom'

import forumService from '../../../../services/ForumService';

import './Comment.css';

function Comment(props) {
    const CommentReactionEnum = {
        NoReaction: 0,
        Like: 1,
        Dislike: 2,
    }

    const params = useParams();
    const [reactions, setReactions] = useState({});

    useEffect(() => {
        async function loadReactions() {
            const result = await forumService.getCommentReactions(props.comment.id);
            if (result.status) {
                setReactions(result.data);
            }
            else {
                alert(result.message);
            }
        }

        loadReactions();
    }, [params])

    const commentLikeClick = async function () {
        if (reactions.userReactionId === CommentReactionEnum.Like) {
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
        if (reactions.userReactionId === CommentReactionEnum.Dislike) {
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
                <img src="/img/anon.png" />
            </div>
            <div className="comment_body">
                <div className="comment_title">
                    <div className="comment_autor">
                        <Link to={"/user/" + props.comment.authorId}>
                            {
                                props.comment.authorId
                            }
                        </Link>
                           
                    </div>
                    <div className="comment_date">
                        <span >
                            {
                                props.comment.createdOn
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