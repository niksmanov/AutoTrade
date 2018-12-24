import React from 'react';
import { Glyphicon, Nav, Navbar, NavItem } from 'react-bootstrap';
import { LinkContainer } from 'react-router-bootstrap';
import '../../styles/components/Profile/Navigation.css';

export default props => (<Navbar className="profile-nav">
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
		<LinkContainer to={'/profile/addvehicle'}>
			<NavItem>
				<Glyphicon glyph='wrench' /> Add Vehicle
          </NavItem>
		</LinkContainer>
		<LinkContainer to={'/profile/listvehicles'}>
			<NavItem>
				<Glyphicon glyph='wrench' /> List Vehicles
          </NavItem>
		</LinkContainer>
	</Nav>
</Navbar>);
