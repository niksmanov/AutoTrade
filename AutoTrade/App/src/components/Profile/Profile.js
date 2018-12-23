import React, { Component } from 'react';
import Navigation from './Navigation';
import { UserContext } from '../Shared/User/UserContext';
import axios from 'axios';
import DisplayErrors from '../Shared/Error/Error';

class Profile extends Component {
	state = {
		errors: [],
		sendEmail: false,
	};

	reSendEmail(userId) {
		axios.get(`/user/resendconfirmationemail?id=${userId}`
		).then(r => {
			return r.data
		}).then(response => {
			if (response.errors.length > 0) {
				this.setState({ errors: response.errors });
				this.setState({ sendEmail: true });
			}
		});
	}

	render() {
		let emailConfirmed;

		if (!this.context.emailConfirmed && !this.state.sendEmail) {
			emailConfirmed = <div className="alert alert-info">
				<p>Please check your email and confirm this account.</p>
				<p>If you haven't receive email from us
					<span onClick={this.reSendEmail.bind(this, this.context.id)}> <u>click here</u></span> to send it again.</p>
			</div>
		}

		return (<UserContext.Consumer>
			{user =>
				<React.Fragment>
					<Navigation />
					<p> Hello, {user.userName} </p>
					{emailConfirmed}


					<DisplayErrors errors={this.state.errors} />
				</React.Fragment>}
		</UserContext.Consumer>);
	}
}

Profile.contextType = UserContext;
export default Profile;