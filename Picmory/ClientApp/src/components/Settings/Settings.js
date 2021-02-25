import React, { useEffect, useState } from "react";
import { Link, useHistory, useLocation } from 'react-router-dom';
import { GithubPicker } from 'react-color';
import axios from 'axios';
import '../Util/Common.css';
import './Settings.css';
import eye from '../../img/eye.png';

const Settings = props => {

    const [hidden, setHidden] = useState(true);
    const [username, setUsername] = useState('');
    const [profilePic, setProfilePic] = useState(0);
    const [newProfilePic, setNewProfilePic] = useState();
    const [newSrc, setNewSrc] = useState('');
    const [colorOne, setColorOne] = useState('');
    const [colorTwo, setColorTwo] = useState('');
    const [usernameError, setUsernameError] = useState(false);
    const [usernameAlreadyExists, setUsernameAlreadyExists] = useState(false);
    const [password, setPassword] = useState('');
    const [passwordError, setPasswordError] = useState(false);
    const [loadingUsername, setLoadingUsername] = useState(false);
    const [loadingSendingForm, setLoadingSendingForm] = useState(false);
    const [changeError, setChangeError] = useState('');
    const history = useHistory();
    const location = useLocation();

    useEffect(() => {
        try {
            const result = async () => {
                const response = await axios.get('/user/myuserinfo');
                setUsername(response.data.userName);
                setProfilePic(response.data.profilePictureId);
                setColorOne(response.data.coloreOne);
                setColorTwo(response.data.coloreTwo);
            }
            result();

        } catch (e) {

        }
    })

    const changeProfilePicture = event => {
        setNewProfilePic(event.target.value);
        setNewSrc(URL.createObjectURL(event.target.files[0]));
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
            ColorTwo: colorTwo,
            Password: password
        }
        if (username !== '' && !usernameAlreadyExists && password !== '' && !passwordError) {
            axios.post("/user/changeuserdata", data).then(result => {
                console.log(result);
                setChangeError('');
                setLoadingSendingForm(false);
                const referrer = location.state ? location.state.from : `/user/${localStorage.getItem('username')}`;
                history.push(referrer);
            })

        } else {
            setLoadingSendingForm(false);
            setChangeError("something is wrong with the data you gave");
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

    const deleteUser = () => {
        axios.post('/user/deleteuser').then((res) => {
            console.log(res);
        })
    }

    return (
        <div className="settings">
            <div className="input-fields">
                <div className="profile-picture">
                    <h4>Change profile picture</h4>
                    {newSrc ? <img className="profile-pic" id="new-pic" src={newSrc} alt="new-profile" /> : <img className="profile-pic" src={`https://localhost:44386/picture/picture/${profilePic}`} alt="profile" />}
                </div>
                <div>
                    <input name='picture' type="file" onChange={(e) => { changeProfilePicture(e) }} />
                </div>
                <div className="input-fields">
                    {usernameError ? <p className="warning">The username cannot be null.</p> : ''}
                    {loadingUsername ? <p className="warning">Loading...</p> : ''}
                    {usernameAlreadyExists ? <p className="warning">Sorry, but this username is already taken.</p> : ''}
                    <div>
                        <input name="username" placeholder="Change username" type="text" onChange={(e) => handleChange(e)} />
                    </div>
                </div>
                <h4>Change themes</h4>
                <div className="themes">
                    <div>
                        <h5>Theme One</h5>
                        <GithubPicker />
                    </div>
                    <div>
                        <h5>Theme Two</h5>
                        <GithubPicker />
                    </div>
                </div>
                <div>
                    {passwordError ? <p className="warning">The password must be at least 6 char long, contain a lowercase and uppercase letter and a number.</p> : ''}
                    <div className="password-container">
                        <input name="password" type={hidden ? "password" : "text"} placeholder="Change password" onChange={(e) => { handleChange(e) }} />
                        <img name="password" src={eye} className="eye" onClick={() => setHidden(!hidden)} alt="toggleShowHide" />
                    </div>
                </div>
                <div>
                    {loadingSendingForm ? <p>Loading...</p> : ''}
                    {changeError !== '' ? <p className="warning">{changeError}</p> : ''}
                    <div className="buttons">
                        <button onClick={(e) => setNewDataInDatabase(e)}>SAVE</button>
                        <button className="delete" onClick={deleteUser}>DELETE ACCOUNT</button>
                        <button className="cancel"><Link to={`/user/${localStorage.getItem('username')}`}>CANCEL</Link></button>
                    </div>
                </div>
            </div>
        </div>
    )
}

export default Settings;