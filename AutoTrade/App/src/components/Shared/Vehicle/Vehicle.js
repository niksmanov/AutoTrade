import React, { Component } from 'react';
import { Carousel } from 'react-responsive-carousel';
import { Link } from 'react-router-dom';
import { connect } from 'react-redux';
import { Col, Row } from 'react-bootstrap';
import { bindActionCreators } from 'redux';
import DisplayErrors from '../Error/Error';
import { UserContext } from '../User/UserContext';
import { vehicleActionCreators } from './store/Vehicle';
import * as types from './store/types';
import 'react-responsive-carousel/lib/styles/carousel.min.css';
import '../../../styles/components/Vehicle/vehicle.css';

class Vehicle extends Component {
	state = {
		errors: []
	};

	componentDidMount() {
		this.props[types.GET_VEHICLE](this.props.match.params.id);
	}

	render() {
		let editButton;
		if (this.context !== null &&
			((this.context.id === this.props.vehicle.userId) || this.context.isAdmin)) {
			editButton =
				<button className="btn btn-default vehicle-edit">
					<span className="glyphicon glyphicon-pencil"></span> Edit </button>
		}

		let vehicle =
			<React.Fragment>
				<div className="loading-app"></div>
			</React.Fragment>;

		if (this.props.vehicle.user) {
			let v = this.props.vehicle;
			vehicle =
				<React.Fragment>
					<Row>
						<Col sm={7} className="vehicle-specs spacer">
							<Link to={`/profile/editvehicle/${v.id}`}>
								<p className="vehicle-heading"> Vehicle</p>
								{editButton}
							</Link>
							<br />
							<Col sm={6}>
								<p><span><b>Type:</b></span> {v.type}</p>
								<p><span><b>Make:</b></span> {v.make}</p>
								<p><span><b>Model:</b></span> {v.model}</p>
								<p><span><b>Gearbox:</b></span> {v.gearboxType}</p>
								<p><span><b>Fuel:</b></span> {v.fuelType}</p>
								<p><span><b>Color:</b></span> {v.color}</p>
								<p><span><b>Horse Power:</b></span> {v.horsePower}</p>
								<p><span><b>Cubic Capacity:</b></span> {v.cubicCapacity}</p>
							</Col>
							<Col sm={6}>
								<p><span><b>Airbags:</b></span> {v.airbags ? "Yes" : "No"}</p>
								<p><span><b>ABS:</b></span> {v.abs ? "Yes" : "No"}</p>
								<p><span><b>ESP:</b></span> {v.esp ? "Yes" : "No"}</p>
								<p><span><b>Central Locking:</b></span> {v.centralLocking ? "Yes" : "No"}</p>
								<p><span><b>Air Conditioning:</b></span> {v.airConditioning ? "Yes" : "No"}</p>
								<p><span><b>Auto Pilot:</b></span> {v.autoPilot ? "Yes" : "No"}</p>
								<p><span><b>Production Date:</b> </span> {new Date(v.displayDate).toLocaleDateString()}</p>
								<p><span><b>Price:</b></span> {v.price} BGN</p>
							</Col>
						</Col>

						<Col sm={4} className="vehicle-specs seller spacer">
							<Link to={'/profile/home'}>
								<p className="vehicle-heading"> Seller </p>
								{editButton}
							</Link>
							<br />
							<p><span><b>Username:</b></span> {v.user.userName}</p>
							<p><span><b>Address:</b></span> {v.user.address}</p>
							<p><span><b>Town:</b></span> {v.user.townName}</p>
							<p><span><b>Email:</b></span> {v.user.email}</p>
							<p><span><b>Phone:</b></span> {v.user.phoneNumber}</p>
						</Col>
					</Row>

					{v.images.length > 0 &&
						<Row>
							<br />
							<Col sm={12} align="center">
								<Carousel width="80%" showArrows={true} dynamicHeight={true} infiniteLoop={true}>
									{v.images.map((img, i) => {
										return (<div key={i}>
											<img alt='vehicle' src={img.url} className="vehicle-image" />
										</div>);
									})}
								</Carousel>
							</Col>
						</Row>}
				</React.Fragment>
		}

		return (<React.Fragment>
			{vehicle}
			<DisplayErrors errors={this.state.errors} />
		</React.Fragment >);
	}
}

Vehicle.contextType = UserContext;
export default connect(
	state => state.vehicle,
	dispatch => bindActionCreators(vehicleActionCreators, dispatch)
)(Vehicle);

