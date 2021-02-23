import React, { useContext, useState } from "react";
import { Link, useHistory, useLocation } from 'react-router-dom';
import UserContext from "../../contexts/UserContext";
import axios from 'axios';
import "./Register.css";
import "../Util/Common.css";
import eye from '../../img/eye.png';

const Register = props => {

    const [hidden1, setHidden1] = useState(true);
    const [hidden2, setHidden2] = useState(true);
    const [isChecked, setIsChecked] = useState(false);
    const [username, setUsername] = useState('');
    const [usernameError, setUsernameError] = useState(false);
    const [usernameAlreadyExists, setUsernameAlreadyExists] = useState(false);
    const [email, setEmail] = useState('');
    const [emailError, setEmailError] = useState(false);
    const [emailAlreadyExists, setEmailAlreadyExists] = useState(false);
    const [password, setPassword] = useState('');
    const [equalPassword, setEqualPassword] = useState(true);
    const [passwordError, setPasswordError] = useState(false);
    const [loadingUsername, setLoadingUsername] = useState(false);
    const [loadingEmail, setLoadingEmail] = useState(false);
    const [loadingSendingForm, setLoadingSendingForm] = useState(false);
    const [registerError, setRegisterError] = useState('');
    const [isAuthenticated, setIsAuthenticated] = useContext(UserContext);
    const history = useHistory();
    const location = useLocation();

    const validateUsername = username => {
        if (username !== '') {
            setUsername(username);
            setUsernameError(false);
        } else {
            setUsernameError(true);
        }
    }

    const validateEmail = email => {
        const pattern = new RegExp(/[a-zA-Z0-9]+[\.]?([a-zA-Z0-9]+)?[\@][a-z]{2,9}[\.][a-z]{2,5}/g);
        const result = pattern.test(email);
        if (result === true) {
            setEmail(email);
            setEmailError(false);
        } else {
            setEmailError(true);
        }
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

    const checkIfPasswordsAreEqual = confirmPassword => {
        if (confirmPassword !== '') {
            if (password === confirmPassword) {
                setEqualPassword(true);
            } else {
                setEqualPassword(false);
            }
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

    const checkIfEmailAlreadyExists = email => {
        setLoadingEmail(true);
        axios.post('/authentication/checkemailalreadyexist', JSON.stringify(email), {
            headers: { 'Content-Type': 'application/json' }
        }).then(result => {
            setLoadingEmail(false);
            if (result.data) {
                setEmailAlreadyExists(true);
            } else {
                setEmailAlreadyExists(false);
            }
        })
    }

    const handleChange = e => {
        if (e.target.name === 'username') {
            validateUsername(e.target.value);
            if (usernameError === false) {
                checkIfUsernameAlreadyExists(e.target.value);
            }
        }

        if (e.target.name === 'email') {
            validateEmail(e.target.value);
            if (emailError === false) {
                checkIfEmailAlreadyExists(e.target.value);
            }
        }

        if (e.target.name === 'password1') {
            validatePassword(e.target.value);
        }

        if (e.target.name === 'password2') {
            checkIfPasswordsAreEqual(e.target.value);
        }
    }

    const handleSubmit = async (e) => {
        e.preventDefault();
        setLoadingSendingForm(true);

        if (isChecked && equalPassword) {

            const data = {
                UserName: username,
                Email: email,
                Password: password
            }

            try {
                const result = await axios.post('/authentication/register', data, {
                    headers: { 'Content-Type': 'application/json' }
                })

                localStorage.setItem('username', username);
                setRegisterError('');
                setIsAuthenticated(true);
                console.log(result);
                const referrer = location.state ? location.state.from : `/user/${localStorage.getItem('username')}`;
                history.push(referrer);
            } catch (err) {
                setRegisterError("Something is still missing or wrong!");
            }
            setLoadingSendingForm(false);

        } else {
            setRegisterError("Something is still missing or wrong!");
            setLoadingSendingForm(false);

        }
    }

    const handleKeyPress = e => {
        if (e.keyCode === 13) {
            handleSubmit();
        }
    }

    return (
        <div className="register" onKeyPress={(e) => handleKeyPress(e)}>
            <h2>Create Account</h2>
            <div className="input-fields">
                {usernameError ? <p className="warning">The username cannot be null.</p> : ''}
                {loadingUsername ? <p className="warning">Loading...</p> : ''}
                {usernameAlreadyExists ? <p className="warning">Sorry, but this username is already taken.</p> : ''}
                <div>
                    <input id="name" name="username" placeholder="Your username*" type="text" onChange={(e) => { handleChange(e) }} />
                </div>
                <div>
                    {loadingEmail ? <p className="warning">Loading...</p> : ''}
                    {emailAlreadyExists ? <p className="warning">There is already a user with this email address.</p> : ''}
                    {emailError ? <p className="warning">Please enter a valid email address.</p> : ''}
                    <input id="email" name="email" placeholder="Your email*" type="text" onChange={(e) => { handleChange(e) }} />
                </div>
                {passwordError ? <p className="warning">The password must be at least 6 char long, contain a lowercase and uppercase letter and a number.</p> : ''}
                <div className="password-container">
                    <input name="password1" type={hidden1 ? "password" : "text"} placeholder="Your password*" onChange={(e) => { handleChange(e) }} />
                    <img name="password1" src={eye} className="eye" onClick={() => setHidden1(!hidden1)} alt="toggleShowHide" />
                </div>
                {equalPassword ? '' : <p className="warning">The passwords are not equal.</p>}
                <div className="password-container">
                    <input name="password2" type={hidden2 ? "password" : "text"} placeholder="Repeat your password*" onChange={(e) => { handleChange(e) }} />
                    <img name="password2" src={eye} className="eye" onClick={() => setHidden2(!hidden2)} alt="toggleShowHide" />
                </div>
            </div>
            <div>
                <p className="underline">
                    <input type="checkbox" id="" name="checkbox" onClick={() => setIsChecked(!isChecked)} />
                         I agree all statements in <Link tag={Link} to='/register'>Terms of service</Link>.
                        </p>
            </div>
            {loadingSendingForm ? <p>Loading...</p> : ''}
            {registerError === '' ? '' : <p className="warning">{registerError}</p>}
            <button onClick={(e) => handleSubmit(e)} name="submit">Sign up</button>
            <p className="back underline">Already have an account? Yay! <Link tag={Link} to="/login"> Login here</Link></p>
        </div>
    );
}

export default Register;