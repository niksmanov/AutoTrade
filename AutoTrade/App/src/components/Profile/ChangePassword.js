import React, { PureComponent } from 'react';
import axios from 'axios';
import DisplayErrors from '../Shared/Error/Error';
import Navigation from './Navigation';

class ChangePassword extends PureComponent {
	state = {
		errors: []
	};

	handleSubmit(e) {
		e.preventDefault();
		axios.post('/user/resetpassword', new FormData(e.target))
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
				<Navigation />

				<form onSubmit={this.handleSubmit.bind(this)}>
					<label>Email:</label>
					<br />
					<input name="email" type="email" autoComplete="off" required />
					<br />
					<label>New Password:</label>
					<br />
					<input name="password" type="password" required />
					<br />
					<button type="submit" className="spacer">Submit</button>
				</form>
				<br />

				<DisplayErrors errors={this.state.errors} />
			</div>
		);
	}
}

export default ChangePassword;
