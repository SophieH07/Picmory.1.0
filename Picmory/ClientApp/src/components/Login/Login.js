import React, { Component } from "react";
import { Link, Redirect } from 'react-router-dom';
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
        this.login = this.login.bind(this);
    }

    //toggles eye image for show/hide password
    toggleShow() {
        this.setState({ hidden: !this.state.hidden })
    }

    //handles changes in input fields and saves state variables
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

    //login
    login() {
        const data = {
            UserName: this.state.usernameOrEmail,
            //Email: this.state.usernameOrEmail,
            Password: this.state.password
        }

        axios.post('/authentication/login', data, {
            headers: { 'Content-Type': 'application/json' }
        }).then(result => {
            //console.log(result);
            this.setState({
                loggedIn: true
            })
        }).catch(err => {
            console.log(err);
            this.setState({
                loggedIn: false
            })
        })
    }

    render() {
        if (this.state.loggedIn) {
            return <Redirect to={{ pathname: `/user` }} />
        } else {
            return (
                <div className="login-main">
                    <h2>Login</h2>
                    <div className="inputs">
                        <div>
                            <input name="usernameOrEmail" placeholder="Username or Email" onChange={(e) => { this.handleChange(e) }}></input>
                        </div>
                        <div className="password-container">
                            <input name="password" type={this.state.hidden ? "password" : "text"} placeholder="Password" onChange={(e) => { this.handleChange(e) }} />
                            <img name="password" src={eye} className="eye" onClick={this.toggleShow} alt="toggleShowHide" />
                        </div>
                    </div>
                    <p className="forgot-password underline"><Link tag={Link} to="/register">Forgot password?</Link></p>
                    <button onClick={this.login}>Login</button>
                    <p className="back-to-register underline">Don't have an account yet? Join us! <Link tag={Link} to="/register">Sign up here</Link></p>
                </div >
            );
        }
    }
}
