import React, { Component } from "react";

export class Login extends Component {
  static displayName = Login.name;
  render() {
    return (
      <div>
        <p>Login</p>
        <input placeholder="Email"></input>
        <input placeholder="Password"></input>
        <button>Login</button>
      </div>
    );
  }
}
