import React, { useContext, useState } from "react";
import { Link, useHistory, useLocation } from 'react-router-dom';
import UserContext from "../../contexts/UserContext";
import axios from 'axios';
import "./Login.css";
import "../Util/Common.css";
import eye from '../../img/eye.png';

const Login = props => {

    const [hidden, setHidden] = useState(true);
    const [usernameOrEmail, setUsernameOrEmail] = useState('');
    const [password, setPassword] = useState('');
    const [loading, setLoading] = useState(false);
    const [loginError, setLoginError] = useState('');
    const [isAuthenticated, setIsAuthenticated] = useContext(UserContext);
    const history = useHistory();
    const location = useLocation();

    const handleSubmit = async (e) => {
        e.preventDefault();
        setLoading(true);
        const data = {
            UserName: usernameOrEmail,
            Password: password
        }

        try {
            const result = await axios.post('/authentication/login', data, {
                headers: { 'Content-Type': 'application/json' }
            })

            localStorage.setItem('username', result.data.userName);
            setLoginError('');
            setIsAuthenticated(true);
            setLoading(false);
            const referrer = location.state ? location.state.from : `/user/${localStorage.getItem('username')}`;
            history.push(referrer);

        } catch (e) {
            setLoginError(e.response.data);
            setLoading(false);
            console.log(e.response.data);
        }
    }

    const handleKeyPress = e => {
        if (e.keyCode === 13) {
            handleSubmit();
        }
    }

    return (
        <div className="login-main">
            {loading ? <p>Loading...</p> : ''}
            {loginError === '' ? '' : <p className="warning">{loginError}</p>}
            <h2>Login</h2>
            <form onKeyPress={(e) => handleKeyPress(e) }>
                <div className="input-fields">
                    <div>
                        <input name="usernameOrEmail" value={usernameOrEmail} placeholder="Username or Email" onChange={(e) => { setUsernameOrEmail(e.target.value) }} />
                    </div>
                    <div className="password-container">
                        <input name="password" value={password} type={hidden ? "password" : "text"} placeholder="Password" onChange={(e) => { setPassword(e.target.value) }} />
                        <img name="password" src={eye} className="eye" onClick={() => setHidden(!hidden)} alt="toggleShowHide" />
                    </div>
                </div>
                <p className="forgot-password underline"><Link tag={Link} to="/register">Forgot password?</Link></p>
                <button type="submit" onClick={(e) => handleSubmit(e)}>Login</button>
            </form>
            <p className="back-to-register underline">Don't have an account yet? Join us! <Link tag={Link} to="/register">Sign up here</Link></p>
        </div >
    );
}

export default Login;
