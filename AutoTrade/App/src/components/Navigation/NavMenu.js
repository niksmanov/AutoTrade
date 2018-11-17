import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { Glyphicon, Nav, Navbar, NavItem } from 'react-bootstrap';
import { LinkContainer } from 'react-router-bootstrap';
import '../../styles/components/Navigation/NavMenu.css';

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

		if (user.userName) {
			profile = <LinkContainer to={'/profile'}>
				<NavItem>
					<Glyphicon glyph='th-list' /> Profile
							</NavItem>
			</LinkContainer>

			logout = <NavItem onClick={this.logoutUser.bind(this)}>
				<Glyphicon glyph='th-list' /> Logout
							</NavItem>
		} else {
			register = <LinkContainer to={'/register'}>
				<NavItem>
					<Glyphicon glyph='education' /> Register
							</NavItem>
			</LinkContainer>

			login = <LinkContainer to={'/login'}>
				<NavItem>
					<Glyphicon glyph='th-list' /> Login
							</NavItem>
			</LinkContainer>
		}

		return (
			<Navbar inverse fixedTop fluid collapseOnSelect>
				<Navbar.Header>
					<Navbar.Brand>
						<Link to={'/'}>AutoTrade</Link>
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
						{logout}
					</Nav>
				</Navbar.Collapse>
			</Navbar>
		);
	}
}

export default connect(
	state => state.user,
	dispatch => bindActionCreators(actionCreators, dispatch)
)(Navigation);