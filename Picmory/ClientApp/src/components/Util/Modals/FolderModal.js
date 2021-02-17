import React, { useState } from "react";
import axios from 'axios';
import './Modal.css';

const FolderModal = props => {

    const [folderName, setFolderName] = useState('');
    const [access, setAccess] = useState(0);

    const showHideClassName = props.show ? "modal display-block" : "modal display-none";

    const handleSubmit = async (e) => {
        try {
            const data = {
                Name: folderName,
                Access: access
            }

            const result = await axios.post('/folder/createnewfolder', data)
            console.log(result.data);

        } catch (e) {
            console.log(e);
        }
    }

    return (
        <div className={showHideClassName}>
            <div className="modal-main">
                <h2>Create new folder</h2>
                <form>
                    <input name='foldername' placeholder='Folder name' onChange={(e) => { setFolderName(e.target.value) }} />
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

export default FolderModal;