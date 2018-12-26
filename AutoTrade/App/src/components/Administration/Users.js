import React, { Component } from 'react';
import { Col, Row } from 'react-bootstrap';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { actionCreators } from '../Shared/User/store/User';
import * as types from '../Shared/User/store/types';
import axios from 'axios';
import Navigation from '../Administration/Navigation';
import DisplayErrors from '../Shared/Error/Error';

class Users extends Component {
	state = {
		errors: []
	};

	componentDidMount() {
		this.props[types.GET_ALL_USERS]();
	}

	handleSubmit(e) {
		e.preventDefault();
		axios.post('/user/changerole', new FormData(e.target))
			.then(r => { return r.data })
			.then(response => {
				if (response.succeeded) {
					this.setState({ errors: ['User role has been changed!'] });
					this.props[types.GET_ALL_USERS]();
				} else {
					this.setState({ errors: ['We have a problem with role changing'] });
				}
			});
	}

	deleteUser(userId) {
		axios.get(`/admin/removeuser?id=${userId}`
		).then(r => {
			return r.data
		}).then(response => {
			if (response.succeeded) {
				this.setState({ errors: ['Entity deleted successfully'] });
				this.props[types.GET_ALL_USERS]();
			} else {
				this.setState({ errors: ['We have a problem with deleting'] });
			}
		});
	}

	render() {
		let users;
		if (this.props.isLoading) {
			users =
				<React.Fragment>
					<div className="loading-app"></div>
				</React.Fragment>;
		} else {
			users = this.props.users.map((user, i) => {
				return (<div key={i} className="admin-entity">
					<button className="btn btn-danger" onClick={this.deleteUser.bind(this, user.id)}>X</button>
					<span>{user.email}</span>
					<hr />
				</div>);
			});
		}

		return (<React.Fragment>
			<Navigation />

			<Row>
				<Col sm={3}>
					<form onSubmit={this.handleSubmit.bind(this)}>
						<label>User Email or Name:</label>
						<br />
						<input name="name" type="text" autoComplete="off" required className="form-control spacer" />
						<br />
						<button type="submit" className="btn btn-primary">Search User</button>
					</form>
					<br />
					<DisplayErrors errors={this.state.errors} />
				</Col>
				<Col sm={9}>
					{users}
				</Col>
			</Row>
		</React.Fragment>);
	}
}

export default connect(
	state => state.user,
	dispatch => bindActionCreators(actionCreators, dispatch)
)(Users);