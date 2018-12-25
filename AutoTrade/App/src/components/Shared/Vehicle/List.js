import React, { PureComponent } from 'react';
import axios from 'axios';
import Navigation from '../../Profile/Navigation';
import DisplayErrors from '../Error/Error';

class List extends PureComponent {
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

export default List;
