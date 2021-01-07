import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import "./Register.css";
import eye from '../../img/eye.png';

export class Register extends Component {
    static displayName = Register.name;

    constructor(props) {
        super(props);
        this.state = {
            isDisabled: true,
            hidden1: true,
            hidden2: true,
        }
        this.toggleShow = this.toggleShow.bind(this);
        //this.submitForm = this.submitForm.bind(this);
    }

    toggleShow(e) {
        if (e.target.name == "password1") {
            this.setState({ hidden1: !this.state.hidden1 });
        } else {
            this.setState({ hidden2: !this.state.hidden2 });
        }
    }

    validateEmail(email) {
        const pattern = /[a-zA-Z0-9]+[\.]?([a-zA-Z0-9]+)?[\@][a-z]{3,9}[\.][a-z]{2,5}/g;
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

    handleChange(e) {
        if (e.target.name === 'email') {
            this.validateEmail(e.target.value);
        }

        if (e.target.name === 'password1') {
            this.validatePassword(e.target.value);
        }

        if (e.target.name === 'password2') {
            this.checkIfPasswordsAreEqual(e.target.value);
        }
    }

    render() {
        return (
            <div className="register">
                <h2>Create Account</h2>
                <div className="input-fields">
                    <div>
                        <input id="name" placeholder="Your username" type="text" />
                    </div>
                    <div>
                        {this.state.emailError ? <p><span className="warning">Please Enter valid email address</span></p> : ''}
                        <input id="email" name="email" placeholder="Your email" type="text" onChange={(e) => { this.handleChange(e) }} />
                    </div>
                    {this.state.passwordError ? <p><span className="warning">The password must be at least 6 char long, contain a lowercase letter, an uppercase letter and a number</span></p> : ''}
                    <div className="password-container">
                        <input name="password1" type={this.state.hidden1 ? "password" : "text"} placeholder="Your password" onChange={(e) => { this.handleChange(e) }} />
                        <img name="password1" src={eye} className="eye" onClick={this.toggleShow} />
                    </div>
                    {this.state.equalPasswords ? <p><span className="warning">The passwords are not equal</span></p> : ''}
                    <div className="password-container">
                        <input name="password2" type={this.state.hidden2 ? "password" : "text"} placeholder="Repeat your password" onChange={(e) => { this.handleChange(e) }} />
                        <img name="password2" src={eye} className="eye" onClick={this.toggleShow} />
                    </div>
                </div>
                <div>
                    <p>
                        <input type="checkbox" id="" name="" value="" />
                         I agree all statements in Terms of service.
                        </p>
                </div>
                <button disabled={this.state.isDisabled}>Sign up</button>
                <p>Already have an account? Yay! <Link tag={Link} to="/login"> Login here</Link></p>
            </div>
        );
    }
}



//handleChange(e){
//    const target = e.target;
//    const value = target.type === 'checkbox' ? target.checked : target.value;
//    const name = target.name;
//    this.setState({
//        [name]: value
//    });
//    if (e.target.name === 'firstname') {
//        if (e.target.value === '' || e.target.value === null) {
//            this.setState({
//                firstnameError: true
//            })
//        } else {
//            this.setState({
//                firstnameError: false,
//                firstName: e.target.value
//            })
//        }
//    }
//    if (e.target.name === 'lastname') {
//        if (e.target.value === '' || e.target.value === null) {
//            this.setState({
//                lastnameError: true
//            })
//        } else {
//            this.setState({
//                lastnameError: false,
//                lastName: e.target.value
//            })
//        }
//    }
//    if (e.target.name === 'email') {
//        this.validateEmail(e.target.value);
//    }
//    if (e.target.name === 'password') {
//        if (e.target.value === '' || e.target.value === null) {
//            this.setState({
//                passwordError: true
//            })
//        } else {
//            this.setState({
//                passwordError: false,
//                password: e.target.value
//            })
//        }
//    }
//    if (this.state.firstnameError === false && this.state.lastnameError === false &&
//        this.state.emailError === false && this.state.passwordError === false) {
//        this.setState({
//            isDisabled: false
//        })
//    }
//}
//submitForm(e){
//    e.preventDefault();
//    const data = {
//        firstName: this.state.firstName,
//        lastName: this.state.lastName,
//        email: this.state.email,
//        password: this.state.password
//    }
//    sendFormData(data).then(res => {
//        if (res.status === 200) {
//            alert(res.data);
//            this.props.history.push('/');
//        } else {

//        }
//    });
//}

