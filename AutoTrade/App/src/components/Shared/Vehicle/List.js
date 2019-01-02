import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { Col, Row } from 'react-bootstrap';

class List extends Component {
	render() {
		return (<React.Fragment>
			<Row className="no-margin">
				{this.props.vehicles.map((vehicle, i) => {
					return (<Col sm={6} key={i} className="vehicle-list-entity">
						<Link to={vehicle.url}>
							{vehicle.coverImageUrl && <Col sm={4}>
								<img alt='cover' src={vehicle.coverImageUrl} />
							</Col>}
							<Col sm={vehicle.coverImageUrl ? 8 : 12}>
								<span><span><b>Make:</b></span> {vehicle.make}</span>
								<span><span><b>Model:</b></span> {vehicle.model}</span>
								<span><span><b>Gearbox:</b></span> {vehicle.gearboxType}</span>
								<span><span><b>Fuel:</b></span> {vehicle.fuelType}</span>
								<span><span><b>Color:</b></span> {vehicle.color}</span>
								<span><span><b>HP:</b></span> {vehicle.horsePower}</span>
								<span><span><b>Cm3:</b></span> {vehicle.cubicCapacity}</span>
								<span><span><b>Prod. Date:</b> </span> {new Date(vehicle.displayDate).toLocaleDateString()}</span>
								<span><span><b>Price:</b></span> {vehicle.price} BGN</span>
							</Col>
						</Link>
					</Col>)
				})}
			</Row>
		</React.Fragment>);
	}
}

export default List;
