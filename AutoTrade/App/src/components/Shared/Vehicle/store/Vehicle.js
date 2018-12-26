import * as types from './types';
import axios from 'axios';

const initialState = {
	towns: [],
	colors: [],
	vehicleMakes: [],
	vehicleModels: [],
	isLoading: true,
};

export const actionCreators = {
	[types.GET_TOWNS]: () => {
		return (dispatch) => {
			axios.get('/vehicle/gettowns')
				.then(r => { return r.data })
				.then(response => {
					if (response.succeeded) {
						dispatch({
							type: types.UPDATE_TOWNS,
							towns: response.data
						});
					}
				});
		}
	},
	[types.GET_COLORS]: () => {
		return (dispatch) => {
			axios.get('/vehicle/getcolors')
				.then(r => { return r.data })
				.then(response => {
					if (response.succeeded) {
						dispatch({
							type: types.UPDATE_COLORS,
							colors: response.data
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
	[types.GET_VEHICLE_MODELS]: (makeId) => {
		return (dispatch) => {
			axios.get(`/vehicle/getvehiclemodels?makeId=${makeId}`)
				.then(r => { return r.data })
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
		case types.UPDATE_TOWNS:
			return {
				...state,
				towns: action.towns,
				isLoading: false,
			};
		case types.UPDATE_COLORS:
			return {
				...state,
				colors: action.colors,
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


