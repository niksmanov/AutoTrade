import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { UserContext } from '../Shared/User/UserContext';
import { Glyphicon, Nav, Navbar, NavItem } from 'react-bootstrap';
import { LinkContainer } from 'react-router-bootstrap';
import '../../styles/components/MainNavigation/MainNavigation.css';

import axios from 'axios';


class Navigation extends Component {
	logoutUser(e) {
		e.preventDefault();
		axios.get('/user/logout')
			.then(() => window.location.href = '/');
	}

	render() {
		return (<Navbar className="main-nav" inverse fixedTop fluid collapseOnSelect>
			<Navbar.Header>
				<Navbar.Brand>
					<Link to={'/'}>
						<Glyphicon glyph='road' /> AutoTrade
						</Link>
				</Navbar.Brand>
				<Navbar.Toggle />
			</Navbar.Header>
			<Navbar.Collapse>
				<Nav>
					<LinkContainer to={'/'} exact>
						<NavItem>
							<Glyphicon glyph='home' /> Home
							</NavItem>
					</LinkContainer>
					<LinkContainer to={'/search'}>
						<NavItem>
							<Glyphicon glyph='search' /> Search Vehicle
						</NavItem>
					</LinkContainer>


					<UserContext.Consumer>
						{user => {
							if (user) {
								//Private routes
								return <React.Fragment>
									<LinkContainer to={'/profile/home'}>
										<NavItem>
											<Glyphicon glyph='user' /> Profile
										</NavItem>
									</LinkContainer>

									{user.isAdmin &&
										<LinkContainer to={'/admin/users'}>
											<NavItem>
												<Glyphicon glyph='warning-sign' /> Administration
										</NavItem>
										</LinkContainer>}

									<LinkContainer to={'/logout'}>
										<NavItem onClick={this.logoutUser}>
											<Glyphicon glyph='off' /> Logout
										</NavItem>
									</LinkContainer>
								</React.Fragment>;
							} else {
								//Public routes
								return <React.Fragment>
									<LinkContainer to={'/register'}>
										<NavItem>
											<Glyphicon glyph='th-list' /> Register
										</NavItem>
									</LinkContainer>
									<LinkContainer to={'/login'}>
										<NavItem>
											<Glyphicon glyph='off' /> Login
										</NavItem>
									</LinkContainer>
									<LinkContainer to={'/forgotpassword'}>
										<NavItem>
											<Glyphicon glyph='refresh' /> Forgot Password
										</NavItem>
									</LinkContainer>
								</React.Fragment>
							}
						}}
					</UserContext.Consumer>
				</Nav>
			</Navbar.Collapse>
		</Navbar>);
	}
}

export default Navigation;