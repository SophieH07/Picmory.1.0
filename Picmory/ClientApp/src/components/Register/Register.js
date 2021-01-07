import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import "./Register.css";

export class Register extends Component {
    static displayName = Register.name;
    render() {
        return (
            <div className="register">
                <h2>Create Account</h2>
                <div className="input-fields">
                    <input id="name" placeholder="Your username" type="text" />
                    <input id="email" placeholder="Your email" type="text" />
                    <input id="password1" placeholder="Your password" type="text" />
                    <input id="password2" placeholder="Repeat your password" type="text" />
                </div>
                <div>
                    <p>
                        <input type="checkbox" id="" name="" value="" />
                         I agree all statements in Terms of service.
                        </p>
                </div>
                <button>Sign up</button>
                <p>Already have an account? Yay! <Link tag={Link} to="/login"> Login here</Link></p>
            </div>
        );
    }
}
