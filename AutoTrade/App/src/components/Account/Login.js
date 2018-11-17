import React, { Component } from 'react';
import axios from 'axios';
import DisplayErrors from '../Shared/Error/Error';

class Login extends Component {
	constructor() {
		super();
		this.state = {
			errors: []
		};
	}

	handleSubmit(e) {
		e.preventDefault();
		axios.post('/user/login', new FormData(e.target))
			.then(r => { return r.data })
			.then(response => {
				if (response.succeeded) {
					window.location.href = '/';
				} else {
					this.setState({ errors: response.errors });
				}
			});
	}

	render() {
		return (
			<div>
				<form onSubmit={this.handleSubmit.bind(this)}>
					<label>Email:</label>
					<input name="email" type="email" autoComplete="off" />
					<br />
					<label>Password:</label>
					<input name="password" type="password" />
					<br />
					<label>Remember me:</label>
					<span>Yes</span>
					<input type="radio" name="rememberMe" value="true" defaultChecked />
					<span>No</span>
					<input type="radio" name="rememberMe" value="false" />
					<br />
					<button type="submit">Submit</button>
				</form>
				<br />

				<DisplayErrors errors={this.state.errors} />
			</div>
		);
	}
}

export default Login;
