﻿import React, { Component } from 'react';
import { Col, Row } from 'react-bootstrap';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { actionCreators } from '../Shared/Vehicle/store/Vehicle';
import * as types from '../Shared/Vehicle/store/types';
import axios from 'axios';
import Navigation from '../Administration/Navigation';
import DisplayErrors from '../Shared/Error/Error';

class VehicleModels extends Component {
	state = {
		errors: [],
		isFormVisible: false,
		makeId: 0,
	};

	componentDidMount() {
		this.props[types.GET_VEHICLE_MAKES]();
	}

	handleSubmit(e) {
		e.preventDefault();
		axios.post('/admin/addvehiclemodel', new FormData(e.target))
			.then(r => { return r.data })
			.then(response => {
				if (response.succeeded) {
					this.setState({ errors: ['Entity added successfully'] });
					this.props[types.GET_VEHICLE_MODELS](this.state.makeId);
				} else {
					this.setState({ errors: ['Entity already exists'] });
				}
			});
	}

	deleteModel(e) {
		e.preventDefault();
		axios.post('/admin/removevehiclemodel', new FormData(e.target))
			.then(r => {
				return r.data
			}).then(response => {
				if (response.succeeded) {
					this.setState({ errors: ['Entity deleted successfully'] });
					this.props[types.GET_VEHICLE_MODELS](this.state.makeId);
				} else {
					this.setState({ errors: ['We have a problem with deleting'] });
				}
			});
	}

	selectMake(e) {
		let id = e.target.value;
		if (id > 0) {
			this.props[types.GET_VEHICLE_MODELS](id);
			this.setState({ isFormVisible: true });
			this.setState({ makeId: id });
		} else {
			this.setState({ isFormVisible: false });
		}
	}

	render() {
		let vehicleModels;
		let vehicleMakes;

		vehicleMakes = <select onChange={this.selectMake.bind(this)} name="makeId" className="form-control spacer">
			<option>Select Vehicle Make</option>
			{this.props.vehicleMakes.map((make, i) => {
				return (<option key={i} value={make.id}>{make.name}</option>)
			})}
		</select>

		if (this.props.isLoading) {
			vehicleModels =
				<React.Fragment>
					<div className="loading-app"></div>
				</React.Fragment>;
		} else {
			vehicleModels = this.props.vehicleModels.map((model, i) => {
				return (<div key={i} className="admin-entity">
					<form onSubmit={this.deleteModel.bind(this)} className="delete-btn-form">
						<input name="id" type="hidden" value={model.id} />
						<button type="submit" className="btn btn-danger">X</button>
					</form>
					<span>{model.name}</span>
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
						{vehicleMakes}
						<br />
						{this.state.isFormVisible &&
							<React.Fragment>
								<label>Model Name:</label>
								<br />
								<input name="name" type="text" autoComplete="off" required className="form-control spacer" />
								<br />
								<button type="submit" className="btn btn-primary">Add Model</button>
							</React.Fragment>}
					</form>
					<br />
					<DisplayErrors errors={this.state.errors} />
				</Col>
				<Col sm={9}>
					{vehicleModels}
				</Col>
			</Row>
		</React.Fragment>);
	}
}

export default connect(
	state => state.vehicle,
	dispatch => bindActionCreators(actionCreators, dispatch)
)(VehicleModels);