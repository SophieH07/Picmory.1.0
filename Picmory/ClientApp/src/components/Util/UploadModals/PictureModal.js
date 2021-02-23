import React, { useState } from "react";
import axios from 'axios';
import '../Modal.css';
import '../Common.css';
import { useHistory, useLocation } from 'react-router-dom';

const PictureModal = props => {

    const [picture, setPicture] = useState();
    const [description, setDescription] = useState('');
    const [access, setAccess] = useState(0);
    const [folderName, setFolderName] = useState('');
    const [folderNameError, setFolderNameError] = useState(false);

    const showHideClassName = props.show ? "modal display-block" : "modal display-none";

    const history = useHistory();
    const location = useLocation();

    const checkFolderNameNotEmpty = e => {
        if (folderName !== '') {
            setFolderName(e.target.value);
            setFolderNameError(false);
        } else {
            setFolderNameError(true);
        }
    }

    const handleSubmit = async (e) => {
        try {
            const data = {
                Photo: picture,
                Description: description,
                Access: access,
                FolderName: folderName
            }

            const result = await axios.post('/picture/uploadpicture', data)
            console.log(result.data);
            const referrer = location.state ? location.state.from : `/user/${localStorage.getItem('username')}`;
            history.push(referrer);

        } catch (e) {
            console.log(e);
        }
    }

    return (
        <div className={showHideClassName}>
            <div className="modal-main" ref={props.reference}>
                <h2>Upload picture</h2>
                <form className="input-fields">
                    <input name='picture' type="file" onChange={(e) => { setPicture(e.target.value) }} />
                    <input name='description' placeholder="Description" onChange={(e) => { setDescription(e.target.value) }} />
                    {folderNameError ? <p className="warning">Folder name cannot be empty</p> : ''}
                    <input name='foldername' placeholder='Folder name' onChange={(e) => { checkFolderNameNotEmpty(e) }} />
                    <select onChange={(e) => { setAccess(e.target.value) }}>
                        <option value='0'>Public</option>
                        <option value='1'>Public only for followers</option>
                        <option value='2'>Private</option>
                    </select>
                </form>
                <button type="submit" onClick={(e) => handleSubmit(e)}>Create</button>
            </div>
        </div>
    );

}

export default PictureModal;