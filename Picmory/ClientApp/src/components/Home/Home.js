import React, { useContext, useEffect } from "react";
import { Link, useHistory, useLocation } from 'react-router-dom';
import UserContext from "../../contexts/UserContext";
import "./Home.css";
import name from '../../img/transparentNameOnly.png';

export function Home(props) {
    const [isAuthenticated] = useContext(UserContext);
    const history = useHistory();
    const location = useLocation();

    useEffect(() => {
        if (isAuthenticated) {
            const referrer = location.state ? location.state.from : `/user/${localStorage.getItem('username')}`;
            history.push(referrer);
        }
    })

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
