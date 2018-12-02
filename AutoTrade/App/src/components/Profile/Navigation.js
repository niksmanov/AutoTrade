import React from 'react';
import { Glyphicon, Nav, Navbar, NavItem } from 'react-bootstrap';
import { LinkContainer } from 'react-router-bootstrap';
import '../../styles/components/Profile/Navigation.css';

export default props => (
	<Navbar className="profile-nav">
		<Nav>
			<LinkContainer to={'/profile'}>
				<NavItem>
					<Glyphicon glyph='user' />
				</NavItem>
			</LinkContainer>
			<LinkContainer to={'/changepassword'}>
				<NavItem>
					<Glyphicon glyph='wrench' /> Change Password
          </NavItem>
			</LinkContainer>
		</Nav>
	</Navbar>
);
