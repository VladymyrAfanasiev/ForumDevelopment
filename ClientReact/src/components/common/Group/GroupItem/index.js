import { React, useState, useEffect } from "react";
import { useParams } from "react-router-dom"
import { Link } from "react-router-dom";

import authService from '../../../../services/AuthService';

import './GroupItem.css'

function GroupItem(props) {
    const params = useParams();
    const [authorName, setAuthorName] = useState('');

    useEffect(() => {
        async function loadAuthorName() {
            const result = await authService.getUserInfo(props.post.authorId);
            if (result.status) {
                setAuthorName(result.data.userName);
            }
            else {
                alert(result.message);
            }
        }

        loadAuthorName();
    }, [props])

    return (
        <div className="groupItem">
            <img width={40} height={40} src="/img/forum.svg" alt="" />
            <div className="groupItemInfo">
                <h4>
                    <Link to={"/group/" + '00000000-0000-0000-0000-000000000000' + "/post/" + props.post.id}>
                        {
                            props.post.name
                        }
                    </Link >
                </h4>
                <p>
                    {
                        props.post.description
                    }
                </p>
            </div>
            <div className="groupItemCommentsCount">{props.post.commentsCount}</div>
            <div className="groupItemAuthor">
                <Link to={"/user/" + props.post.authorId}>
                    {
                        authorName
                    }
                </Link>
            </div>
        </div>
    );
}

export default GroupItem;