import React, { PureComponent } from 'react';
import { Redirect } from 'react-router';
import axios from 'axios';
import DisplayErrors from '../Shared/Error/Error';
import Navigation from '../Profile/Navigation';

class ChangePassword extends PureComponent {
	state = {
		errors: []
	};

	handleSubmit(e) {
		e.preventDefault();
		axios.post('/user/resetpassword', new FormData(e.target))
			.then(r => { return r.data })
			.then(response => {
				this.setState({ errors: response.errors });
			});
	}

	render() {
		return (<React.Fragment>
			<Navigation />

			<form onSubmit={this.handleSubmit.bind(this)}>
				<label>Email:</label>
				<input name="email" type="email" autoComplete="off" required className="form-control spacer" />
				<label>Old Password:</label>
				<input name="oldPassword" type="password" required className="form-control spacer" />
				<label>New Password:</label>
				<input name="password" type="password" required className="form-control spacer" />
				<br />
				<button type="submit" className="btn btn-primary">Submit </button>
			</form>
			<br />

			<DisplayErrors errors={this.state.errors} />
		</React.Fragment>);
	}
}

export default ChangePassword;
