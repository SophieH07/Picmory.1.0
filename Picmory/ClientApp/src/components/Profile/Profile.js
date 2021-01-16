import React, { useEffect, useState } from "react";
import axios from 'axios';
import { Link } from "react-router-dom";
import pencil from "../../img/pngwing.com.png";
import "./Profile.css";

const Profile = props => {
    const [username, setUsername] = useState('');
    const [email, setEmail] = useState('');
    const [profilePic, setProfilePic] = useState(0);
    const [followed, setFollowed] = useState(0);
    const [followers, setFollowers] = useState('');
    const [colorOne, setColorOne] = useState(0);
    const [colorTwo, setColorTwo] = useState(0);

    useEffect(() => {
        const result = async () => {
            const response = await axios.get('/user/myinfo');

            setUsername(response.data.userName);
            setEmail(response.data.email);
            setProfilePic(response.data.profilePictureId);
            setFollowers(response.data.followers);
        }
        result();
    }, [])

    return (
        <div className="profile" >
            <div className="left-side">
                <img className="profile-pic" alt="profile pic" />
                <p className="username">{username}</p>
                <p className="username">{email}</p>
                <p>{followers} followers 0 following</p>
                <div>
                    <div className="upload-pic">
                        <Link to="/">upload picture +</Link>
                    </div>
                </div>
                <div className="folders">
                    <div className="folder">
                        <Link to="/">folder<img className="pencil" src={pencil} alt="edit" /></Link>
                    </div>
                    <div className="folder new"><Link to="/">new folder +</Link></div>
                </div>
            </div>
            <div className="right-side">
                <p>basically all the pictures here</p>
            </div>
        </div>
    );
}

export default Profile;