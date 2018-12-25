import axios from 'axios';
import * as types from './types';

const initialState = {
	user: {},
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
	if (action.type === types.UPDATE_USER) {
		return {
			...state,
			user: action.user
		};
	}
	return state;
};
