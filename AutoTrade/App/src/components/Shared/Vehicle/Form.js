import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { UserContext } from '../User/UserContext';
import { vehicleActionCreators } from './store/Vehicle';
import { commonActionCreators } from '../Common/store/Common';
import * as types from './store/types';
import * as commonTypes from '../Common/store/types';
import DisplayErrors from '../Error/Error';
import axios from 'axios';

class Form extends Component {
	state = {
		errors: [],
		selectFuel: 0,
		selectGearbox: 0,
		selectColor: 0,
		selectMake: 0,
		selectModel: 0,
		selectType: 0,
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

	deleteVehicle(vehicleId, e) {
		e.preventDefault();
		var formdata = new FormData();
		formdata.append('id', vehicleId);

		axios.post('/profile/removevehicle', formdata)
			.then(r => { return r.data })
			.then(response => {
				if (response.succeeded) {
					this.setState({ errors: ['Entity deleted successfully'] });
					window.location.href = '/profile/vehicles';
				} else {
					this.setState({ errors: ['We have a problem with deleting'] });
				}
			});
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

			let deleteButton;
			if (this.props.vehicle.user === null) {
				deleteButton =
					<button type="submit" className="btn btn-primary">Submit </button>
			} else if (this.context.id === this.props.vehicle.userId) {
				deleteButton =
					<React.Fragment>
						<input type="hidden" name="userId" value={this.props.vehicle.userId} />
						<button type="submit" className="btn btn-primary" style={{ marginRight: '50px' }}>Submit </button>
						<button onClick={this.deleteVehicle.bind(this, this.props.vehicle.id)} className="btn btn-danger">Delete</button>
					</React.Fragment>
			} else {
				window.location.href = '/';
			}

			vehicleForm =
				<React.Fragment>
					<form onSubmit={this.props.handleSubmit.bind(this)} className="row">
						<div className="col-sm-6 col-xs-12">
							<div>
								<label>Vehicle Type:</label>
								<select value={this.state.selectType} onChange={this.selectType.bind(this)} name="typeId" required className="form-control spacer">
									<option>Select Vehicle Type</option>
									{this.props.allCommons.vehicleTypes.map((type, i) => {
										return (<option key={i} value={type.id}>{type.name}</option>)
									})}
								</select>
							</div>

							<div>
								<label>Fuel Type:</label>
								<select value={this.state.selectFuel || this.props.vehicle.fuelTypeId} onChange={this.selectEvent.bind(this, 'selectFuel')} name="fuelTypeId" required className="form-control spacer">
									<option>Select Fuel Type</option>
									{this.props.allCommons.fuelTypes.map((type, i) => {
										return (<option key={i} value={type.id}>{type.name}</option>)
									})}
								</select>
							</div>

							<div>
								<label>Gearbox Type:</label>
								<select value={this.state.selectGearbox || this.props.vehicle.gearboxTypeId} onChange={this.selectEvent.bind(this, 'selectGearbox')} name="gearboxTypeId" required className="form-control spacer">
									<option>Select Gearbox Type</option>
									{this.props.allCommons.gearboxTypes.map((type, i) => {
										return (<option key={i} value={type.id}>{type.name}</option>)
									})}
								</select>
							</div>

							<label>Makes:</label>
							<select value={this.state.selectMake} onChange={this.selectMake.bind(this)} name="makeId" required className="form-control spacer">
								<option>Select Make</option>
								{this.props.vehicleMakes.map((make, i) => {
									return (<option key={i} value={make.id}>{make.name}</option>)
								})}
							</select>

							<div>
								<label>Models:</label>
								<select value={this.state.selectModel || this.props.vehicle.modelId} onChange={this.selectEvent.bind(this, 'selectModel')} name="modelId" required className="form-control spacer">
									<option>Select Model</option>
									{this.props.vehicleModels.map((model, i) => {
										return (<option key={i} value={model.id}>{model.name}</option>)
									})}
								</select>
							</div>

							<div>
								<label>Colors:</label>
								<select value={this.state.selectColor || this.props.vehicle.colorId} onChange={this.selectEvent.bind(this, 'selectColor')} name="colorId" required className="form-control spacer">
									<option>Select Color</option>
									{this.props.allCommons.colors.map((color, i) => {
										return (<option key={i} value={color.id}>{color.name}</option>)
									})}
								</select>
							</div>

							<div className="spacer">
								<label>Horse power:</label>
								<input type="number" name="horsePower" defaultValue={this.props.vehicle.horsePower} required min="1" className="form-control" />
							</div>

							<div className="spacer">
								<label>Price (BGN):</label>
								<input type="number" name="price" defaultValue={this.props.vehicle.price} required min="1" className="form-control" />
							</div>
						</div>

						<div className="col-sm-6 col-xs-12">
							<div className="spacer">
								<label>Cubic capacity (cm3):</label>
								<input type="number" name="cubicCapacity" defaultValue={this.props.vehicle.cubicCapacity} required min="50" className="form-control" />
							</div>

							<div>
								<label>Airbags:</label>
								<select value={this.state.selectAirbags || this.props.vehicle.airbags} onChange={this.selectEvent.bind(this, 'selectAirbags')} name="airbags" required className="form-control spacer">
									<option value="true">Yes</option>
									<option value="false">No</option>
								</select>
							</div>

							<div>
								<label>ABS:</label>
								<select value={this.state.selectAbs || this.props.vehicle.abs} onChange={this.selectEvent.bind(this, 'selectAbs')} name="abs" required className="form-control spacer">
									<option value="true">Yes</option>
									<option value="false">No</option>
								</select>
							</div>

							<div>
								<label>ESP:</label>
								<select value={this.state.selectEsp || this.props.vehicle.esp} onChange={this.selectEvent.bind(this, 'selectEsp')} name="esp" required className="form-control spacer">
									<option value="true">Yes</option>
									<option value="false">No</option>
								</select>
							</div>

							<div>
								<label>Central Locking:</label>
								<select value={this.state.selectCentralLocking || this.props.vehicle.centralLocking} onChange={this.selectEvent.bind(this, 'selectCentralLocking')} name="centralLocking" required className="form-control spacer">
									<option value="true">Yes</option>
									<option value="false">No</option>
								</select>
							</div>

							<div>
								<label>Air Conditioning:</label>
								<select value={this.state.selectAirConditioning || this.props.vehicle.airConditioning} onChange={this.selectEvent.bind(this, 'selectAirConditioning')} name="airConditioning" required className="form-control spacer">
									<option value="true">Yes</option>
									<option value="false">No</option>
								</select>
							</div>

							<div>
								<label>Auto Pilot:</label>
								<select value={this.state.selectAutoPilot || this.props.vehicle.autoPilot} onChange={this.selectEvent.bind(this, 'selectAutoPilot')} name="autoPilot" required className="form-control spacer">
									<option value="true">Yes</option>
									<option value="false">No</option>
								</select>
							</div>

							<div className="spacer">
								<label>Production Date:</label>
								<input type="date" name="productionDate" defaultValue={this.props.vehicle.displayDate} required className="form-control" />
							</div>
							<br />

							{deleteButton}
						</div>
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