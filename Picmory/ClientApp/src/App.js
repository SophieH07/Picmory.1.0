import React from 'react';
import { Route } from 'react-router';
import { NavMenu } from './components/NavMenu/NavMenu';
import { Home } from './components/Home/Home';
import { Register } from './components/Register/Register';
import { Login } from './components/Login/Login';
import { Profile } from './components/Profile/Profile';
import './custom.css'

function App() {
    return (
        <div className="main">
            <NavMenu />
            <Route exact path='/' component={Home} />
            <Route path='/login' component={Login} />
            <Route path='/register' component={Register} />
            <Route path='/user' component={Profile} />
        </div>
    );
}

export default App;
