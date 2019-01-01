import React, { Component } from 'react';
import { UserContext } from '../Shared/User/UserContext';
import Navigation from './Navigation';
import VehicleList from '../Shared/Vehicle/List';

class Vehicles extends Component {
	state = {
		errors: []
	};

	render() {
		return (<UserContext.Consumer>
			{user =>
				<React.Fragment>
					<Navigation />
					<VehicleList userId={user.id} />
				</React.Fragment>}
		</UserContext.Consumer>);
	}
}

export default Vehicles;
