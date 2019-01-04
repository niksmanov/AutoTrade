import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { vehicleActionCreators } from '../Shared/Vehicle/store/Vehicle';
import { UserContext } from '../Shared/User/UserContext';
import Navigation from './Navigation';
import VehicleForm from '../Shared/Vehicle/Form';
import * as types from '../Shared/Vehicle/store/types';
import DisplayErrors from '../Shared/Error/Error';
import axios from 'axios';


class EditVehicle extends Component {
	state = {
		errors: [],
	};

	componentDidMount() {
		this.props[types.GET_VEHICLE](this.props.match.params.id);
	}

	handleSubmit(e) {
		e.preventDefault();
		var formdata = new FormData(e.target);
		formdata.append('id', this.props.match.params.id);

		axios.post('/profile/editvehicle', formdata)
			.then(r => { return r.data })
			.then(response => {
				this.setState({ errors: response.errors });
				if (response.succeeded)
					window.location.href = `/vehicle/${response.data}`;
			});
	}

	render() {
		return (<React.Fragment>
			<Navigation />
			<VehicleForm vehicle={this.props.vehicle} handleSubmit={this.handleSubmit.bind(this)} />
			<br />
			<DisplayErrors errors={this.state.errors} />
		</React.Fragment>);
	}
}

EditVehicle.contextType = UserContext;
export default connect(
	state => state.vehicle,
	dispatch => bindActionCreators(vehicleActionCreators, dispatch)
)(EditVehicle);