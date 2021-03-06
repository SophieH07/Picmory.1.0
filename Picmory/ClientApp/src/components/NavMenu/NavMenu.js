import React, { useState, useEffect } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { NavDropdown } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import './NavMenu.css';
import logo from "../../img/PicmoryLogoTransparent.png";
import name from "../../img/transparentNameOnly.png";

export function NavMenu(props) {
    const [collapsed, setCollapsed] = useState(true);
    const [username, setUsername] = useState('');

    useEffect(() => {
        if (props.isAuthenticated) {
            setUsername(localStorage.getItem("username"))
        }
    }, [props.isAuthenticated])

    const contentUser = (
        <ul className="navbar-nav flex-grow">
            <NavItem>
                <NavDropdown as={NavItem} title={username} className="text-dark" >
                    <NavDropdown.Item href={`/user/${username}`}>Profile</NavDropdown.Item>
                    <NavDropdown.Item href={`/${username}/settings`}>Settings</NavDropdown.Item>
                    <NavDropdown.Divider />
                    <NavDropdown.Item onClick={props.handleLogOut}>Log out</NavDropdown.Item>
                </NavDropdown>
            </NavItem >
        </ul >
    );

    const contentNoUser = (
        <ul className="navbar-nav flex-grow">
            <NavItem>
                <NavLink tag={Link} className="text-dark" to="/login">Log in</NavLink>
            </NavItem>
            <NavItem>
                <NavLink tag={Link} className="text-dark" to="/register">Sign up</NavLink>
            </NavItem>
        </ul>
    )

    return (
        <header>
            <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" light>
                <Container>
                    <NavbarBrand tag={Link} to="/"><img src={logo} className="navbar-logo-name" alt="logo" />
                        <img src={name} className="navbar-logo-name" alt="name-logo" />
                    </NavbarBrand>
                    <NavbarToggler onClick={() => setCollapsed(!collapsed)} className="mr-2" />
                    <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!collapsed} navbar>
                        {props.isAuthenticated ? contentUser : contentNoUser}
                    </Collapse>
                </Container>
            </Navbar>
        </header>
    );
}
