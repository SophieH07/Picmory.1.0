import React, { Component } from 'react';
import "./Register.css";

export class Register extends Component {
  static displayName = Register.name;
  render() {
    return (
      <div>
        <h2>Create Account</h2>
        <p>Your name</p>
        <input type="text"></input>
        <p>Your Email</p>
        <input type="text"></input>
        <p>Password</p>
        <input type="text"></input>
        <p>Repeat your passord</p>
        <input type="text"></input>
        <br></br>
        <input type="checkbox" id="vehicle1" name="vehicle1" value="Bike"></input>
        <p>I agree all statenments in Terms of service</p>
        <button>Sign up</button>
        <p>Have already an account? Login here</p>
      </div>
    );
  }
}
