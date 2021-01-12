import React, { Component } from "react";
import { Link } from "react-router-dom";
import pencil from "../../img/pngwing.com.png";
import "./Profile.css";

export class Profile extends Component {
    static displayName = Profile.name;

    render() {
        return (
            <div className="profile">
                <div className="left-side">
                    <img className="profile-pic" alt="profile pic" />
                    <p className="username">username here</p>
                    <p>0 followers 0 following</p>
                    <div>
                        <div className="upload-pic">
                            <Link>upload picture +</Link>
                        </div>
                    </div>
                    <div className="folders">
                        <div className="folder">
                            <Link>folder<img className="pencil" src={pencil} alt="edit" /></Link>
                        </div>
                        <div className="folder new"><Link>new folder +</Link></div>
                    </div>
                </div>
                <div className="right-side">
                    <p>basically all the pictures here</p>
                </div>
            </div>
        );
    }
}