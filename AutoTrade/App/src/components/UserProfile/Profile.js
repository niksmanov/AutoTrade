import React, { Component } from 'react';
import axios from 'axios';

class Profile extends Component {
	render() {

		let errorMessages = [];
		let logout;

		if (this.state.errors) {
			errorMessages = this.state.errors.map((err, i) => {
				return <p key={i}> {err.description} </p>
			});
		}

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
				<div>
					{errorMessages}
				</div>

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
