import React, { Component } from "react";
import { Link } from 'react-router-dom';
import "./Login.css";
import eye from '../../img/eye.png';

export class Login extends Component {
    static displayName = Login.name;

    constructor(props) {
        super(props);
        this.state = {
            hidden: true,
            username: '',
            password: ''
        }
        this.toggleShow = this.toggleShow.bind(this);
        this.validateUsername = this.validateUsername.bind(this);
        this.validatePassword = this.validatePassword.bind(this);
    }

    toggleShow() {
        this.setState({ hidden: !this.state.hidden })
    }

    validateUsername(e) {

    }

    validatePassword(e) {

    }

    render() {
        return (
            <div className="login-main">
                <h2>Login</h2>
                <div className="inputs">
                <div>
                    <input placeholder="Username or Email"></input>
                </div>
                <div className="password-container">
                    <input name="password" type={this.state.hidden ? "password" : "text"} placeholder="Password" />
                    <img name="password" src={eye} className="eye" onClick={this.toggleShow} />
                    </div>
                </div>
                <p className="forgot-password underline"><Link tag={Link} to="/register">Forgot password?</Link></p>
                <button>Login</button>
                <p className="back-to-register underline">Don't have an account yet? Join us! <Link tag={Link} to="/register">Sign up here</Link></p>
            </div >
        );
    }
}
