import React, { Component } from 'react';
import { Col, Row } from 'react-bootstrap';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { vehicleActionCreators } from '../Shared/Vehicle/store/Vehicle';
import { commonActionCreators } from '../Shared/Common/store/Common';
import * as types from '../Shared/Vehicle/store/types';
import * as commonTypes from '../Shared/Common/store/types';
import axios from 'axios';
import Navigation from '../Administration/Navigation';
import DisplayErrors from '../Shared/Error/Error';

class VehicleModels extends Component {
	state = {
		errors: [],
		isFormVisible: false,
		makeId: 0,
		typeId: 0,
	};

	componentDidMount() {
		this.props[types.GET_VEHICLE_MAKES]();
		this.props[commonTypes.GET_VEHICLE_TYPES]();
	}

	handleSubmit(e) {
		e.preventDefault();
		axios.post('/admin/addvehiclemodel', new FormData(e.target))
			.then(r => { return r.data })
			.then(response => {
				this.setState({ errors: response.errors });
				if (response.succeeded)
					this.props[types.GET_VEHICLE_MODELS](this.state.makeId, this.state.typeId);
			});
	}

	deleteModel(e) {
		e.preventDefault();
		axios.post('/admin/removevehiclemodel', new FormData(e.target))
			.then(r => { return r.data })
			.then(response => {
				this.setState({ errors: response.errors });
				if (response.succeeded)
					this.props[types.GET_VEHICLE_MODELS](this.state.makeId, this.state.typeId);
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

	selectType(e) {
		let type = e.target.value;
		this.props[types.GET_VEHICLE_MODELS](this.state.makeId, type);
		this.setState({ typeId: type });
	}

	render() {
		let vehicleModels;

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
						<div>
							<label>Make Name:</label>
							<br />
							<select onChange={this.selectMake.bind(this)} name="makeId" required className="form-control spacer">
								<option>Select Vehicle Make</option>
								{this.props.vehicleMakes.map((make, i) => {
									return (<option key={i} value={make.id}>{make.name}</option>)
								})}
							</select>
						</div>
						<br />
						{this.state.isFormVisible &&
							<React.Fragment>
								<div>
									<label>Vehicle Type:</label>
									<br />
									<select onChange={this.selectType.bind(this)} name="vehicleTypeId" required className="form-control spacer">
										<option>Select Vehicle Type</option>
										{this.props.vehicleTypes.map((type, i) => {
											return (<option key={i} value={type.id}>{type.name}</option>)
										})}
									</select>
								</div>

								<br />
								<div>
									<label>Model Name:</label>
									<br />
									<input name="name" type="text" autoComplete="off" required className="form-control spacer" />
								</div>

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
	state => Object.assign({}, state.vehicle, state.common),
	dispatch => bindActionCreators(Object.assign({}, vehicleActionCreators, commonActionCreators), dispatch)
)(VehicleModels);