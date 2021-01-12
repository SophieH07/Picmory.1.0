import React, { Component } from 'react';
import { Container } from 'reactstrap';
import { NavMenu } from '../../NavMenu/NavMenu';

export class Layout extends Component {
    static displayName = Layout.name;

    render() {
        const containerStyle = {
            textAlign: '-webkit-center'
        };

        return (
            <div>
                <NavMenu />
                <Container style={containerStyle}>
                    {this.props.children}
                </Container >
            </div >
        );
    }
}