import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { Glyphicon, Nav, Navbar, NavItem } from 'react-bootstrap';
import { LinkContainer } from 'react-router-bootstrap';
import '../../styles/components/MainNavigation/NavMenu.css';

import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { actionCreators } from '../Shared/User/store/User';
import axios from 'axios';


class Navigation extends Component {
	componentWillMount() {
		this.props.getUser();
	}

	logoutUser(e) {
		e.preventDefault();
		axios.get('/user/logout')
			.then(() => window.location.href = '/');
	}

	render() {
		let user = this.props.user;

		let login;
		let register;
		let profile;
		let logout;
		let forgotPassword;

		if (user.userName) {
			profile = <LinkContainer to={'/profile'}>
				<NavItem>
					<Glyphicon glyph='user' /> Profile
							</NavItem>
			</LinkContainer>

			logout = <LinkContainer to={'/logout'}>
				<NavItem onClick={this.logoutUser}>
					<Glyphicon glyph='off' /> Logout
							</NavItem>
			</LinkContainer>
		} else {
			register = <LinkContainer to={'/register'}>
				<NavItem>
					<Glyphicon glyph='th-list' /> Register
							</NavItem>
			</LinkContainer>

			login = <LinkContainer to={'/login'}>
				<NavItem>
					<Glyphicon glyph='off' /> Login
							</NavItem>
			</LinkContainer>

			forgotPassword = <LinkContainer to={'/forgotpassword'}>
				<NavItem>
					<Glyphicon glyph='refresh' /> Forgot Password
							</NavItem>
			</LinkContainer>
		}

		return (
			<Navbar className="main-nav" inverse fixedTop fluid collapseOnSelect>
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
						{login}
						{register}
						{profile}
						{forgotPassword}
						{logout}
					</Nav>
				</Navbar.Collapse>
			</Navbar>
		);
	}
}

export default connect(
	state => state.user,
	dispatch => bindActionCreators(actionCreators, dispatch),
	null, { pure: false },
)(Navigation);