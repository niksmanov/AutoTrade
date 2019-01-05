import React, { Component } from 'react';
import { Col, Row } from 'react-bootstrap';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { userActionCreators } from '../Shared/User/store/User';
import * as types from '../Shared/User/store/types';
import axios from 'axios';
import InfiniteScroll from 'react-infinite-scroller';
import Navigation from '../Administration/Navigation';
import DisplayErrors from '../Shared/Error/Error';

class Users extends Component {
	state = {
		page: 0,
		size: 10,
		hasMore: true,
		errors: [],
	};

	componentDidMount() {
		this.props[types.CLEAR_STATE]();
		this.props[types.GET_USERS](this.state.page, this.state.size);
	}

	loadMore() {
		if (this.props.users.length === (this.state.page + 1) * this.state.size) {
			this.setState({ page: this.state.page + 1 }, () => {
				this.props[types.GET_USERS](this.state.page, this.state.size);
			});
		}
	}

	changeRole(e) {
		e.preventDefault();
		axios.post('/admin/changerole', new FormData(e.target))
			.then(r => { return r.data })
			.then(response => {
				this.setState({ errors: response.errors });
				if (response.succeeded) {
					this.setState({ page: 0 }, () => {
						this.props[types.CLEAR_STATE]();
						this.props[types.GET_USERS](this.state.page, this.state.size);
					});
				}
			});
	}

	deleteUser(e) {
		e.preventDefault();
		axios.post('/admin/removeuser', new FormData(e.target))
			.then(r => { return r.data })
			.then(response => {
				this.setState({ errors: response.errors });
				if (response.succeeded) {
					this.setState({ page: 0 }, () => {
						this.props[types.CLEAR_STATE]();
						this.props[types.GET_USERS](this.state.page, this.state.size);
					});
				}
			});
	}

	searchUser(e) {
		e.preventDefault();
		let search = new FormData(e.target).get('search');
		this.setState({ page: 0 }, () => {
			this.props[types.CLEAR_STATE]();
			this.props[types.GET_USERS](this.state.page, this.state.size, search);
		});
	}

	render() {
		let users;
		if (!this.props.isLoading) {
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
						<form onSubmit={this.deleteUser.bind(this)} className="delete-btn-form">
							<input name="id" type="hidden" value={user.id} />
							<button type="submit" className="btn btn-danger">X</button>
						</form>
					</td>
				</tr>);
			});
		}

		return (<React.Fragment>
			<Navigation />

			<Row>
				<Col sm={3}>
					<form onSubmit={this.searchUser.bind(this)}>
						<label>Username or Email:</label>
						<br />
						<input type="text" name="search" autoComplete="off" className="form-control spacer" />
						<button type="submit" className="btn btn-primary">Submit</button>
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
						<InfiniteScroll
							element={'tbody'}
							pageStart={0}
							loadMore={this.loadMore.bind(this)}
							hasMore={this.state.hasMore}
							loader={<tr key={0}><td>Loading...</td></tr>}>
							<React.Fragment>
								{users}
							</React.Fragment>
						</InfiniteScroll>
					</table>
				</Col>
			</Row>
		</React.Fragment >);
	}
}

export default connect(
	state => state.user,
	dispatch => bindActionCreators(userActionCreators, dispatch)
)(Users);