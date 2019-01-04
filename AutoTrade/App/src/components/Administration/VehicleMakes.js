import React, { Component } from 'react';
import { Col, Row } from 'react-bootstrap';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { vehicleActionCreators } from '../Shared/Vehicle/store/Vehicle';
import * as types from '../Shared/Vehicle/store/types';
import axios from 'axios';
import Navigation from '../Administration/Navigation';
import DisplayErrors from '../Shared/Error/Error';

class VehicleMakes extends Component {
	state = {
		errors: []
	};

	componentDidMount() {
		this.props[types.GET_VEHICLE_MAKES]();
	}

	handleSubmit(e) {
		e.preventDefault();
		axios.post('/admin/addvehiclemake', new FormData(e.target))
			.then(r => { return r.data })
			.then(response => {
				this.setState({ errors: response.errors });
				if (response.succeeded)
					this.props[types.GET_VEHICLE_MAKES]();
			});
	}

	deleteMake(e) {
		e.preventDefault();
		axios.post('/admin/removevehiclemake', new FormData(e.target))
			.then(r => { return r.data })
			.then(response => {
				this.setState({ errors: response.errors });
				if (response.succeeded)
					this.props[types.GET_VEHICLE_MAKES]();
			});
	}

	render() {
		let vehicleMakes;
		if (this.props.isLoading) {
			vehicleMakes =
				<React.Fragment>
					<div className="loading-app"></div>
				</React.Fragment>;
		} else {
			vehicleMakes = this.props.vehicleMakes.map((make, i) => {
				return (<div key={i} className="admin-entity">
					<form onSubmit={this.deleteMake.bind(this)} className="delete-btn-form">
						<input name="id" type="hidden" value={make.id} />
						<button type="submit" className="btn btn-danger">X</button>
					</form>
					<span>{make.name}</span>
					<hr />
				</div>);
			});
		}

		return (<React.Fragment>
			<Navigation />

			<Row>
				<Col sm={3}>
					<form onSubmit={this.handleSubmit.bind(this)}>
						<label>Make Name:</label>
						<br />
						<input name="name" type="text" autoComplete="off" required className="form-control spacer" />
						<br />
						<button type="submit" className="btn btn-primary">Add Make</button>
					</form>
					<br />
					<DisplayErrors errors={this.state.errors} />
				</Col>
				<Col sm={9}>
					{vehicleMakes}
				</Col>
			</Row>
		</React.Fragment>);
	}
}

export default connect(
	state => state.vehicle,
	dispatch => bindActionCreators(vehicleActionCreators, dispatch)
)(VehicleMakes);