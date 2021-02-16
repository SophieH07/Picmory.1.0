import React, { useContext } from "react";
import { Route, Redirect } from "react-router-dom";
import UserContext from "../../contexts/UserContext";

const ProtectedRoute = ({ component: Component, ...rest }) => {
    const [isAuthenticated] = useContext(UserContext);

    return (
        <Route
            {...rest}
            render={(props) => {
                if (isAuthenticated) {
                    console.log(isAuthenticated);
                    return <Component {...props} />
                } else {
                    console.log(isAuthenticated);
                    return <Redirect
                        to={{
                            pathname: "/",
                            state: { from: props.location.pathname },
                        }}
                    />
                }
            }}
        />
    );
};

export default ProtectedRoute;