import React, { useState } from 'react';
import { Route } from 'react-router';
import { NavMenu } from './components/NavMenu/NavMenu';
import { Home } from './components/Home/Home';
import Register from './components/Register/Register';
import Login from './components/Login/Login';
import Profile from './components/Profile/Profile';
import './custom.css'

function App() {

    const [state, setState] = useState({
        loggedIn: false,
        username: '',
        profilePic: 0,
        colorOne: 0,
        colorTwo: 0
    });

    const handleLogin = (username, profilePic, colorOne, colorTwo) => {
        setState({
            loggedIn: true,
            username: username,
            profilePic: profilePic,
            colorOne: colorOne,
            colorTwo: colorTwo
        });
    }

    return (
        <div className="main" >
            <NavMenu profilPicture={state.profilePic} username={state.username} loggedIn={state.loggedIn} />
            <Route exact path='/' component={Home} />
            <Route path='/login' render={props => (<Login  {...props} username={state.username} loggedIn={state.loggedIn} handleLogIn={handleLogin} />)} />
            <Route path='/register' component={Register} />
            <Route path={`/user/${state.username}`} render={props => (<Profile  {...props} loggedIn={state.loggedIn} />)} />
        </div>
    );
}

export default App;