import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { actionCreators } from '../Shared/User/store/User';
import Navigation from './Navigation';

class Profile extends Component {
	componentWillMount() {
		this.props.getUser();
	}

	render() {
		let user = this.props.user;

		return (
			<div>
				<Navigation />

				<p> Hello, {user.userName} </p>
				<br />
			</div>
		);
	}
}

export default connect(
	state => state.user,
	dispatch => bindActionCreators(actionCreators, dispatch)
)(Profile);

