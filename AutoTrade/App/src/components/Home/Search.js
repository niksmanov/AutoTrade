import React, { PureComponent } from 'react';
import axios from 'axios';
import DisplayErrors from '../Shared/Error/Error';

class Search extends PureComponent {
	state = {
		errors: []
	};

	render() {
		return (<React.Fragment>
			<DisplayErrors errors={this.state.errors} />
		</React.Fragment>);
	}
}

export default Search;
