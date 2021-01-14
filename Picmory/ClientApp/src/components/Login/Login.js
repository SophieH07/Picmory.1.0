import React, { useState } from "react";
import { Link, Redirect } from 'react-router-dom';
import axios from 'axios';
import "./Login.css";
import eye from '../../img/eye.png';

const Login = props => {

    const [hidden, setHidden] = useState(true);
    const [usernameOrEmail, setUsernameOrEmail] = useState('');
    const [password, setPassword] = useState('');
    const [loggedIn, setLoggedIn] = useState(false);
    const [username, setUsername] = useState('');
    const [loading, setLoading] = useState(false);

    const toggleShow = () => {
        setHidden(!hidden);
    }

    const login = () => {
        setLoading(true);
        const data = {
            UserName: usernameOrEmail,
            //Email: state.usernameOrEmail,
            Password: password
        }

        axios.post('/authentication/login', data, {
            headers: { 'Content-Type': 'application/json' }
        }).then(result => {
            props.handleLogIn(result.data.userName, result.data.pictureId, result.data.coloreOne, result.data.coloreTwo);
            setUsername(result.data.userName); //stays empty
            setLoading(false);
            setLoggedIn(true);
        }).catch(err => {
            console.log(err);
            setLoggedIn(false);
        })
    }

    if (loggedIn) {
        return <Redirect to={{ pathname: `/user/${username}` }} />
    } else {
        return (
            <div className="login-main">
                {loading ? <p>Loading...</p> : ''}
                <h2>Login</h2>
                <form>
                    <div className="inputs">
                        <div>
                            <input name="usernameOrEmail" value={usernameOrEmail} placeholder="Username or Email" onChange={(e) => { setUsernameOrEmail(e.target.value) }}></input>
                        </div>
                        <div className="password-container">
                            <input name="password" value={password} type={hidden ? "password" : "text"} placeholder="Password" onChange={(e) => { setPassword(e.target.value) }} />
                            <img name="password" src={eye} className="eye" onClick={toggleShow} alt="toggleShowHide" />
                        </div>
                    </div>
                </form>
                <p className="forgot-password underline"><Link tag={Link} to="/register">Forgot password?</Link></p>
                <button onClick={login}>Login</button>
                <p className="back-to-register underline">Don't have an account yet? Join us! <Link tag={Link} to="/register">Sign up here</Link></p>
            </div >
        );
    }
}

export default Login;
