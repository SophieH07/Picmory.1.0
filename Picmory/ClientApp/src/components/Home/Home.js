import React from 'react';
import { Link } from 'react-router-dom';
import "./Home.css";
import name from '../../img/transparentNameOnly.png';

export function Home() {
    return (
        <div className="home">
            <h1>Welcome on</h1>
            <img src={name} className="home-name" alt="picmory" />
            <h3>Save and share your memories by photos</h3>
            <h2>
                <Link tag={Link} to="/register">Sign up</Link> or <Link tag={Link} to="/login">Log in</Link> to start
            </h2>
        </div>
    );
}
