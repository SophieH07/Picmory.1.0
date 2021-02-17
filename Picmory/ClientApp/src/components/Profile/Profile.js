import React, { useEffect, useState, useRef } from "react";
import axios from 'axios';
import { Link } from "react-router-dom";
import pencil from "../../img/pngwing.com.png";
import "./Profile.css";
import FolderModal from '../Util/Modals/FolderModal.js';
import useOutsideClick from "./useOutsideClick";

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
    const ref = useRef();

    useOutsideClick(ref, () => {
        if (showFolderModal || showPictureModal) {
            setShowFolderModal(false);
            setShowPictureModal(false);
        }
    });

    useEffect(() => {
        try {
            const result = async () => {
                const response = await axios.get('/user/myuserinfo');
                console.log(response.data);
                setUsername(response.data.userName);
                setEmail(response.data.email);
                setProfilePic(response.data.profilePictureId);
                setFollowers(response.data.followers);
                setFollowed(response.data.followed);
                setFolders(response.data.folders);
                setIsLoading(false);
            }
            result();

            const data = {
                Offset: 0
            }

            const res = async () => {
                const resp = await axios.post('/picture/getmyimages', data)
                console.log(resp.data);
                setPictures(resp.data);
            }
            res();

        } catch (e) {
            console.log(e);
        }
    }, [])

    if (isLoading) {
        return (<div><p>Loading...</p></div>)
    }

    const folderList = Object.entries(folders).map(([key, value]) =>
        <div className="folder" key={key}>
            <p><Link to="/">{value.folderName}<img className="pencil" src={pencil} alt="edit" /></Link></p>
        </div>
    );

    const pictureList = Object.entries(pictures).map(([key, value]) =>
        <div key={key}>
            <img className="picture" src={`https://localhost:44386/picture/picture/${value.id}`} alt="picture" />
        </div>
    );

    return (
        <div className="profile">
            {showFolderModal && (<FolderModal show={showFolderModal} reference={ref} />)}
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