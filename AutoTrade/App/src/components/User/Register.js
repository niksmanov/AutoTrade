import React, { Component } from 'react';
import axios from 'axios';

class Register extends Component {
	render() {
		let errorMessages = [];

		if (this.state.errors) {
			errorMessages = this.state.errors.map((err, i) => {
				return <p key={i}> {err.description} </p>
			});
		}

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
				<div>
					{errorMessages}
				</div>
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
				if (!response.succeeded) {
					this.setState({ errors: response.errors });
				}
			});
	}
}

export default Register;
