import * as types from './types';
import axios from 'axios';

const initialState = {
	vehicle: {},
	vehicles: [],
	vehicleMakes: [],
	vehicleModels: [],
	isLoading: true,
};

export const vehicleActionCreators = {
	[types.GET_VEHICLE]: (id = '') => {
		return (dispatch) => {
			axios.get(`/vehicle/getvehicle?id=${id}`)
				.then(r => { return r.data })
				.then(response => {
					if (response.succeeded) {
						dispatch({
							type: types.UPDATE_VEHICLE,
							vehicle: response.data
						});
					}
				});
		}
	},
	[types.GET_VEHICLES]: (userId = '') => {
		return (dispatch) => {
			axios.get(`/vehicle/getvehicles?userId=${userId}`)
				.then(r => { return r.data })
				.then(response => {
					if (response.succeeded) {
						dispatch({
							type: types.UPDATE_VEHICLES,
							vehicles: response.data
						});
					}
				});
		}
	},
	[types.GET_VEHICLE_MAKES]: () => {
		return (dispatch) => {
			axios.get('/vehicle/getvehiclemakes')
				.then(r => { return r.data })
				.then(response => {
					if (response.succeeded) {
						dispatch({
							type: types.UPDATE_VEHICLE_MAKES,
							vehicleMakes: response.data
						});
					}
				});
		}
	},
	[types.GET_VEHICLE_MODELS]: (makeId, vehicleTypeId) => {
		return (dispatch) => {
			axios.get('/vehicle/getvehiclemodels', {
				params: {
					makeId: makeId,
					vehicleTypeId: vehicleTypeId,
				}
			}).then(r => { return r.data })
				.then(response => {
					if (response.succeeded) {
						dispatch({
							type: types.UPDATE_VEHICLE_MODELS,
							vehicleModels: response.data
						});
					}
				});
		}
	},
};


export const reducer = (state = initialState, action) => {
	switch (action.type) {
		case types.UPDATE_VEHICLE:
			return {
				...state,
				vehicle: action.vehicle,
				isLoading: false,
			};
		case types.UPDATE_VEHICLES:
			return {
				...state,
				vehicles: action.vehicles,
				isLoading: false,
			};
		case types.UPDATE_VEHICLE_MAKES:
			return {
				...state,
				vehicleMakes: action.vehicleMakes,
				isLoading: false,
			};
		case types.UPDATE_VEHICLE_MODELS:
			return {
				...state,
				vehicleModels: action.vehicleModels,
				isLoading: false,
			};

		default: return state;
	}
};


