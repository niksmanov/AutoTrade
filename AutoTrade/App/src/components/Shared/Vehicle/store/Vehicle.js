import * as types from './types';
import axios from 'axios';

const initialState = {
	towns: [],
	colors: [],
	images: [],
	vehicles: [],
	vehicleMakes: [],
	vehicleModels: [],
	vehicleEnums: {},
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
	[types.GET_IMAGES]: (vehicleId = '') => {
		return (dispatch) => {
			axios.get(`/vehicle/getimages?vehicleId=${vehicleId}`)
				.then(r => { return r.data })
				.then(response => {
					if (response.succeeded) {
						dispatch({
							type: types.UPDATE_IMAGES,
							images: response.data
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
	[types.GET_VEHICLE_MODELS]: (makeId, vehicleType = '') => {
		return (dispatch) => {
			axios.get('/vehicle/getvehiclemodels', {
				params: {
					makeId: makeId,
					vehicleType: vehicleType,
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
	[types.GET_VEHICLE_ENUMS]: () => {
		return (dispatch) => {
			axios.get('/vehicle/getvehicleenums')
				.then(r => { return r.data })
				.then(response => {
					if (response.succeeded) {
						dispatch({
							type: types.UPDATE_VEHICLE_ENUMS,
							vehicleEnums: response.data
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
		case types.UPDATE_IMAGES:
			return {
				...state,
				images: action.images,
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
		case types.UPDATE_VEHICLE_ENUMS:
			return {
				...state,
				vehicleEnums: action.vehicleEnums,
				isLoading: false,
			};

		default: return state;
	}
};


