import React, { useState, useEffect } from 'react';
import { Route } from 'react-router';
import { NavMenu } from './components/NavMenu/NavMenu';
import { Home } from './components/Home/Home';
import Register from './components/Register/Register';
import Login from './components/Login/Login';
import Profile from './components/Profile/Profile';
import './custom.css'

function App() {

    const [username, setUsername] = useState('');
    const [profilePic, setProfilePic] = useState(0);
    const [colorOne, setColorOne] = useState(0);
    const [colorTwo, setColorTwo] = useState(0);

    useEffect(() => {
        const loggedInUserName = localStorage.getItem("username");
        if (loggedInUserName !== null) {
            setUsername(loggedInUserName);
        }
    }, []);

    return (
        <div className="main" >
            <NavMenu username={username} />
            <Route exact path='/' component={Home} />
            <Route path='/login' render={props => (<Login  {...props} username={username} />)} />
            <Route path='/register' component={Register} />
            <Route path={`/user/:username`} render={props => (<Profile  {...props} username={username} />)} />
        </div>
    );
}

export default App;