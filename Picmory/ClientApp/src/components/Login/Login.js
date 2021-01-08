import React, { Component } from "react";
import { Link } from 'react-router-dom';
import "./Login.css";
import eye from '../../img/eye.png';

export class Login extends Component {
    static displayName = Login.name;

    constructor(props) {
        super(props);
        this.state = {
            hidden: true
        }
        this.toggleShow = this.toggleShow.bind(this);
    }

    toggleShow() {
        this.setState({ hidden: !this.state.hidden })
    }

    render() {
        return (
            <div className="login-main">
                <h2>Login</h2>
                <div className="inputs">
                <div>
                    <input placeholder="Email"></input>
                </div>
                <div className="password-container">
                    <input name="password" type={this.state.hidden ? "password" : "text"} placeholder="Password" />
                    <img name="password" src={eye} className="eye" onClick={this.toggleShow} />
                    </div>
                </div>
                <button>Login</button>
                <p className="back-to-register underline">Don't have an account yet? Join us! <Link tag={Link} to="/register"> Sign up here</Link></p>
            </div >
        );
    }
}
