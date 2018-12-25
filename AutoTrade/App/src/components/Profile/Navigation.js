﻿import React from 'react';
import { Glyphicon, Nav, Navbar, NavItem } from 'react-bootstrap';
import { LinkContainer } from 'react-router-bootstrap';
import '../../styles/components/Profile/Navigation.css';

export default props => (<Navbar className="profile-nav">
	<Nav>
		<LinkContainer to={'/profile/home'}>
			<NavItem>
				<Glyphicon glyph='user' />
			</NavItem>
		</LinkContainer>
		<LinkContainer to={'/profile/changepassword'}>
			<NavItem>
				Change Password
          </NavItem>
		</LinkContainer>
		<LinkContainer to={'/profile/addvehicle'}>
			<NavItem>
				Add Vehicle
          </NavItem>
		</LinkContainer>
		<LinkContainer to={'/profile/listvehicles'}>
			<NavItem>
				List Vehicles
          </NavItem>
		</LinkContainer>
	</Nav>
</Navbar>);
