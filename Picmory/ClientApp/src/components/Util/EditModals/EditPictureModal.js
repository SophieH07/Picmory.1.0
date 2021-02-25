import React, { useState } from "react";
import axios from 'axios';
import '../Modal.css';
import '../Common.css';
import { useHistory, useLocation } from 'react-router-dom';

const EditPictureModal = props => {

    const [description, setDescription] = useState('');
    const [access, setAccess] = useState(0);
    const [folderName, setFolderName] = useState('');
    const [folderNameError, setFolderNameError] = useState(false);

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
        try {
            const data = {
                Description: description,
                Access: access,
                FolderName: folderName
            }

            const result = await axios.post('/picture/editpicture', data)
            console.log(result.data);
            const referrer = location.state ? location.state.from : `/user/${localStorage.getItem('username')}`;
            history.push(referrer);

        } catch (e) {
            console.log(e);
        }
    }

    const deletePicture = async (e) => {
        try {
            const id = props.picture.id;
            const result = await axios.post('picture/deletepicture', id)
            console.log(result);
            const referrer = location.state ? location.state.from : `/user/${localStorage.getItem('username')}`;
            history.push(referrer);
        } catch (e) {
            console.log(e);
        }
    }

    return (
        <div className={showHideClassName}>
            <div className="modal-main" ref={props.reference}>
                <h2>Edit picture</h2>
                <form className="input-fields">
                    <div>
                        <img className="picture" src={`https://localhost:44386/picture/picture/${props.picture.id}`} alt="chosenpicture" />
                    </div>
                    <div>
                        <input name='description' placeholder={props.picture.description} onChange={(e) => { setDescription(e.target.value) }} />
                    </div>
                    {folderNameError ? '' : <p className="warning">Folder name cannot be empty</p>}
                    <div>
                        <input name='foldername' placeholder={props.picture.folderName} onChange={(e) => { checkFolderNameNotEmpty(e) }} />
                    </div>
                    <div>
                        <select onChange={(e) => { setAccess(e.target.value) }}>
                            <option value='0'>Public</option>
                            <option value='1'>Public only for followers</option>
                            <option value='2'>Private</option>
                        </select>
                    </div>
                    <button type="submit" onClick={(e) => handleSubmit(e)}>Save</button>
                </form>
                <button onClick={(e) => deletePicture(e)}>Delete</button>
            </div>
        </div>
    );

}

export default EditPictureModal;