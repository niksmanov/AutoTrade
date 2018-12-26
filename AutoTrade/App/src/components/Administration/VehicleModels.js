import React, { Component } from 'react';
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
					window.location.reload();
				} else {
					this.setState({ errors: ['Entity already exists'] });
				}
			});
	}

	deleteModel(modelId) {
		axios.get(`/admin/removevehiclemodel?id=${modelId}`
		).then(r => {
			return r.data
		}).then(response => {
			if (response.succeeded) {
				this.setState({ errors: ['Entity deleted successfully'] });
				window.location.reload();
			} else {
				this.setState({ errors: ['We have a problem with deleting'] });
			}
		});
	}

	selectMake(e) {
		let makeId = e.target.value;
		if (makeId > 0) {
			this.props[types.GET_VEHICLE_MODELS](makeId);
			this.setState({ isFormVisible: true });
		} else {
			this.setState({ isFormVisible: false });
		}
	}

	render() {
		let vehicleModels;
		let vehicleMakes;

		vehicleMakes = <select onChange={this.selectMake.bind(this)} name="makeId">
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
					<button className="btn btn-danger" onClick={this.deleteModel.bind(this, model.id)}>X</button>
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
						<br />
						{this.state.isFormVisible &&
							<React.Fragment>
								<label>Model Name:</label>
								<br />
								<input name="name" type="text" autoComplete="off" required />
								<br />
								<button type="submit" className="spacer">Add Model</button>
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