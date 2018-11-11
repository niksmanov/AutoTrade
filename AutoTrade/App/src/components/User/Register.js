import React, { Component } from 'react';
import axios from 'axios';
import DisplayErrors from '../Shared/Error/Error';

class Register extends Component {
	render() {
		return (
			<div>
				<form onSubmit={this.handleSubmit}>
					<label>Email:</label>
					<input name="email" type="email" autoComplete="off" />
					<br />
					<label>Username:</label>
					<input name="userName" type="text" autoComplete="off" />
					<br />
					<label>Password:</label>
					<input name="password" type="password" />
					<br />
					<button type="submit">Submit</button>
				</form>
				<br />

				<DisplayErrors errors={this.state.errors} />
			</div>
		);
	}

	state = {
		errors: [],
	}

	constructor(props) {
		super(props);
		this.handleSubmit = this.handleSubmit.bind(this);
	}

	handleSubmit(e) {
		e.preventDefault();
		axios.post('/user/register', new FormData(e.target))
			.then(r => { return r.data })
			.then(response => {
				this.setState({ errors: response.errors });
			});
	}
}

export default Register;
