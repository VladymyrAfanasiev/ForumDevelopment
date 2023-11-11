import React from 'react';
import {Link} from 'react-router-dom'

import './Comment.css';

class Comment extends React.Component {
    render() {
        return (
            <div className="comment_content">
                <div className="comment_autor_img">
                    <img src="/img/anon.png" />
                </div>
                <div className="comment_body">
                    <div className="comment_title">
                        <div className="comment_autor">
                            <Link to={"/user/" + this.props.comment.author.name}>
                                {
                                    this.props.comment.author.name
                                }
                            </Link>
                           
                        </div>
                        <div className="comment_date">
                            <span >
                                {
                                    new Date().toJSON().slice(0,10)
                                }
                            </span>
                        </div>
                    </div>
                    <div className="comment_text">
                            <span>
                                {
                                    this.props.comment.comment
                                }
                            </span>
                    </div>
                    <div className="comment_votes">
                        <button className="comment_votes_button">
                            <img className="comment_vote_button_img_like" src="/img/thumb.svg" />
                            <p2>1</p2>
                        </button>
                        <button className="comment_votes_button">
                            <img className="comment_vote_button_img_dislike" src="/img/thumb.svg" />
                            <p2>0</p2>
                        </button>
                    </div>
                </div>
            </div>
        );
    }
}

export default Comment;