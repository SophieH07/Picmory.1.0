import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import axios from 'axios';
import '../Common.css';
import './Settings.css';
import eye from '../../img/eye.png';

export class Settings extends Component {
    static displayName = Settings.name;

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

    changeProfilePicture(picture) {
        //...
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

    setNewDataInDatabase() {
        const data = {
            UserName: this.state.username,
            ColorOne: this.state.colorOne,
            ColorTwo: this.state.colorTwo
        }
        if (this.state.username !== '' && this.state.usernameAlreadyExist === false) {
            axios.post("/user/changethemeandusername", data).then(result => {
                console.log(result);
            })
        } else {
            alert("something is wrong with the new username");
        }
    }

    setNewPasswordInDatabase() {
        if (this.state.password !== '' && this.state.passwordError === false) {
            axios.post("/user/changepassword", JSON.stringify(this.sate.password)).then(result => {
                console.log(result);
            })
        } else {
            alert("something wrong with the new password");
        }

    }

    handleChange(e) {
        if (e.target.name === 'username') {
            this.validateUsername(e.target.value);
            if (this.state.usernameError === false) {
                this.checkIfUsernameAlreadyExists(e.target.value);
            }
        }
        if (e.target.name === "colorOne") {
            this.setState({
                colorOne: e.target.value
            })
        }
        if (e.target.name === "colorTwo") {
            this.setState({
                colorTwo: e.target.value
            })
        }
        if (e.target.name === "password") {
            this.validatePassword(e.target.value)
        }
    }

    render() {
        return (
            <div className="settings">
                <div className="input-fields">
                    <div className="profile-picture">
                        <h4>Change profile picture</h4>
                        <img alt="profile picture" />
                        <button>Pick picture</button>
                    </div>
                    <div>
                        <input name="username" placeholder="Change username" type="text" onChange={(e) => this.handleChange(e)} />
                    </div>
                    <div className="themes">
                        <h4>Change themes</h4>
                        <p>Theme One</p>
                        <p>~colors~</p>
                        <p>Theme Two</p>
                        <p>~colors~</p>
                        <button>SAVE USERNAME AND COLORS</button>
                    </div>
                    <div>
                        <div className="password-container">
                            <input name="password" type={this.state.hidden ? "password" : "text"} placeholder="Change password" onChange={(e) => { this.handleChange(e) }} />
                            <img name="password" src={eye} className="eye" onClick={this.toggleShow} alt="toggleShowHide" />
                        </div>
                    </div>
                    <div>
                        <button>SAVE PASSWORD</button>
                        <button className="delete">DELETE ACCOUNT</button>
                        <button className="cancel">CANCEL</button>
                    </div>
                </div>
            </div>
        )
    }
}