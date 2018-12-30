import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { actionCreators } from '../Shared/Vehicle/store/Vehicle';
import * as types from '../Shared/Vehicle/store/types';
import axios from 'axios';
import { UserContext } from '../Shared/User/UserContext';
import DisplayErrors from '../Shared/Error/Error';
import Navigation from './Navigation';

class Profile extends Component {
	state = {
		errors: [],
		sendEmail: false,
		selectValue: null,
	};

	componentDidMount() {
		this.props[types.GET_TOWNS]();
	}

	selectTown(e) {
		this.setState({ selectValue: e.target.value });
	}

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

	editInfo(e) {
		e.preventDefault();
		axios.post('/profile/editinfo', new FormData(e.target))
			.then(r => { return r.data })
			.then(response => {
				if (response.succeeded) {
					this.setState({ errors: ['Entity edited successfully'] });
				} else {
					this.setState({ errors: ['Entity already exists'] });
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

					<form onSubmit={this.editInfo.bind(this)}>
						<label>Username:</label>
						<span className="form-control spacer"> {user.userName} </span>
						<label>Town:</label>
						<select value={this.state.selectValue || user.townId} onChange={this.selectTown.bind(this)} name="townId" className="form-control spacer">
							<option>Select Town</option>
							{this.props.towns.map((town, i) => {
								return (<option key={i} value={town.id}>{town.name}</option>)
							})}
						</select>
						<label>Address:</label>
						<input name="address" type="text" defaultValue={user.address} required className="form-control spacer" autoComplete="off" />
						<label>Phone number:</label>
						<input name="phoneNumber" type="tel" defaultValue={user.phoneNumber} required className="form-control spacer" autoComplete="off" />
						<input name="id" type="hidden" value={user.id} />
						<br />
						<button type="submit" className="btn btn-primary">Submit </button>
					</form>
					<br />

					{emailConfirmed}
					<DisplayErrors errors={this.state.errors} />
				</React.Fragment>}
		</UserContext.Consumer>);
	}
}

Profile.contextType = UserContext;
export default connect(
	state => state.vehicle,
	dispatch => bindActionCreators(actionCreators, dispatch)
)(Profile);