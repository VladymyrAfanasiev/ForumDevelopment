import React from 'react';
import { Link } from "react-router-dom";

import './GroupItem.css'

class GroupItem extends React.Component {
    render() {
        return (
            <div className="groupItem">
                <img width={40} height={40} src="/img/forum.svg" alt="" />
                <div className="groupItemInfo">
                    <h4>
                        <Link to={"/group/" + -1 + "/post/" + this.props.post.id}>
                            {
                                this.props.post.name
                            }
                        </Link >
                    </h4>
                    <p>
                        {
                            this.props.post.description
                        }
                    </p>
                </div>
                <div className="groupItemCommentsCount">{this.props.post.commentsCount}</div>
                <div className="groupItemAuthor">
                    <Link to={"/user/" + this.props.post.authorId}>
                        {
                            this.props.post.authorId
                        }
                    </Link>
                </div>
            </div>
        );
    }
}

export default GroupItem;