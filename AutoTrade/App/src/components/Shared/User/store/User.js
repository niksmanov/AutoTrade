import axios from 'axios';

const GET_USER = "USER_GET_USER";

const initialState = {
	user: {},
};

export const reducer = (state = initialState, action) => {
	if (action.type === GET_USER) {
		return state = {
			...state,
			user: action.user
		};
	}
	return state;
};


export const actionCreators = {
	getUser: () => {
		return (dispatch) => {
			axios.get('/user/current')
				.then(r => { return r.data })
				.then(response => {
					if (response.succeeded) {
						dispatch({
							type: GET_USER,
							user: response.data
						});
					}
				});
		}
	},
};
