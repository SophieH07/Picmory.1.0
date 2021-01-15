import React, { useContext } from "react";
import { Route, Redirect } from "react-router-dom";
import UserContext from "../contexts/UserContext";

const ProtectedRoute = ({ component: Component, ...rest }) => {
    const [isAuthenticated] = useContext(UserContext);

    return (
        <Route
            {...rest}
            render={(props) => {
                return isAuthenticated === true ? (
                    <Component {...props} />
                ) : (
                        <Redirect
                            to={{
                                pathname: "/",
                                state: { from: props.location.pathname },
                            }}
                        />
                    );
            }}
        />
    );
};

export default ProtectedRoute;