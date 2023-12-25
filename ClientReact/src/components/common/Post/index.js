import { React, useState, useEffect } from "react";
import { Link } from "react-router-dom";
import { Trans } from 'react-i18next/icu.macro';

import authService from '../../../services/AuthService';

import './Post.css'

function Post(props) {
    const [authorName, setAuthorName] = useState('');

    useEffect(() => {
        async function loadAuthorName() {
            const result = await authService.getUserInfo(props.post.authorId);
            if (result.status) {
                setAuthorName(result.data.name);
            }
            else {
                alert(result.message);
            }
        }

        loadAuthorName();
    }, [props])

    return (
        <div className="post">
            <img width={40} height={40} src="/img/forum.svg" alt="" />
            <div className="postInfo">
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
            <div className="postCommentsCount">
                <p>{props.post.commentsCount}</p>
                <p>
                    <Trans>Comments</Trans>
                </p>
            </div>
            <div className="postAuthor">
                <Link to={"/user/" + props.post.authorId}>
                    <p>{authorName}</p>
                </Link>
                <p>
                    <Trans>Author</Trans>
                </p>
            </div>
        </div>
    );
}

export default Post;