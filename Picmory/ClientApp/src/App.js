import React, { useState, useEffect } from 'react';
import { BrowserRouter as Router, Route } from "react-router-dom";
import { NavMenu } from './components/NavMenu/NavMenu';
import { Home } from './components/Home/Home';
import Register from './components/Register/Register';
import Login from './components/Login/Login';
import Profile from './components/Profile/Profile';
import { UserContext } from "./contexts/UserContext";
import ProtectedRoute from "./components/ProtectedRoute";
import './custom.css'

function App() {

    const [username, setUsername] = useState('');
    const [isAuthenticated, setIsAuthenticated] = useState(false);

    //useEffect(() => {
    //    const checkAuthentication = async () => {
    //        setIsAuthenticated(await studentService.isAuthenticated());
    //        setIsLoading(false);
    //    };

    //    checkAuthentication();
    //}, [studentService]);


    useEffect(() => {
        const loggedInUserName = localStorage.getItem("username");
        if (loggedInUserName !== null) {
            setUsername(loggedInUserName);
        }
        console.log(username);
    }, [username]);

    const handleLogOut = () => {
        setIsAuthenticated(false);
        localStorage.clear();
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
                </UserContext.Provider>
            </Router>
        </div>
    );
}

export default App;