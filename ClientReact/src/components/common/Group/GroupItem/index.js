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
                        <Link to="/forumItem">
                            {
                                this.props.groupItem.itemName
                            }
                        </Link >
                    </h4>
                    <p>
                        {
                            this.props.groupItem.description
                        }
                    </p>
                </div>
                <div className="groupItemCommentsCount">100</div>
                <div className="groupItemAuthor">
                    <Link to={"/user/" + this.props.groupItem.author}>
                        {
                            this.props.groupItem.author
                        }
                    </Link>
                </div>
            </div>
        );
    }
}

export default GroupItem;