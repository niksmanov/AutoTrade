import React, { PureComponent } from 'react';
import axios from 'axios';
import DisplayErrors from '../Shared/Error/Error';

class Login extends PureComponent {
	state = {
		errors: []
	};

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
		return (<React.Fragment>
			<form onSubmit={this.handleSubmit.bind(this)}>
				<label>Email:</label>
				<input name="email" type="email" autoComplete="off" required className="form-control spacer" />
				<label>Password:</label>
				<input name="password" type="password" required className="form-control spacer" />
				<label className="spacer">Remember me: </label>
				<span> Yes </span>
				<input type="radio" name="rememberMe" value="true" defaultChecked />
				<span> No </span>
				<input type="radio" name="rememberMe" value="false" />
				<br />
				<button type="submit" className="btn btn-primary spacer">Submit</button>
			</form>
			<br />

			<DisplayErrors errors={this.state.errors} />
		</React.Fragment>);
	}
}

export default Login;
