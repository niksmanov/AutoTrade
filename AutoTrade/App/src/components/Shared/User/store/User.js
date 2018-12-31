import axios from 'axios';
import * as types from './types';

const initialState = {
	user: {},
	users: [],
	isLoading: true,
};

export const userActionCreators = {
	[types.GET_USER]: () => {
		return (dispatch) => {
			axios.get('/user/current')
				.then(r => { return r.data })
				.then(response => {
					if (response.succeeded) {
						dispatch({
							type: types.UPDATE_USER,
							user: response.data
						});
					}
				});
		}
	},
	[types.GET_ALL_USERS]: (search = '') => {
		return (dispatch) => {
			axios.get(`/admin/getusers?search=${search}`)
				.then(r => { return r.data })
				.then(response => {
					if (response.succeeded) {
						dispatch({
							type: types.UPDATE_ALL_USERS,
							users: response.data
						});
					}
				});
		}
	},
};

export const reducer = (state = initialState, action) => {
	switch (action.type) {
		case types.UPDATE_USER:
			return {
				...state,
				user: action.user,
				isLoading: false,
			};
		case types.UPDATE_ALL_USERS:
			return {
				...state,
				users: action.users,
				isLoading: false,
			};

		default: return state;
	}
};
