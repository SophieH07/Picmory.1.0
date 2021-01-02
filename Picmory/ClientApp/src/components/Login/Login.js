import React, { Component } from "react";
import "./Login.css";

export class Login extends Component {
  static displayName = Login.name;
  render() {
    return (
      <div className="login-main">
        <p>Login</p>
        <input placeholder="Email"></input>
        <input placeholder="Password"></input>
        <button>Login</button>
      </div>
    );
  }
}
