import React, { Component } from 'react';
import { Col, Row } from 'react-bootstrap';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { commonActionCreators } from '../Shared/Common/store/Common';
import * as types from '../Shared/Common/store/types';
import axios from 'axios';
import Navigation from '../Administration/Navigation';
import DisplayErrors from '../Shared/Error/Error';

class Colors extends Component {
	state = {
		errors: []
	};

	componentDidMount() {
		this.props[types.GET_COLORS]();
	}

	handleSubmit(e) {
		e.preventDefault();
		axios.post('/admin/addcolor', new FormData(e.target))
			.then(r => { return r.data })
			.then(response => {
				if (response.succeeded) {
					this.setState({ errors: ['Entity added successfully'] });
					this.props[types.GET_COLORS]();
				} else {
					this.setState({ errors: ['Entity already exists'] });
				}
			});
	}

	deleteColor(e) {
		e.preventDefault();
		axios.post('/admin/removecolor', new FormData(e.target))
			.then(r => {
				return r.data
			}).then(response => {
				if (response.succeeded) {
					this.setState({ errors: ['Entity deleted successfully'] });
					this.props[types.GET_COLORS]();
				} else {
					this.setState({ errors: ['We have a problem with deleting'] });
				}
			});
	}

	render() {
		let colors;
		if (this.props.isLoading) {
			colors =
				<React.Fragment>
					<div className="loading-app"></div>
				</React.Fragment>;
		} else {
			colors = this.props.colors.map((color, i) => {
				return (<div key={i} className="admin-entity">
					<form onSubmit={this.deleteColor.bind(this)} className="delete-btn-form">
						<input name="id" type="hidden" value={color.id} />
						<button type="submit" className="btn btn-danger">X</button>
					</form>
					<span>{color.name}</span>
					<hr />
				</div>);
			});
		}

		return (<React.Fragment>
			<Navigation />

			<Row>
				<Col sm={3}>
					<form onSubmit={this.handleSubmit.bind(this)}>
						<label>Color Name:</label>
						<br />
						<input name="name" type="text" autoComplete="off" required className="form-control spacer" />
						<br />
						<button type="submit" className="btn btn-primary">Add Color</button>
					</form>
					<br />
					<DisplayErrors errors={this.state.errors} />
				</Col>
				<Col sm={9}>
					{colors}
				</Col>
			</Row>
		</React.Fragment>);
	}
}

export default connect(
	state => state.common,
	dispatch => bindActionCreators(commonActionCreators, dispatch)
)(Colors);