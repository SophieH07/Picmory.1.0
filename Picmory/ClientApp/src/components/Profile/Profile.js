import React from "react";
import { Link, Redirect } from "react-router-dom";
import pencil from "../../img/pngwing.com.png";
import "./Profile.css";

const Profile = props => {
    if (props.username !== null) {
        return (
            <div className="profile" >
                <h3>{ }</h3>
                <div className="left-side">
                    <img className="profile-pic" alt="profile pic" />
                    <p className="username">username here</p>
                    <p>0 followers 0 following</p>
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
    } else {
        return <Redirect to="/login" />
    }
}

export default Profile;