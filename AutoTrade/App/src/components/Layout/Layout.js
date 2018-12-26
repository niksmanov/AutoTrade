import React from 'react';
import { Col, Grid, Row } from 'react-bootstrap';
import MainNavigation from '../MainNavigation/MainNavigation';

export default props => {
	return (<Grid fluid style={{ maxWidth: '1980px' }}>
		<Row>
			<Col sm={1}>
				<MainNavigation />
			</Col>
			<Col sm={11} className="spacer">
				{props.children}
			</Col>
		</Row>
	</Grid>)
};
