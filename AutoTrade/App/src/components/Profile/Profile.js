import React from 'react';
import Navigation from './Navigation';
import { UserContext } from '../../App';

export default props => {
	return (<UserContext.Consumer>
		{user => <React.Fragment>
			<Navigation />

			<p> Hello, {user.userName} </p>
		</React.Fragment>}
	</UserContext.Consumer>);
}