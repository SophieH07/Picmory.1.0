import React, { useState } from "react";
import axios from 'axios';
import '../Modal.css';
import '../Common.css';
import { useHistory, useLocation } from 'react-router-dom';

const FolderModal = props => {

    const [newFolderName, setNewFolderName] = useState('');
    const [folderNameError, setFolderNameError] = useState(false);
    const [access, setAccess] = useState(0);
    const [loading, setLoading] = useState(false);
    const [editError, setEditError] = useState('');

    const showHideClassName = props.show ? "modal display-block" : "modal display-none";

    const history = useHistory();
    const location = useLocation();


    const checkFolderNameNotEmpty = e => {
        if (e.target.value !== '') {
            setNewFolderName(e.target.value);
            setFolderNameError(false);
        } else {
            setFolderNameError(true);
        }
    }

    const handleSubmit = async (e) => {
        setLoading(true);
        try {
            const data = {
                Name: newFolderName,
                Access: access
            }

            const result = await axios.post('/folder/changefolderdata', data)
            console.log(result.data);
            setEditError('');
            setLoading(false);
            const referrer = location.state ? location.state.from : `/user/${localStorage.getItem('username')}`;
            history.push(referrer);

        } catch (e) {
            setEditError(e.response.data);
            setLoading(false);
            console.log(e);
        }
    }

    const deleteFolder = async (e) => {
        setLoading(true);
        try {
            const folderName = props.folder.folderName

            const result = await axios.post('/folder/deletefolder', JSON.stringify(folderName))
            console.log(result);
            setLoading(false);
            const referrer = location.state ? location.state.from : `/user/${localStorage.getItem('username')}`;
            history.push(referrer);

        } catch (e) {
            setLoading(false);
            console.log(e);
        }
    }

    return (
        <div className={showHideClassName}>
            <div className="modal-main" ref={props.reference}>
                <h2>Edit folder</h2>
                {loading ? <p>Loading...</p> : ''}
                {editError !== '' ? <p>{editError}</p> : ''}
                <form className="input-fields">
                    {folderNameError ? <p className="warning">Folder name cannot be empty</p> : ''}
                    <div>
                        <input name='foldername' placeholder={props.folder.folderName} onChange={(e) => { checkFolderNameNotEmpty(e) }} />
                    </div>
                    <div>
                        <select onChange={(e) => { setAccess(e.target.value) }}>
                            <option value='0'>Public</option>
                            <option value='1'>Public only for followers</option>
                            <option value='2'>Private</option>
                        </select>
                    </div>
                </form>
                <button type="submit" onClick={(e) => handleSubmit(e)}>Create</button>
                <button onClick={(e) => deleteFolder(e)}>Delete</button>
            </div>
        </div>
    );
}

export default FolderModal;