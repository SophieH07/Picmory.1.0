import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import axios from 'axios';
import "./Register.css";
import eye from '../../img/eye.png';

export class Register extends Component {
    static displayName = Register.name;

    constructor(props) {
        super(props);
        this.state = {
            hidden1: true,
            hidden2: true,
            isChecked: false
        }
        this.toggleShow = this.toggleShow.bind(this);
        this.toggleCheck = this.toggleCheck.bind(this);
        this.submitForm = this.submitForm.bind(this);
    }

    toggleShow(e) {
        if (e.target.name === "password1") {
            this.setState({ hidden1: !this.state.hidden1 });
        } else {
            this.setState({ hidden2: !this.state.hidden2 });
        }
    }

    toggleCheck() {
        this.setState({
            isChecked: !this.state.isChecked
        })
    }

    validateUsername(username) {
        if (username !== '') {
            this.setState({
                username: username,
                usernameError: false
            })
        } else {
            this.setState({
                usernameError: true
            })
        }
    }

    validateEmail(email) {
        const pattern = new RegExp(/[a-zA-Z0-9]+[\.]?([a-zA-Z0-9]+)?[\@][a-z]{2,9}[\.][a-z]{2,5}/g);
        const result = pattern.test(email);
        if (result === true) {
            this.setState({
                emailError: false,
                email: email
            })
        } else {
            this.setState({
                emailError: true
            })
        }
    }

    validatePassword(password) {
        const pattern = new RegExp("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.{6,})");
        const validInput = pattern.test(password);
        if (validInput === true) {
            this.setState({
                passwordError: false,
                password: password
            })
        } else {
            this.setState({
                passwordError: true
            })
        }
    }

    checkIfPasswordsAreEqual(confirmPassword) {
        if (this.state.password === confirmPassword) {
            this.setState({
                equalPasswords: false
            })
        } else {
            this.setState({
                equalPasswords: true
            })
        }
    }

    checkIfUsernameAlreadyExists(username) {
        let usernameString = '"' + username + '"'
        axios.post('https://localhost:44386/authentication/checkusernamealreadyexist', usernameString, {
            headers: { 'Content-Type': 'application/json' }
        }).then(result => {
            console.log(result)
            if (result.data) {
                this.setState({
                    usernameAlreadyExist: true
                })
            } else {
                this.setState({
                    usernameAlreadyExist: false
                })
            }
        })
    }

    checkIfEmailAlreadyExists(email) {
        let emailString = '"' + email + '"'
        axios.post('https://localhost:44386/authentication/checkemailalreadyexist', emailString, {
            headers: { 'Content-Type': 'application/json' }
        }).then(result => {
            console.log(result)
            if (result.data) {
                this.setState({
                    emailAlreadyExist: true
                })
            } else {
                this.setState({
                    emailAlreadyExist: false
                })
            }
        })
    }

    handleChange(e) {
        if (e.target.name === 'username') {
            this.validateUsername(e.target.value);
            this.checkIfUsernameAlreadyExists(e.target.value);
        }

        if (e.target.name === 'email') {
            this.validateEmail(e.target.value);
            this.checkIfEmailAlreadyExists(e.target.value);
        }

        if (e.target.name === 'password1') {
            this.validatePassword(e.target.value);
        }

        if (e.target.name === 'password2') {
            this.checkIfPasswordsAreEqual(e.target.value);
        }
    }

    registration() {
        const data = {
            UserName: this.state.username,
            Email: this.state.email,
            Password: this.state.password
        }

        axios.post('https://localhost:44386/authentication/register', data, {
            headers: { 'Content-Type': 'application/json' }
        }).then(result => {
            console.log(result);
        })
    }


    submitForm() {
        if (this.state.isChecked === true && this.state.username !== '' && this.state.email !== '' && this.state.emailError === false && this.state.password !== '' && this.state.passwordError === false && this.state.equalPasswords === false) {
            this.setState({
                formIsFull: true
            })
            this.registration()
        } else {
            this.setState({
                formIsFull: false
            })
            alert("Oops, something is still missing or wrong!");
        }
    }

    render() {
        return (
            <div className="register">
                <h2>Create Account</h2>
                <div className="input-fields">
                    {this.state.usernameError ? <p><span className="warning">The username cannot be null</span></p> : ''}
                    {this.state.usernameAlreadyExist ? <p><span className="warning">The username is already taken</span></p> : ''}
                    <div>
                        <input id="name" name="username" placeholder="Your username*" type="text" onChange={(e) => { this.handleChange(e) }} />
                    </div>
                    <div>
                        {this.state.emailAlreadyExist ? <p><span className="warning">There is already a user with this email address</span></p> : ''}
                        {this.state.emailError ? <p><span className="warning">Please enter a valid email address</span></p> : ''}
                        <input id="email" name="email" placeholder="Your email*" type="text" onChange={(e) => { this.handleChange(e) }} />
                    </div>
                    {this.state.passwordError ? <p><span className="warning">The password must be at least 6 char long, contain a lowercase letter, an uppercase letter and a number</span></p> : ''}
                    <div className="password-container">
                        <input name="password1" type={this.state.hidden1 ? "password" : "text"} placeholder="Your password*" onChange={(e) => { this.handleChange(e) }} />
                        <img name="password1" src={eye} className="eye" onClick={this.toggleShow} alt="toggleShowHide" />
                    </div>
                    {this.state.equalPasswords ? <p><span className="warning">The passwords are not equal</span></p> : ''}
                    <div className="password-container">
                        <input name="password2" type={this.state.hidden2 ? "password" : "text"} placeholder="Repeat your password*" onChange={(e) => { this.handleChange(e) }} />
                        <img name="password2" src={eye} className="eye" onClick={this.toggleShow} alt="toggleShowHide" />
                    </div>
                </div>
                <div>
                    <p className="underline">
                        <input type="checkbox" id="" name="checkbox" onClick={this.toggleCheck} />
                         I agree all statements in <Link tag={Link} to='/register'>Terms of service</Link>.
                        </p>
                </div>
                <button onClick={this.submitForm} name="submit">Sign up</button>
                <p className="back-to-login underline">Already have an account? Yay! <Link tag={Link} to="/login"> Login here</Link></p>
            </div>
        );
    }
}

