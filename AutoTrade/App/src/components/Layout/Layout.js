import React from 'react';
import { Col, Grid, Row } from 'react-bootstrap';
import NavMenu from '../MainNavigation/NavMenu';

export default props => {



	return (<Grid fluid>
		<Row>
			<Col sm={1}>
				<NavMenu />
			</Col>
			<Col sm={11}>
				{props.children}
			</Col>
		</Row>
	</Grid>)
};
