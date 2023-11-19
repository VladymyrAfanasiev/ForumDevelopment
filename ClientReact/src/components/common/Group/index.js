import { Link } from "react-router-dom";
import { useTranslation } from 'react-i18next';

import './Group.css'

import GroupItem from './GroupItem';

function Group(props) {
    const { t, i18n } = useTranslation();

    return (
        <div className="mainGroup">
            <h2 className="mainGroupName">
                <Link to={'/group/' + props.group.id}>
                    {
                        props.group.name
                    }
                </Link>
                <Link title={t("Add new group item")} className="addNewGroup_a_button" to={'/addNewGroupItem/' + props.group.id}></Link>
            </h2>
            <div className="groupItemsContainer">
                <ol>
                    {
                        props.simpleView ?
                            props.group.posts.slice(0, 3).map(post => <li><GroupItem post={post} /></li>) :
                            props.group.posts.map(post => <li><GroupItem post={post} /></li>)
                    }
                    {
                        props.simpleView && props.group.posts.length > 3 ? (
                            <li className="groupItemsContainer_openMove">
                                <Link className="root_a_button" to={'/group/' + props.group.id}>
                                    {
                                        t("Open more items...")
                                    }
                                </Link>
                            </li>
                        ) : (
                            <li />
                        ) 
                    }
                </ol>
            </div>
        </div>
    );
}

export default Group;