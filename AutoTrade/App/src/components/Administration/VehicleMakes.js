import React, { Component } from 'react';
import { Col, Row } from 'react-bootstrap';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { actionCreators } from '../Shared/Vehicle/store/Vehicle';
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
				if (response.succeeded) {
					this.setState({ errors: ['Entity added successfully'] });
					window.location.reload();
				} else {
					this.setState({ errors: ['Entity already exists'] });
				}
			});
	}

	deleteMake(makeId) {
		axios.get(`/admin/removevehiclemake?id=${makeId}`
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
					<button className="btn btn-danger" onClick={this.deleteMake.bind(this, make.id)}>X</button>
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
						<input name="name" type="text" autoComplete="off" required />
						<br />
						<button type="submit" className="spacer">Add Make</button>
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
	dispatch => bindActionCreators(actionCreators, dispatch)
)(VehicleMakes);