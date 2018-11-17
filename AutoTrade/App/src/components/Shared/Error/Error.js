import React, { Component } from 'react';

class Error extends Component {
	render() {
		let errorMessages = [];
		if (this.props.errors && this.props.errors.length > 0) {
			errorMessages = this.props.errors.map((err, i) => {
				return (<div key={i} className="alert alert-danger">
					<p>{err}</p>
				</div>);
			});
		}
		return (<React.Fragment>{errorMessages}</React.Fragment>);
	}
}

export default Error;