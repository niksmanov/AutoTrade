import React, { Component } from 'react';
import * as formModule from '../../modules/form-module';

class Register extends Component {
	render() {
		return (
			<form onSubmit={this.handleSubmit}>
				<label>
					Email:
				</label>
				<input name="email" type="email" />
				<br />
				<label>
					Password:
				</label>
				<input name="password" type="password" />
				<br />
				<button type="submit">Submit</button>
			</form>
		);
	}

	constructor(props) {
		super(props);

		this.state = {
			email: '',
			password: '',
		};

		this.handleSubmit = this.handleSubmit.bind(this);
	}

	handleSubmit(e) {
		e.preventDefault();
		const url = '/account/register';
		let callback = function (data) { console.log(data); }
		formModule.sendForm(url, 'POST', e.target, callback);
	}
}

export default Register;
