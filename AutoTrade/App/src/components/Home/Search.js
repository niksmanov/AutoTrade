import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { vehicleActionCreators } from '../Shared/Vehicle/store/Vehicle';
import * as types from '../Shared/Vehicle/store/types';
import DisplayErrors from '../Shared/Error/Error';
import VehicleList from '../Shared/Vehicle/List';


class Search extends Component {
	state = {
		errors: [],
	};

	showAllVehicles() {
		this.props[types.GET_VEHICLES]();
	}

	render() {
		return (<React.Fragment>
			<button onClick={this.showAllVehicles.bind(this)} className="btn btn-default spacer">Show all vehicles</button>
			<VehicleList vehicles={this.props.vehicles} />
			<DisplayErrors errors={this.state.errors} />
		</React.Fragment>);
	}
}

export default connect(
	state => state.vehicle,
	dispatch => bindActionCreators(vehicleActionCreators, dispatch)
)(Search);

