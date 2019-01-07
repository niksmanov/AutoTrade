import React, { Component } from 'react';
import { Col, Row } from 'react-bootstrap';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { UserContext } from '../Shared/User/UserContext';
import { vehicleActionCreators } from '../Shared/Vehicle/store/Vehicle';
import { commonActionCreators } from '../Shared/Common/store/Common';
import * as types from '../Shared/Vehicle/store/types';
import * as commonTypes from '../Shared/Common/store/types';
import DisplayErrors from '../Shared/Error/Error';


class Form extends Component {
	state = {
		errors: [],
		selectTown: 0,
		selectMake: 0,
		selectModel: 0,
		selectColor: 0,
		selectType: 0,
		selectFuel: 0,
		selectGearbox: 0,
		selectAirbags: false,
		selectAbs: false,
		selectEsp: false,
		selectCentralLocking: false,
		selectAirConditioning: false,
		selectAutoPilot: false,
	};

	componentDidMount() {
		this.props[types.GET_VEHICLE_MAKES]();
		this.props[commonTypes.GET_ALL_COMMONS]();
	}

	selectMake(e) {
		let id = e.target.value;
		if (id > 0) {
			this.props[types.GET_VEHICLE_MODELS](id, this.state.selectType);
			this.setState({ selectMake: id });
		}
	}

	selectType(e) {
		let type = e.target.value;
		this.props[types.GET_VEHICLE_MODELS](this.state.selectMake, type);
		this.setState({ selectType: type });
	}

	selectEvent(stateProp, e) {
		this.setState({ [stateProp]: e.target.value });
	}

	render() {
		let vehicleForm =
			<React.Fragment>
				<div className="loading-app"></div>
			</React.Fragment>;

		if (this.props.allCommons.vehicleTypes) {
			vehicleForm =
				<React.Fragment>
					<form onSubmit={this.props.submitSearch.bind(this)}>
						<Row>
							<Col sm={6}>
								<div>
									<label>Towns:</label>
									<select value={this.state.selectTown} onChange={this.selectEvent.bind(this, 'selectTown')} name="townId" className="form-control spacer">
										<option>Select Town</option>
										{this.props.allCommons.towns.map((town, i) => {
											return (<option key={i} value={town.id}>{town.name}</option>)
										})}
									</select>
								</div>

								<div>
									<label>Vehicle Type:</label>
									<select value={this.state.selectType} onChange={this.selectType.bind(this)} name="typeId" className="form-control spacer">
										<option>Select Vehicle Type</option>
										{this.props.allCommons.vehicleTypes.map((type, i) => {
											return (<option key={i} value={type.id}>{type.name}</option>)
										})}
									</select>
								</div>

								<div>
									<label>Fuel Type:</label>
									<select value={this.state.selectFuel} onChange={this.selectEvent.bind(this, 'selectFuel')} name="fuelTypeId" className="form-control spacer">
										<option>Select Fuel Type</option>
										{this.props.allCommons.fuelTypes.map((type, i) => {
											return (<option key={i} value={type.id}>{type.name}</option>)
										})}
									</select>
								</div>

								<div>
									<label>Gearbox Type:</label>
									<select value={this.state.selectGearbox} onChange={this.selectEvent.bind(this, 'selectGearbox')} name="gearboxTypeId" className="form-control spacer">
										<option>Select Gearbox Type</option>
										{this.props.allCommons.gearboxTypes.map((type, i) => {
											return (<option key={i} value={type.id}>{type.name}</option>)
										})}
									</select>
								</div>

								<label>Makes:</label>
								<select value={this.state.selectMake} onChange={this.selectMake.bind(this)} name="makeId" className="form-control spacer">
									<option>Select Make</option>
									{this.props.vehicleMakes.map((make, i) => {
										return (<option key={i} value={make.id}>{make.name}</option>)
									})}
								</select>

								<div>
									<label>Models:</label>
									<select value={this.state.selectModel} onChange={this.selectEvent.bind(this, 'selectModel')} name="modelId" className="form-control spacer">
										<option>Select Model</option>
										{this.props.vehicleModels.map((model, i) => {
											return (<option key={i} value={model.id}>{model.name}</option>)
										})}
									</select>
								</div>

								<div>
									<label>Colors:</label>
									<select value={this.state.selectColor} onChange={this.selectEvent.bind(this, 'selectColor')} name="colorId" className="form-control spacer">
										<option>Select Color</option>
										{this.props.allCommons.colors.map((color, i) => {
											return (<option key={i} value={color.id}>{color.name}</option>)
										})}
									</select>
								</div>

								<div className="spacer">
									<div className="sm-input-first">
										<label>From horse power:</label>
										<input type="number" name="fromHorsePower" min="1" className="form-control sm" />
									</div>
									<div>
										<label>To horse power:</label>
										<input type="number" name="toHorsePower" min="1" className="form-control sm" />
									</div>
								</div>

								<div className="spacer">
									<div className="sm-input-first">
										<label>From price (BGN):</label>
										<input type="number" name="fromPrice" min="1" className="form-control sm" />
									</div>
									<div>
										<label>To price (BGN):</label>
										<input type="number" name="toPrice" min="1" className="form-control sm" />
									</div>
								</div>
							</Col>

							<Col sm={6} className="spacer">
								<div className="sm-input-first">
									<label>From cubic capacity (cm3):</label>
									<input type="number" name="fromCubicCapacity" min="50" className="form-control sm" />
								</div>
								<div>
									<label>To cubic capacity (cm3):</label>
									<input type="number" name="toCubicCapacity" min="50" className="form-control sm" />
								</div>

								<div className="spacer">
									<label>Airbags:</label>
									<select value={this.state.selectAirbags} onChange={this.selectEvent.bind(this, 'selectAirbags')} name="airbags" className="form-control spacer">
										<option value="true">Yes</option>
										<option value="false">No</option>
									</select>
								</div>

								<div>
									<label>ABS:</label>
									<select value={this.state.selectAbs} onChange={this.selectEvent.bind(this, 'selectAbs')} name="abs" className="form-control spacer">
										<option value="true">Yes</option>
										<option value="false">No</option>
									</select>
								</div>

								<div>
									<label>ESP:</label>
									<select value={this.state.selectEsp} onChange={this.selectEvent.bind(this, 'selectEsp')} name="esp" className="form-control spacer">
										<option value="true">Yes</option>
										<option value="false">No</option>
									</select>
								</div>

								<div>
									<label>Central Locking:</label>
									<select value={this.state.selectCentralLocking} onChange={this.selectEvent.bind(this, 'selectCentralLocking')} name="centralLocking" className="form-control spacer">
										<option value="true">Yes</option>
										<option value="false">No</option>
									</select>
								</div>

								<div>
									<label>Air Conditioning:</label>
									<select value={this.state.selectAirConditioning} onChange={this.selectEvent.bind(this, 'selectAirConditioning')} name="airConditioning" className="form-control spacer">
										<option value="true">Yes</option>
										<option value="false">No</option>
									</select>
								</div>

								<div>
									<label>Auto Pilot:</label>
									<select value={this.state.selectAutoPilot} onChange={this.selectEvent.bind(this, 'selectAutoPilot')} name="autoPilot" className="form-control spacer">
										<option value="true">Yes</option>
										<option value="false">No</option>
									</select>
								</div>

								<div className="spacer">
									<div className="sm-input-first ">
										<label>From production date:</label>
										<input type="date" name="fromProductionDate" className="form-control sm" />
									</div>
									<div>
										<label>To production date:</label>
										<input type="date" name="toProductionDate" className="form-control sm" />
									</div>
								</div>
								<div align="right" style={{ maxWidth: '350px' }}>
									<br />
									<button type="submit" className="btn btn-primary spacer">Submit </button>
								</div>
							</Col>
						</Row>
					</form>
					<br />
					<DisplayErrors errors={this.state.errors} />
				</React.Fragment>;
		}

		return (<React.Fragment>
			{vehicleForm}
		</React.Fragment>);
	}
}

Form.contextType = UserContext;
export default connect(
	state => Object.assign({}, state.vehicle, state.common),
	dispatch => bindActionCreators(Object.assign({}, vehicleActionCreators, commonActionCreators), dispatch)
)(Form);