import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import "./Register.css";
import eye from '../../img/eye.png';
import PasswordMask from 'react-password-mask';

export class Register extends Component {
    static displayName = Register.name;

    constructor(props) {
        super(props);
        this.state = {
            isDisabled: true,
            hidden1: true,
            hidden2: true
        }
        this.toggleShowOne = this.toggleShowOne.bind(this);
        this.toggleShowTwo = this.toggleShowTwo.bind(this);
        //this.submitForm = this.submitForm.bind(this);
    }

    toggleShowOne() {
        this.setState({ hidden1: !this.state.hidden1 });
    }

    toggleShowTwo() {
        this.setState({ hidden2: !this.state.hidden2 });
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

    validatePassword(password1, password2) {

    }

    handleChange(e) {
        //const target = e.target;
        //const name = target.name;

        //this.setState({
        //    [name]: value
        //});

        if (e.target.name === 'email') {
            this.validateEmail(e.target.value);
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
                    <div className="password-container">
                        <input id="password1" type={ this.state.hidden1 ? "password" : "text" } placeholder="Your password"/>
                        <img src={eye} className="eye" onClick={this.toggleShowOne} />
                    </div>
                    <div className="password-container">
                        <input id="password2" type={this.state.hidden2 ? "password" : "text"}  placeholder="Repeat your password"/>
                        <img src={eye} className="eye" onClick={this.toggleShowTwo} />
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

