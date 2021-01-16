import React, { useState, useEffect } from 'react';
import { useHistory, BrowserRouter as Router, Route } from "react-router-dom";
import { NavMenu } from './components/NavMenu/NavMenu';
import { Home } from './components/Home/Home';
import Register from './components/Register/Register';
import Login from './components/Login/Login';
import Profile from './components/Profile/Profile';
import Settings from './components/Settings/Settings';
import { UserContext } from "./contexts/UserContext";
import ProtectedRoute from "./components/ProtectedRoute";
import './custom.css'

function App() {

    const [isAuthenticated, setIsAuthenticated] = useState(false);
    const [isLoading, setIsLoading] = useState(true);
    const history = useHistory();

    useEffect(() => {
        const loggedInUserName = localStorage.getItem("username");
        if (loggedInUserName !== null) {
            setIsAuthenticated(true);
        }
        setIsLoading(false);
    }, [history, isAuthenticated]);

    const handleLogOut = () => {
        setIsAuthenticated(false);
        localStorage.clear();
    }

    if (isLoading) {
        return null;
    }

    return (
        <div className="main" >
            <Router>
                <UserContext.Provider value={[isAuthenticated, setIsAuthenticated]}>
                    <NavMenu isAuthenticated={isAuthenticated} handleLogOut={handleLogOut} />
                    <Route exact path='/' component={props => (<Home  {...props} />)} />
                    <Route path='/login' component={props => (<Login  {...props} />)} />
                    <Route path='/register' component={Register} />
                    <ProtectedRoute path='/user/:username' component={props => (<Profile  {...props} />)} />
                    <ProtectedRoute path='/:username/settings' component={props => (<Settings  {...props} />)} />
                </UserContext.Provider>
            </Router>
        </div>
    );
}

export default App;