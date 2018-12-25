import * as types from './types';
import axios from 'axios';

const initialState = {
	users: [],
	towns: [],
	colors: [],
	vehicleModels: [],
	vehicleMakes: [],
	isLoading: true,
};

export const actionCreators = {
	[types.GET_TOWNS]: () => {
		return (dispatch) => {
			axios.get('/admin/gettowns')
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
			axios.get('/admin/getcolors')
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

		default: return state;
	}
};


