import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { actionCreators } from './store/Vehicle';
import * as types from './store/types';
import axios from 'axios';

const EMPTY_VEHICLE_ID = '00000000-0000-0000-0000-000000000000';

class Form extends Component {
	state = {
		selectFuel: 0,
		selectGearbox: 0,
		selectColor: 0,
		selectMake: 0,
		selectModel: 0,
		selectType: 0,
	};

	componentDidMount() {
		this.props[types.GET_VEHICLE_MAKES]();
		this.props[types.GET_COLORS]();
		this.props[types.GET_VEHICLE_ENUMS]();
	}

	handleSubmit(e) {
		e.preventDefault();
		let actionName = 'edit';
		if (this.props.vehicle.id === EMPTY_VEHICLE_ID) {
			actionName = 'add'
		}

		axios.post(`/profile/${actionName}vehicle`, new FormData(e.target))
			.then(r => { return r.data })
			.then(response => {
				if (response.succeeded) {
					this.setState({ errors: [`Entity ${actionName}ed successfully`] });
				} else {
					this.setState({ errors: [`We have a problem with ${actionName}ing`] });
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

		if (this.props.vehicleEnums.vehicleType) {
			vehicleForm =
				<form onSubmit={this.handleSubmit.bind(this)}>

					<label>Fuel Type:</label>
					<br />
					<select value={this.state.selectFuel || this.props.vehicle.fuelTypeId} onChange={this.selectEvent.bind(this, 'selectFuel')} name="fuelTypeId" required className="form-control spacer">
						{this.props.vehicleEnums.fuelType.map((type, i) => {
							return (<option key={i} value={type.id}>{type.name}</option>)
						})}
					</select>

					<label>Gearbox Type:</label>
					<br />
					<select value={this.state.selectGearbox || this.props.vehicle.gearboxTypeId} onChange={this.selectEvent.bind(this, 'selectGearbox')} name="gearboxTypeId" required className="form-control spacer">
						{this.props.vehicleEnums.gearboxType.map((type, i) => {
							return (<option key={i} value={type.id}>{type.name}</option>)
						})}
					</select>

					<label>Makes:</label>
					<select value={this.state.selectMake || this.props.vehicle.makeId} onChange={this.selectMake.bind(this)} name="makeId" required className="form-control spacer">
						<option>Select Make</option>
						{this.props.vehicleMakes.map((make, i) => {
							return (<option key={i} value={make.id}>{make.name}</option>)
						})}
					</select>

					<label>Vehicle Type:</label>
					<br />
					<select value={this.state.selectType || this.props.vehicle.typeId} onChange={this.selectType.bind(this)} name="typeId" required className="form-control spacer">
						<option>Select Type</option>
						{this.props.vehicleEnums.vehicleType.map((type, i) => {
							return (<option key={i} value={type.id}>{type.name}</option>)
						})}
					</select>

					<label>Models:</label>
					<select value={this.state.selectModel || this.props.vehicle.modelId} onChange={this.selectEvent.bind(this, 'selectModel')} name="modelId" required className="form-control spacer">
						<option>Select Model</option>
						{this.props.vehicleModels.map((model, i) => {
							return (<option key={i} value={model.id}>{model.name}</option>)
						})}
					</select>

					<label>Colors:</label>
					<select value={this.state.selectColor || this.props.vehicle.colorId} onChange={this.selectEvent.bind(this, 'selectColor')} name="colorId" required className="form-control spacer">
						<option>Select Color</option>
						{this.props.colors.map((color, i) => {
							return (<option key={i} value={color.id}>{color.name}</option>)
						})}
					</select>


					<input name="userId" type="hidden" value={this.props.userId} />
					<br />
					<button type="submit" className="btn btn-primary">Submit </button>
				</form>
		}

		return (<React.Fragment>
			{vehicleForm}
		</React.Fragment>);
	}
}

export default connect(
	state => state.vehicle,
	dispatch => bindActionCreators(actionCreators, dispatch)
)(Form);
