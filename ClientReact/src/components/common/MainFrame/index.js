import "./MainFrame.css";

function MainFrame(props) {
    return (
        <div className="mainFrame_container">
            <h2 className="mainFrame_name" title={props.title ?? props.name}>{props.name}</h2>
            <div className="mainFrame_content">
                {
                    props.children
                }
            </div>
        </div>
    );
}

export default MainFrame;