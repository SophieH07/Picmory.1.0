import React, { Component } from 'react';
import { Route } from 'react-router';
import { NavMenu } from './components/NavMenu/NavMenu';
import { Home } from './components/Home/Home';
import Register from './components/Register/Register';
import Login from './components/Login/Login';
import Profile from './components/Profile/Profile';
import './custom.css'

export default class App extends Component {

    constructor(props) {
        super(props);
        this.handleLogin = this.handleLogin.bind(this);
        this.state = {
            loggedIn: false,
            username: '',
            profilePic: 0,
            colorOne: 0,
            colorTwo: 0
        }
    }

    handleLogin(username, profilePic, colorOne, colorTwo) {
        this.setState({
            loggedIn: true,
            username: username,
            profilePic: profilePic,
            colorOne: colorOne,
            colorTwo: colorTwo
        })
    }

    render() {
        return (
            <div className="main" >
                <NavMenu profilPicture={this.state.profilePic} username={this.state.username} loggedIn={this.state.loggedIn} />
                <Route exact path='/' component={Home} />
                <Route path='/login' render={props => (<Login  {...props} username={this.state.username} loggedIn={this.state.loggedIn} handleLogIn={this.handleLogin} />)} />
                <Route path='/register' component={Register} />
                <Route path='/user' render={props => (<Profile  {...props} loggedIn={this.state.loggedIn} />)} />
            </div>
        );
    }
}
