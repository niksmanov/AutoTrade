import React, { PureComponent } from 'react';
import axios from 'axios';
import Navigation from '../Administration/Navigation';
import DisplayErrors from '../Shared/Error/Error';

class VehicleModels extends PureComponent {
	state = {
		errors: []
	};

	render() {
		return (<React.Fragment>
			<Navigation />
			<DisplayErrors errors={this.state.errors} />
		</React.Fragment>);
	}
}

export default VehicleModels;
