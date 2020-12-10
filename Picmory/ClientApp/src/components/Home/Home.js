import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import "./Home.css";
import name from '../../img/transparentNameOnly.png';

export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
      <div className="home">
            <h1>Welcome on</h1>
            <img src={name} className="home-name" />
            <h3>Save and share your memories by photos</h3>
            <h2>
                <Link tag={ Link } to="/fetch-data">Sign up</Link> or <Link tag={Link} to="/counter">Log in</Link> to start
            </h2>
      </div>
    );
  }
}
