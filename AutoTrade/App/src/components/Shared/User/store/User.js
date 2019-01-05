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
	[types.GET_USERS]: (page, size, search = '') => {
		return (dispatch) => {
			axios.get('/admin/getusers', {
				params: {
					page: page,
					size: size,
					search: search,
				}
			}).then(r => { return r.data })
				.then(response => {
					if (response.succeeded) {
						dispatch({
							type: types.UPDATE_USERS,
							users: response.data,
						});
					}
				});
		}
	},
	[types.CLEAR_STATE]: () => {
		return (dispatch) => {
			dispatch({ type: types.UPDATE_CLEAR_STATE });
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
		case types.UPDATE_USERS:
			return {
				...state,
				users: state.users.concat(action.users),
				isLoading: false,
			};
		case types.UPDATE_CLEAR_STATE:
			return initialState;

		default: return state;
	}
};
