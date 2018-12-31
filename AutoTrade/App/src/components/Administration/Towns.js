import React, { Component } from 'react';
import { Col, Row } from 'react-bootstrap';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { commonActionCreators } from '../Shared/Common/store/Common';
import * as types from '../Shared/Common/store/types';
import axios from 'axios';
import Navigation from '../Administration/Navigation';
import DisplayErrors from '../Shared/Error/Error';

class Towns extends Component {
	state = {
		errors: []
	};

	componentDidMount() {
		this.props[types.GET_TOWNS]();
	}

	handleSubmit(e) {
		e.preventDefault();
		axios.post('/admin/addtown', new FormData(e.target))
			.then(r => { return r.data })
			.then(response => {
				if (response.succeeded) {
					this.setState({ errors: ['Entity added successfully'] });
					this.props[types.GET_TOWNS]();
				} else {
					this.setState({ errors: ['Entity already exists'] });
				}
			});
	}

	deleteTown(e) {
		e.preventDefault();
		axios.post('/admin/removetown', new FormData(e.target))
			.then(r => {
				return r.data
			}).then(response => {
				if (response.succeeded) {
					this.setState({ errors: ['Entity deleted successfully'] });
					this.props[types.GET_TOWNS]();
				} else {
					this.setState({ errors: ['We have a problem with deleting'] });
				}
			});
	}

	render() {
		let towns;
		if (this.props.isLoading) {
			towns =
				<React.Fragment>
					<div className="loading-app"></div>
				</React.Fragment>;
		} else {
			towns = this.props.towns.map((town, i) => {
				return (<div key={i} className="admin-entity">
					<form onSubmit={this.deleteTown.bind(this)} className="delete-btn-form">
						<input name="id" type="hidden" value={town.id} />
						<button type="submit" className="btn btn-danger">X</button>
					</form>
					<span>{town.name}</span>
					<hr />
				</div>);
			});
		}

		return (<React.Fragment>
			<Navigation />

			<Row>
				<Col sm={3}>
					<form onSubmit={this.handleSubmit.bind(this)}>
						<label>Town Name:</label>
						<br />
						<input name="name" type="text" autoComplete="off" required className="form-control spacer" />
						<br />
						<button type="submit" className="btn btn-primary">Add Town</button>
					</form>
					<br />
					<DisplayErrors errors={this.state.errors} />
				</Col>
				<Col sm={9}>
					{towns}
				</Col>
			</Row>
		</React.Fragment>);
	}
}

export default connect(
	state => state.common,
	dispatch => bindActionCreators(commonActionCreators, dispatch)
)(Towns);