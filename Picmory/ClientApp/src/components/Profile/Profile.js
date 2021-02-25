﻿import React, { useEffect, useState, useRef } from "react";
import axios from 'axios';
import pencil from "../../img/pngwing.com.png";
import "./Profile.css";
import FolderModal from '../Util/UploadModals/FolderModal.js';
import PictureModal from '../Util/UploadModals/PictureModal.js';
import EditFolderModal from '../Util/EditModals/EditFolderModal.js';
import EditPictureModal from '../Util/EditModals/EditPictureModal.js';
import useOutsideClick from "../Util/useOutsideClick";

const Profile = () => {
    const [username, setUsername] = useState('');
    const [email, setEmail] = useState('');
    const [profilePic, setProfilePic] = useState(0);
    const [followed, setFollowed] = useState(0);
    const [followers, setFollowers] = useState('');
    const [colorOne, setColorOne] = useState(0);
    const [colorTwo, setColorTwo] = useState(0);
    const [isLoading, setIsLoading] = useState(true);
    const [pictures, setPictures] = useState();
    const [folders, setFolders] = useState();


    const [showFolderModal, setShowFolderModal] = useState(false);
    const [showPictureModal, setShowPictureModal] = useState(false);

    const [showEditFolderModal, setShowEditFolderModal] = useState(false);
    const [showEditPictureModal, setShowEditPictureModal] = useState(false);

    const [selectedPicture, setSelectedPicture] = useState();
    const [selectedFolder, setSelectedFolder] = useState();

    const ref = useRef();

    useOutsideClick(ref, () => {
        if (showFolderModal || showPictureModal || showEditFolderModal || showEditPictureModal) {
            setShowFolderModal(false);
            setShowPictureModal(false);
            setShowEditFolderModal(false);
            setShowEditPictureModal(false);
        }
    });

    useEffect(() => {
        try {
            const data = {
                Offset: 0
            }

            const result = async () => {
                const response = await axios.get('/user/myuserinfo');
                const resp = await axios.post('/picture/getmyimages', data);
                setUsername(response.data.userName);
                setEmail(response.data.email);
                setColorOne(response.data.coloreOne);
                setColorTwo(response.data.coloreTwo);
                setProfilePic(response.data.profilePictureId);
                setFollowers(response.data.followers);
                setFollowed(response.data.followed);
                setFolders(response.data.folders);
                setPictures(resp.data);
                setIsLoading(false);
            }
            result();

        } catch (e) {
            console.log(e);
        }
    }, [])

    const showPictureFromFolder = folder => {
        try {
            const data = {
                Offset: 0,
                FolderName: folder.folderName
            }

            const result = async (e) => {
                const resp = await axios.post('/picture/getmyimages', data);
                setPictures(resp.data);
            }
            result();
        } catch (e) {
            console.log(e);
        }
    }


    const editFolder = folder => {
        setSelectedFolder(folder);
        setShowEditFolderModal(!showEditFolderModal);
    }

    const editPicture = picture => {
        setSelectedPicture(picture);
        setShowEditPictureModal(!showEditPictureModal);
    }

    if (isLoading) {
        return (<div><p>Loading...</p></div>)
    }

    const folderList = Object.entries(folders).map(([key, value]) =>
        <div className="folder" key={key}>
            <img className="pencil" src={pencil} alt="edit" onClick={() => editFolder(value)} />
            <p onClick={() => showPictureFromFolder(value)}>{value.folderName}</p>
        </div>
    );

    const pictureList = Object.entries(pictures).map(([key, value]) =>
        <div className="picture-div" key={key}>
            <img className="picture" src={`https://localhost:44386/picture/picture/${value.id}`} alt="picture" onClick={() => editPicture(value)} />
            <p>{value.description}</p>
        </div>
    );

    return (
        <div className="profile">
            {showFolderModal && (<FolderModal show={showFolderModal} reference={ref} />)}
            {showEditFolderModal && (<EditFolderModal folder={selectedFolder} show={showEditFolderModal} reference={ref} />)}
            {showPictureModal && (<PictureModal show={showPictureModal} reference={ref} />)}
            {showEditPictureModal && (<EditPictureModal picture={selectedPicture} show={showEditPictureModal} reference={ref} />)}
            <div className="left-side">
                <img src={`https://localhost:44386/picture/picture/${profilePic}`} className="profile-pic" alt="profile pic" />
                <p className="username">{username}</p>
                <p className="username">{email}</p>
                <p>{followers} followers {followed} following</p>
                <div>
                    <div className="upload-pic">
                        <button onClick={() => setShowPictureModal(!showPictureModal)}>upload picture +</button>
                    </div>
                </div>
                <div className="folders">
                    {folderList}
                    <div className="folder new">
                        <button onClick={() => setShowFolderModal(!showFolderModal)}>new folder +</button>
                    </div>
                </div>
            </div>
            <div className="right-side">
                <div>{pictureList}</div>
            </div>
        </div>
    );
}

export default Profile;