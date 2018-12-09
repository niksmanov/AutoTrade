﻿import React, { PureComponent } from 'react';
import axios from 'axios';
import DisplayErrors from '../Shared/Error/Error';

class ForgotPassword extends PureComponent {
	state = {
		errors: []
	};

	handleSubmit(e) {
		e.preventDefault();
		axios.post('/user/forgotpassword', new FormData(e.target))
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
		return (<React.Fragment>
			<form onSubmit={this.handleSubmit.bind(this)}>
				<label>Email:</label>
				<br />
				<input name="email" type="email" autoComplete="off" required />
				<br />
				<button type="submit" className="spacer">Submit</button>
			</form>
			<br />

			<DisplayErrors errors={this.state.errors} />
		</React.Fragment>);
	}
}

export default ForgotPassword;
