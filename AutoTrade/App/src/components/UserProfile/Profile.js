import React, { Component } from 'react';
import axios from 'axios';
import DisplayErrors from '../Shared/Error/Error';

class Profile extends Component {
	render() {
		let logout;
		if (this.state.userName.length > 0) {
			logout = <a href="/user/logout">
				<button>
					Logout
				</button>
			</a>
		}

		return (
			<div>
				<p> Hello {this.state.userName} </p>
				<br />

				<DisplayErrors errors={this.state.errors} />

				{logout}
			</div>
		);
	}

	state = {
		errors: [],
		userName: ''
	}

	componentWillMount() {
		axios.get('/user/current')
			.then(r => { return r.data })
			.then(response => {
				if (!response.succeeded) {
					this.setState({ errors: response.errors });
				} else {
					this.setState({ userName: response.data.userName });
				}
			});
	}

}

export default Profile;
