import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { actionCreators } from '../Shared/User/store/User';

class Profile extends Component {
	componentWillMount() {
		this.props.getUser();
	}

	render() {
		let user = this.props.user;

		return (
			<div>
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

