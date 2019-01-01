import * as types from './types';
import axios from 'axios';

const initialState = {
	allCommons: {},
	towns: [],
	colors: [],
	vehicleTypes: [],
	gearboxTypes: [],
	fuelTypes: [],
	images: [],
	isLoading: true,
};

export const commonActionCreators = {
	[types.GET_TOWNS]: () => {
		return (dispatch) => {
			axios.get('/common/gettowns')
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
			axios.get('/common/getcolors')
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
	[types.GET_VEHICLE_TYPES]: () => {
		return (dispatch) => {
			axios.get('/common/getvehicletypes')
				.then(r => { return r.data })
				.then(response => {
					if (response.succeeded) {
						dispatch({
							type: types.UPDATE_VEHICLE_TYPES,
							vehicleTypes: response.data
						});
					}
				});
		}
	},
	[types.GET_FUEL_TYPES]: () => {
		return (dispatch) => {
			axios.get('/common/getfueltypes')
				.then(r => { return r.data })
				.then(response => {
					if (response.succeeded) {
						dispatch({
							type: types.UPDATE_FUEL_TYPES,
							fuelTypes: response.data
						});
					}
				});
		}
	},
	[types.GET_GEARBOX_TYPES]: () => {
		return (dispatch) => {
			axios.get('/common/getgearboxtypes')
				.then(r => { return r.data })
				.then(response => {
					if (response.succeeded) {
						dispatch({
							type: types.UPDATE_GEARBOX_TYPES,
							gearboxTypes: response.data
						});
					}
				});
		}
	},
	[types.GET_ALL_COMMONS]: () => {
		return (dispatch) => {
			axios.get('/common/getallcommons')
				.then(r => { return r.data })
				.then(response => {
					if (response.succeeded) {
						dispatch({
							type: types.UPDATE_ALL_COMMONS,
							allCommons: response.data
						});
					}
				});
		}
	},
	[types.GET_IMAGES]: (vehicleId = '') => {
		return (dispatch) => {
			axios.get(`/common/getimages?vehicleId=${vehicleId}`)
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
		case types.UPDATE_VEHICLE_TYPES:
			return {
				...state,
				vehicleTypes: action.vehicleTypes,
				isLoading: false,
			};
		case types.UPDATE_FUEL_TYPES:
			return {
				...state,
				fuelTypes: action.fuelTypes,
				isLoading: false,
			};
		case types.UPDATE_GEARBOX_TYPES:
			return {
				...state,
				gearboxTypes: action.gearboxTypes,
				isLoading: false,
			};
		case types.UPDATE_ALL_COMMONS:
			return {
				...state,
				allCommons: action.allCommons,
				isLoading: false,
			};
		case types.UPDATE_IMAGES:
			return {
				...state,
				images: action.images,
				isLoading: false,
			};

		default: return state;
	}
};