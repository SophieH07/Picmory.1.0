﻿import React, { useState } from "react";
import { Link, useHistory, useLocation } from 'react-router-dom';
import axios from 'axios';
import '../Common.css';
import './Settings.css';
import eye from '../../img/eye.png';

const Settings = props => {

    const [hidden, setHidden] = useState(true);
    const [username, setUsername] = useState('');
    const [colorOne, setColorOne] = useState('');
    const [colorTwo, setColorTwo] = useState('');
    const [usernameError, setUsernameError] = useState(false);
    const [usernameAlreadyExists, setUsernameAlreadyExists] = useState(false);
    const [password, setPassword] = useState('');
    const [passwordError, setPasswordError] = useState(false);
    const [loadingUsername, setLoadingUsername] = useState(false);
    const [loadingEmail, setLoadingEmail] = useState(false);
    const [loadingSendingForm, setLoadingSendingForm] = useState(false);
    const [changeError, setChangeError] = useState('');
    const history = useHistory();
    const location = useLocation();

    const changeProfilePicture = picture => {
        //...
    }

    const validateUsername = username => {
        if (username !== '') {
            setUsername(username);
            setUsernameError(false);
        } else {
            setUsernameError(true);
        }
    }

    const checkIfUsernameAlreadyExists = username => {
        setLoadingUsername(true);
        axios.post('/authentication/checkusernamealreadyexist', JSON.stringify(username), {
            headers: { 'Content-Type': 'application/json' }
        }).then(result => {
            setLoadingUsername(false);
            if (result.data) {
                setUsernameAlreadyExists(true);
            } else {
                setUsernameAlreadyExists(false);
            }
        })
    }

    const validatePassword = password => {
        const pattern = new RegExp("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.{6,})");
        const validInput = pattern.test(password);
        if (validInput === true) {
            setPassword(password);
            setPasswordError(false);
        } else {
            setPasswordError(true);
        }
    }

    const setNewDataInDatabase = e => {
        e.preventDefault();
        setLoadingSendingForm(true);
        const data = {
            UserName: username,
            ColorOne: colorOne,
            ColorTwo: colorTwo
        }
        if (username !== '' && !usernameAlreadyExists) {
            axios.post("/user/changethemeandusername", data).then(result => {
                console.log(result);
                setLoadingSendingForm(false);
                //const referrer = location.state ? location.state.from : `/user/${localStorage.getItem('username')}`;
                //history.push(referrer);

            })

        } else {
            setLoadingSendingForm(false);
            setChangeError("something is wrong with the username or the colors");
        }
    }

    const setNewPasswordInDatabase = e => {
        e.preventDefault();
        setLoadingSendingForm(true);

        if (password !== '' && !passwordError) {
            axios.post("/user/changepassword", JSON.stringify(password)).then(result => {
                console.log(result);
                setLoadingSendingForm(false);
            })
        } else {
            setLoadingSendingForm(false);
            setChangeError("something wrong with the new password");
        }
    }

    const handleChange = e => {
        if (e.target.name === 'username') {
            validateUsername(e.target.value);
            if (!usernameError) {
                checkIfUsernameAlreadyExists(e.target.value);
            }
        }
        if (e.target.name === "colorOne") {
            setColorOne(e.target.value);
        }
        if (e.target.name === "colorTwo") {
            setColorTwo(e.target.value);
        }
        if (e.target.name === "password") {
            validatePassword(e.target.value)
        }
    }

    return (
        <div className="settings">
            <div className="input-fields">
                <div className="profile-picture">
                    <h4>Change profile picture</h4>
                    <img alt="profile" />
                    <button>Pick picture</button>
                </div>
                <div>
                    <input name="username" placeholder="Change username" type="text" onChange={(e) => handleChange(e)} />
                </div>
                <div className="themes">
                    <h4>Change themes</h4>
                    <p>Theme One</p>
                    <p>~colors~</p>
                    <p>Theme Two</p>
                    <p>~colors~</p>
                    <button onClick={(e) => setNewDataInDatabase(e)}>SAVE USERNAME AND COLORS</button>
                </div>
                <div>
                    <div className="password-container">
                        <input name="password" type={hidden ? "password" : "text"} placeholder="Change password" onChange={(e) => { handleChange(e) }} />
                        <img name="password" src={eye} className="eye" onClick={() => setHidden(!hidden)} alt="toggleShowHide" />
                    </div>
                </div>
                <div>
                    <div>
                        <button onClick={(e) => setNewPasswordInDatabase(e)}>SAVE PASSWORD</button>
                    </div>
                    <button className="delete">DELETE ACCOUNT</button>
                    <button className="cancel"><Link to='/user'>CANCEL</Link></button>
                </div>
            </div>
        </div>
    )
}

export default Settings;