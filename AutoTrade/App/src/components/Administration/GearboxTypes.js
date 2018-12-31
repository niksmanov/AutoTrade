import React, { Component } from 'react';
import { Col, Row } from 'react-bootstrap';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { commonActionCreators } from '../Shared/Common/store/Common';
import * as types from '../Shared/Common/store/types';
import axios from 'axios';
import Navigation from '../Administration/Navigation';
import DisplayErrors from '../Shared/Error/Error';

class GearboxTypes extends Component {
	state = {
		errors: []
	};

	componentDidMount() {
		this.props[types.GET_GEARBOX_TYPES]();
	}

	handleSubmit(e) {
		e.preventDefault();
		axios.post('/admin/addgearboxtype', new FormData(e.target))
			.then(r => { return r.data })
			.then(response => {
				if (response.succeeded) {
					this.setState({ errors: ['Entity added successfully'] });
					this.props[types.GET_GEARBOX_TYPES]();
				} else {
					this.setState({ errors: ['Entity already exists'] });
				}
			});
	}

	deleteGearboxType(e) {
		e.preventDefault();
		axios.post('/admin/removegearboxtype', new FormData(e.target))
			.then(r => {
				return r.data
			}).then(response => {
				if (response.succeeded) {
					this.setState({ errors: ['Entity deleted successfully'] });
					this.props[types.GET_GEARBOX_TYPES]();
				} else {
					this.setState({ errors: ['We have a problem with deleting'] });
				}
			});
	}

	render() {
		let gearboxTypes;
		if (this.props.isLoading) {
			gearboxTypes =
				<React.Fragment>
					<div className="loading-app"></div>
				</React.Fragment>;
		} else {
			gearboxTypes = this.props.gearboxTypes.map((type, i) => {
				return (<div key={i} className="admin-entity">
					<form onSubmit={this.deleteGearboxType.bind(this)} className="delete-btn-form">
						<input name="id" type="hidden" value={type.id} />
						<button type="submit" className="btn btn-danger">X</button>
					</form>
					<span>{type.name}</span>
					<hr />
				</div>);
			});
		}

		return (<React.Fragment>
			<Navigation />

			<Row>
				<Col sm={3}>
					<form onSubmit={this.handleSubmit.bind(this)}>
						<label>Type Name:</label>
						<br />
						<input name="name" type="text" autoComplete="off" required className="form-control spacer" />
						<br />
						<button type="submit" className="btn btn-primary">Add Type</button>
					</form>
					<br />
					<DisplayErrors errors={this.state.errors} />
				</Col>
				<Col sm={9}>
					{gearboxTypes}
				</Col>
			</Row>
		</React.Fragment>);
	}
}

export default connect(
	state => state.common,
	dispatch => bindActionCreators(commonActionCreators, dispatch)
)(GearboxTypes);