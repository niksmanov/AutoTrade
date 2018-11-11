import React from 'react';
import { Link } from 'react-router-dom';
import { Glyphicon, Nav, Navbar, NavItem } from 'react-bootstrap';
import { LinkContainer } from 'react-router-bootstrap';
import '../../styles/components/Navigation/NavMenu.css';

export default props => (
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
				<LinkContainer to={'/counter'}>
					<NavItem>
						<Glyphicon glyph='education' /> Counter
          </NavItem>
				</LinkContainer>
				<LinkContainer to={'/register'}>
					<NavItem>
						<Glyphicon glyph='education' /> Register
          </NavItem>
				</LinkContainer>
				<LinkContainer to={'/login'}>
					<NavItem>
						<Glyphicon glyph='education' /> Login
          </NavItem>
				</LinkContainer>

				<LinkContainer to={'/fetchdata'}>
					<NavItem>
						<Glyphicon glyph='th-list' /> Fetch data
          </NavItem>
				</LinkContainer>
				<LinkContainer to={'/profile'}>
					<NavItem>
						<Glyphicon glyph='th-list' /> Profile
          </NavItem>
				</LinkContainer>
			</Nav>
		</Navbar.Collapse>
	</Navbar>
);
