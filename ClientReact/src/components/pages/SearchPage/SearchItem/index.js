import MaineFrame from '../../../common/MainFrame';

import './SearchItem.css';

function SearchItem(props) {
    return (
        <div className='searchItem_root'>
            <MaineFrame>
                <div className='searchItem_content'>
                    {
                        props.item.name
                    }
                </div>
            </MaineFrame>
        </div>
    );
}

export default SearchItem;