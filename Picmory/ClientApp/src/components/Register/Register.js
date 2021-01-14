import React, { Component } from 'react';
import { Link, Redirect } from 'react-router-dom';
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
            isChecked: false,
        }
        this.toggleShow = this.toggleShow.bind(this);
        this.toggleCheck = this.toggleCheck.bind(this);
        this.submitForm = this.submitForm.bind(this);
    }

    //toggles eye image for show/hide password
    toggleShow(e) {
        if (e.target.name === "password1") {
            this.setState({ hidden1: !this.state.hidden1 });
        } else {
            this.setState({ hidden2: !this.state.hidden2 });
        }
    }

    //checks if checkbox is checked or not
    toggleCheck() {
        this.setState({
            isChecked: !this.state.isChecked
        })
    }

    //checks if username is not empty
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

    //checks if email has correct format
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

    //checks if password is at least 6 char long, has number, lowercase and uppercase letters
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

    //checks if both password input fields are equal
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

    //checks from database if username is alredy taken
    checkIfUsernameAlreadyExists(username) {
        axios.post('/authentication/checkusernamealreadyexist', JSON.stringify(username), {
            headers: { 'Content-Type': 'application/json' }
        }).then(result => {
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

    //checks from database if email is already registered
    checkIfEmailAlreadyExists(email) {
        axios.post('/authentication/checkemailalreadyexist', JSON.stringify(email), {
            headers: { 'Content-Type': 'application/json' }
        }).then(result => {
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

    //handles changes in input fields and call related functions
    handleChange(e) {
        if (e.target.name === 'username') {
            this.validateUsername(e.target.value);
            if (this.state.usernameError === false) {
                this.checkIfUsernameAlreadyExists(e.target.value);
            }
        }

        if (e.target.name === 'email') {
            this.validateEmail(e.target.value);
            if (this.state.emailError === false) {
                this.checkIfEmailAlreadyExists(e.target.value);
            }
        }

        if (e.target.name === 'password1') {
            this.validatePassword(e.target.value);
        }

        if (e.target.name === 'password2') {
            this.checkIfPasswordsAreEqual(e.target.value);
        }
    }

    //send user data to backend to save into database
    register() {
        const data = {
            UserName: this.state.username,
            Email: this.state.email,
            Password: this.state.password
        }

        axios.post('/authentication/register', data, {
            headers: { 'Content-Type': 'application/json' }
        }).then(result => {
            this.setState({
                isLoggedIn: true
            })
            console.log(result);
        }).catch(err => {
            this.setState({
                loginError: err.response.data,
                isLoggedIn: false
            })
        })
    }

    //when sign up button clicked, checks if everything is correct, then call register function
    submitForm() {
        if (this.state.emailAlreadyExist === false && this.state.usernameAlreadyExist === false && this.state.isChecked === true && this.state.username !== '' && this.state.email !== '' && this.state.emailError === false && this.state.password !== '' && this.state.passwordError === false && this.state.equalPasswords === false) {
            this.setState({
                formIsFull: true
            })
            this.register()
        } else {
            this.setState({
                formIsFull: false
            })
            alert("Oops, something is still missing or wrong!");
        }
    }

    render() {
        if (this.state.isLoggedIn) {
            return <Redirect to={{ pathname: `/user/${this.state.username}` }} />
        } else {
            return (
                <div className="register">
                    <h2>Create Account</h2>
                    <div className="input-fields">
                        {this.state.usernameError ? <p className="warning">The username cannot be null.</p> : ''}
                        {this.state.usernameAlreadyExist ? <p className="warning">Sorry, but this username is already taken.</p> : ''}
                        <div>
                            <input id="name" name="username" placeholder="Your username*" type="text" onChange={(e) => { this.handleChange(e) }} />
                        </div>
                        <div>
                            {this.state.emailAlreadyExist ? <p className="warning">There is already a user with this email address.</p> : ''}
                            {this.state.emailError ? <p className="warning">Please enter a valid email address.</p> : ''}
                            <input id="email" name="email" placeholder="Your email*" type="email" onChange={(e) => { this.handleChange(e) }} />
                        </div>
                        {this.state.passwordError ? <p className="warning">The password must be at least 6 char long, contain a lowercase and uppercase letter and a number.</p> : ''}
                        <div className="password-container">
                            <input name="password1" type={this.state.hidden1 ? "password" : "text"} placeholder="Your password*" onChange={(e) => { this.handleChange(e) }} />
                            <img name="password1" src={eye} className="eye" onClick={this.toggleShow} alt="toggleShowHide" />
                        </div>
                        {this.state.equalPasswords ? <p className="warning">The passwords are not equal.</p> : ''}
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
}

