import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { vehicleActionCreators } from '../Shared/Vehicle/store/Vehicle';
import * as types from '../Shared/Vehicle/store/types';
import { UserContext } from '../Shared/User/UserContext';
import Navigation from './Navigation';
import VehicleList from '../Shared/Vehicle/List';


class Vehicles extends Component {
	componentDidMount() {
		this.props[types.GET_VEHICLES](this.context.id);
	}

	render() {
		return (<React.Fragment>
			<Navigation />
			<VehicleList vehicles={this.props.vehicles} />
		</React.Fragment>);
	}
}

Vehicles.contextType = UserContext;
export default connect(
	state => state.vehicle,
	dispatch => bindActionCreators(vehicleActionCreators, dispatch)
)(Vehicles);

