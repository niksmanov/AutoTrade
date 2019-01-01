import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { vehicleActionCreators } from '../Shared/Vehicle/store/Vehicle';
import axios from 'axios';
import { UserContext } from '../Shared/User/UserContext';
import Navigation from './Navigation';
import VehicleForm from '../Shared/Vehicle/Form';

class AddVehicle extends Component {
	state = {
		model: {},
	};

	componentDidMount() {
		this.getVehicle();
	}

	getVehicle() {
		axios.get('/vehicle/getvehicle'
		).then(r => {
			return r.data
		}).then(response => {
			if (response.succeeded) {
				this.setState({ model: response.data });
			}
		});
	}

	render() {
		return (<UserContext.Consumer>
			{user =>
				<React.Fragment>
					<Navigation />
					<VehicleForm userId={user.id} vehicle={this.state.model} />
				</React.Fragment>}
		</UserContext.Consumer>);
	}
}

export default connect(
	state => state.vehicle,
	dispatch => bindActionCreators(vehicleActionCreators, dispatch)
)(AddVehicle);