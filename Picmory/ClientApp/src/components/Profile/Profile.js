import React, { useEffect, useState } from "react";
import axios from 'axios';
import { Link } from "react-router-dom";
import pencil from "../../img/pngwing.com.png";
import "./Profile.css";

const Profile = () => {
    const [username, setUsername] = useState('');
    const [email, setEmail] = useState('');
    const [profilePic, setProfilePic] = useState(0);
    const [followed, setFollowed] = useState(0);
    const [followers, setFollowers] = useState('');
    const [colorOne, setColorOne] = useState(0);
    const [colorTwo, setColorTwo] = useState(0);
    const [isLoading, setIsLoading] = useState(true);
    const [folders, setFolders] = useState();
    const [pictures, setPictures] = useState();

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
        } catch (e) {
            console.log(e);
        }

        try {
            const data = {
                Offset: 10,
                FolderName: ''
            };
            const res = async () => {
                const resp = await axios.get('/picture/getmyimages', data);
                console.log(resp.data);
            }
            res();
        } catch (e) {
            console.log(e);
        }
    }, [])

    if (isLoading) {
        return (<div><p>Loading...</p></div>)
    }

    const folderList = folders.map((folder) =>
        <Link to="/">{folder}<img className="pencil" src={pencil} alt="edit" /></Link>
    );

    //const pictureList = pictures.map((picture) =>
    //    <Link to="/">{picture}<img className="pencil" src={pencil} alt="edit" /></Link>
    //);

    return (
        <div className="profile" >
            <div className="left-side">
                <img src={`https://localhost:44386/picture/${profilePic}`} className="profile-pic" alt="profile pic" />
                <p className="username">{username}</p>
                <p className="username">{email}</p>
                <p>{followers} followers {followed} following</p>
                <div>
                    <div className="upload-pic">
                        <button>upload picture +</button>
                    </div>
                </div>
                <div className="folders">
                    <div className="folder">
                        {folderList}
                    </div>
                    <div className="folder new"><button>new folder +</button></div>
                </div>
            </div>
            <div className="right-side">
                <p>basically all the pictures here</p>
            </div>
        </div>
    );
}

export default Profile;