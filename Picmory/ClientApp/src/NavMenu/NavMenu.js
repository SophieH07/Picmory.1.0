import React, { Component } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import './NavMenu.css';
import logo from "../img/PicmoryLogoTransparent.png";
import name from "../img/transparentNameOnly.png";

export class NavMenu extends Component {
    static displayName = NavMenu.name;

    constructor(props) {
        super(props);
        this.state = {
            collapsed: true,
        };
        this.toggleNavbar = this.toggleNavbar.bind(this);
    }

    toggleNavbar() {
        this.setState({ collapsed: !this.state.collapsed });
    }

    render() {
        return (
            <header>
                <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" light>
                    <Container>
                        <NavbarBrand tag={Link} to="/"><img src={logo} className="navbar-logo-name" alt="logo"/>
                            <img src={name} className="navbar-logo-name" alt="name-logo"/></NavbarBrand>
                        <NavbarToggler onClick={this.toggleNavbar} className="mr-2" />
                        <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!this.state.collapsed} navbar>
                            <ul className="navbar-nav flex-grow">
                                <NavItem>
                                    <NavLink tag={Link} className="text-dark" to="/">Home</NavLink>
                                </NavItem>
                                <NavItem>
                                    <NavLink tag={Link} className="text-dark" to="/login">Log in</NavLink>
                                </NavItem>
                                <NavItem>
                                    <NavLink tag={Link} className="text-dark" to="/register">Sign up</NavLink>
                                </NavItem>
                            </ul>
                        </Collapse>
                    </Container>
                </Navbar>
            </header>
        );
    }
}