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

	changeRole(e) {
		e.preventDefault();
		axios.post('/admin/changerole', new FormData(e.target))
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

	searchUser(e) {
		let search = e.target.value;
		this.props[types.GET_ALL_USERS](search);
	}

	render() {
		let users;
		let isLoading;
		if (this.props.isLoading) {
			isLoading =
				<React.Fragment>
					<div className="loading-app"></div>
				</React.Fragment>;
		} else {
			users = this.props.users.map((user, i) => {
				return (<tr key={i}>
					<td>{user.email}</td>
					<td>{user.userName}</td>
					<td>{user.isAdmin ? 'Admin' : 'User'}</td>
					<td>
						<form onSubmit={this.changeRole.bind(this)} >
							<input type="hidden" name="isAdmin" value={!user.isAdmin} />
							<input type="hidden" name="id" value={user.id} />
							<button type="submit" className="btn btn-default">{user.isAdmin ? 'Make User' : 'Make Admin'}</button>
						</form>
					</td>
					<td>
						<button className="btn btn-danger" onClick={this.deleteUser.bind(this, user.id)}>X</button>
					</td>
				</tr>);
			});
		}

		return (<React.Fragment>
			<Navigation />

			<Row>
				<Col sm={3}>
					<form>
						<label>Username or Email:</label>
						<br />
						<input onChange={this.searchUser.bind(this)} type="text" autoComplete="off" required className="form-control spacer" />
					</form>
					<br />
					<DisplayErrors errors={this.state.errors} />
				</Col>
				<Col sm={9} style={{ overflowX: 'scroll' }}>
					<table className="table">
						<thead>
							<tr>
								<th scope="col">Email</th>
								<th scope="col">Username</th>
								<th scope="col">Role</th>
								<th scope="col">Change Role</th>
								<th scope="col">Delete?</th>
							</tr>
						</thead>
						<tbody>
							{users}
						</tbody>
					</table>
					{isLoading}
				</Col>
			</Row>
		</React.Fragment >);
	}
}

export default connect(
	state => state.user,
	dispatch => bindActionCreators(actionCreators, dispatch)
)(Users);