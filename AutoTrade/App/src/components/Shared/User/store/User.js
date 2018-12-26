import axios from 'axios';
import * as types from './types';

const initialState = {
	user: {},
	users: [],
	isLoading: true,
};

export const actionCreators = {
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
};

export const reducer = (state = initialState, action) => {
	switch (action.type) {
		case types.UPDATE_USER:
			return {
				...state,
				user: action.user,
				isLoading: false,
			};

		default: return state;
	}
};
