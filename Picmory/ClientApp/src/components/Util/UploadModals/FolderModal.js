import React, { useState } from "react";
import axios from 'axios';
import '../Modal.css';
import '../Common.css';
import { useHistory, useLocation } from 'react-router-dom';

const FolderModal = props => {

    const [folderName, setFolderName] = useState('');
    const [folderNameError, setFolderNameError] = useState(false);
    const [access, setAccess] = useState(0);
    const [loading, setLoading] = useState(false);
    const [uploadError, setUploadError] = useState('');

    const showHideClassName = props.show ? "modal display-block" : "modal display-none";

    const history = useHistory();
    const location = useLocation();

    const checkFolderNameNotEmpty = e => {
        if (e.target.value !== '') {
            setFolderName(e.target.value);
            setFolderNameError(false);
        } else {
            setFolderNameError(true);
        }
    }

    const handleSubmit = async (e) => {
        setLoading(true);
        try {
            const data = {
                Name: folderName,
                Access: access
            }

            const result = await axios.post('/folder/createnewfolder', data)
            console.log(result.data);
            setUploadError('');
            setLoading(false);
            const referrer = location.state ? location.state.from : `/user/${localStorage.getItem('username')}`;
            history.push(referrer);

        } catch (e) {
            setUploadError(e.response.data);
            setLoading(false);
            console.log(e);
        }
    }

    return (
        <div className={showHideClassName}>
            <div className="modal-main" ref={props.reference}>
                <h2>Create new folder</h2>
                {loading ? <p>Loading...</p> : ''}
                {uploadError !== '' ? <p>{uploadError}</p> : ''}
                <form className="input-fields">
                    {folderNameError ? <p className="warning">Folder name cannot be empty</p> : ''}
                    <div>
                        <input name='foldername' placeholder='Folder name' onChange={(e) => { checkFolderNameNotEmpty(e) }} />
                    </div>
                    <div>
                        <select onChange={(e) => { setAccess(e.target.value) }}>
                            <option value='0'>Public</option>
                            <option value='1'>Public only for followers</option>
                            <option value='2'>Private</option>
                        </select>
                    </div>
                    <button type="submit" onClick={(e) => handleSubmit(e)}>Create</button>
                </form>
            </div>
        </div>
    );

}

export default FolderModal;