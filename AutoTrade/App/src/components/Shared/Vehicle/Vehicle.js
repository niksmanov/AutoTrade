import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Col, Row } from 'react-bootstrap';
import { bindActionCreators } from 'redux';
import DisplayErrors from '../Error/Error';
import { vehicleActionCreators } from './store/Vehicle';
import * as types from './store/types';


class Vehicle extends Component {
	state = {
		errors: []
	};

	render() {
		return (<React.Fragment>
			<DisplayErrors errors={this.state.errors} />
		</React.Fragment>);
	}
}

export default connect(
	state => state.vehicle,
	dispatch => bindActionCreators(vehicleActionCreators, dispatch)
)(Vehicle);

