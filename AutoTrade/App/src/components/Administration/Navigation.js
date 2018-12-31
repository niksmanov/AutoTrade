import React from 'react';
import { Nav, Navbar, NavItem } from 'react-bootstrap';
import { LinkContainer } from 'react-router-bootstrap';
import '../../styles/components/Administration/Common.css';
import '../../styles/components/Profile/Navigation.css';


export default props => (<Navbar className="profile-nav">
	<Nav>
		<LinkContainer to={'/admin/users'}>
			<NavItem>
				Users
          </NavItem>
		</LinkContainer>
		<LinkContainer to={'/admin/makes'}>
			<NavItem>
				Vehicle Makes
          </NavItem>
		</LinkContainer>
		<LinkContainer to={'/admin/models'}>
			<NavItem>
				Vehicle Models
          </NavItem>
		</LinkContainer>
		<LinkContainer to={'/admin/vehicletypes'}>
			<NavItem>
				Vehicle Types
          </NavItem>
		</LinkContainer>
		<LinkContainer to={'/admin/fueltypes'}>
			<NavItem>
				Fuel Types
          </NavItem>
		</LinkContainer>
		<LinkContainer to={'/admin/gearboxtypes'}>
			<NavItem>
				Gearbox Types
          </NavItem>
		</LinkContainer>
		<LinkContainer to={'/admin/towns'}>
			<NavItem>
				Towns
          </NavItem>
		</LinkContainer>
		<LinkContainer to={'/admin/colors'}>
			<NavItem>
				Colors
          </NavItem>
		</LinkContainer>
	</Nav>
</Navbar>);
