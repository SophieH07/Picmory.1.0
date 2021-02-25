import React, { useState } from "react";
import axios from 'axios';
import '../Modal.css';
import '../Common.css';
import { useHistory, useLocation } from 'react-router-dom';

const PictureModal = props => {

    const [selectedFile, setSelectedFile] = useState()
    const [preview, setPreview] = useState()
    const [description, setDescription] = useState('');
    const [access, setAccess] = useState(0);
    const [folderName, setFolderName] = useState('');
    const [folderNameError, setFolderNameError] = useState(false);
    const [uploadError, setUploadError] = useState('');
    const [loading, setLoading] = useState(false);

    const showHideClassName = props.show ? "modal display-block" : "modal display-none";

    const history = useHistory();
    const location = useLocation();

    const uploadPicture = e => {
        if (e.target.files || e.target.files.length !== 0) {
            setSelectedFile(e.target.files[0])
            setPreview(URL.createObjectURL(e.target.files[0]));
        }
    }


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
                Photo: selectedFile.name,
                Description: description,
                FolderName: folderName,
                Access: access
            }
            console.log(data);
            const result = await axios.post('/picture/uploadpicture', data)
            console.log(result.data);
            setUploadError('');
            setLoading(false);
            const referrer = location.state ? location.state.from : `/user/${localStorage.getItem('username')}`;
            history.push(referrer);

        } catch (error) {
            setUploadError(error.response.data);
            setLoading(false);
            console.log(error);
        }
    }

    return (
        <div className={showHideClassName}>
            <div className="modal-main" ref={props.reference}>
                <h2>Upload picture</h2>
                {loading ? <p>Loading...</p> : ''}
                {uploadError !== '' ? <p>{uploadError}</p> : ''}
                <form className="input-fields">
                    <div>
                        {selectedFile && <img className="up-picture" src={preview} />}
                        <input type='file' onChange={(e) => { uploadPicture(e) }} />
                    </div>
                    <div>
                        <input name='description' placeholder="Description" onChange={(e) => { setDescription(e.target.value) }} />
                    </div>
                    <div>
                        {folderNameError ? '' : <p className="warning">Folder name cannot be empty</p>}
                        <input name='foldername' type="text" placeholder='Folder name' onChange={(e) => checkFolderNameNotEmpty(e)} />
                    </div>
                    <div>
                        <select onChange={(e) => { setAccess(e.target.value) }}>
                            <option value='0'>Public</option>
                            <option value='1'>Public only for followers</option>
                            <option value='2'>Private</option>
                        </select>
                    </div>
                    {uploadError !== '' ? <p>{uploadError}</p> : ''}
                    <button type="submit" onClick={(e) => handleSubmit(e)}>Create</button>
                </form>
            </div>
        </div>
    );

}

export default PictureModal;