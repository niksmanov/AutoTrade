import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { vehicleActionCreators } from '../Shared/Vehicle/store/Vehicle';
import * as types from '../Shared/Vehicle/store/types';
import VehicleList from '../Shared/Vehicle/List';


class Home extends Component {
	state = {
		page: 0,
		size: 10,
	};

	componentDidMount() {
		this.props[types.CLEAR_STATE]();
		this.props[types.GET_VEHICLES](this.state.page, this.state.size);
	}


	render() {
		let vehicleList;
		if (this.props.isLoading) {
			vehicleList =
				<React.Fragment>
					<div className="loading-app"></div>
				</React.Fragment>;
		} else {
			vehicleList =
				<VehicleList vehicles={this.props.vehicles} />
		}

		return (<React.Fragment>
			<h3 style={{ textAlign: 'center', fontWeight: '800' }}> Last added vehicles </h3>
			<br />
			{vehicleList}
			<br />
			<p style={{ textAlign: 'center' }} className="spacer">
				<b><a href="https://gitlab.com/niksmanov/autotrade" rel="noopener noreferrer" target="_blank">Project Repository</a></b>
			</p>
		</React.Fragment >);
	}
}

export default connect(
	state => state.vehicle,
	dispatch => bindActionCreators(vehicleActionCreators, dispatch)
)(Home);