import React, { Component } from "react";
import { Link } from 'react-router-dom';
import axios from 'axios';
import "./Login.css";
import eye from '../../img/eye.png';

export class Login extends Component {
    static displayName = Login.name;

    constructor(props) {
        super(props);
        this.state = {
            hidden: true,
            usernameOrEmail: '',
            password: ''
        }
        this.toggleShow = this.toggleShow.bind(this);
        this.handleChange = this.handleChange.bind(this);
    }

    toggleShow() {
        this.setState({ hidden: !this.state.hidden })
    }

    handleChange(e) {
        if (e.target.name === 'usernameOrEmail') {
            this.setState({
                usernameOrEmail: e.target.value
            })
        }
        if (e.target.name === 'password') {
            this.setState({
                password: e.target.value
            })
        }
    }

    validateLogin(usernameOrEmail, password) {
        axios.post('https://localhost:44386/authentication/login', {
            UserName: usernameOrEmail,
            Email: usernameOrEmail,
            password: password
        }).then(result => {
            console.log(result);
        })
    }

    render() {
        return (
            <div className="login-main">
                <h2>Login</h2>
                <div className="inputs">
                    <div>
                        <input name="usernameOrEmail" placeholder="Username or Email"></input>
                    </div>
                    <div className="password-container">
                        <input name="password" type={this.state.hidden ? "password" : "text"} placeholder="Password" />
                        <img name="password" src={eye} className="eye" onClick={this.toggleShow} alt="toggleShowHide" />
                    </div>
                </div>
                <p className="forgot-password underline"><Link tag={Link} to="/register">Forgot password?</Link></p>
                <button>Login</button>
                <p className="back-to-register underline">Don't have an account yet? Join us! <Link tag={Link} to="/register">Sign up here</Link></p>
            </div >
        );
    }
}
