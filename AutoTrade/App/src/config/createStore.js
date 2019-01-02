import { applyMiddleware, combineReducers, compose, createStore } from 'redux';
import thunk from 'redux-thunk';
import { routerReducer, routerMiddleware } from 'react-router-redux';
import * as user from '../components/Shared/User/store/User';
import * as vehicle from '../components/Shared/Vehicle/store/Vehicle';
import * as common from '../components/Shared/Common/store/Common';


export default function configureStore(history, initialState) {
	const reducers = {
		user: user.reducer,
		vehicle: vehicle.reducer,
		common: common.reducer,
	};

	const middleware = [
		thunk,
		routerMiddleware(history)
	];

	const enhancers = [];
	const isDevelopment = process.env.NODE_ENV === 'development';
	if (isDevelopment && typeof window !== 'undefined' && window.devToolsExtension) {
		enhancers.push(window.devToolsExtension());
	}

	const rootReducer = combineReducers({
		...reducers,
		routing: routerReducer
	});

	return createStore(
		rootReducer,
		initialState,
		compose(applyMiddleware(...middleware), ...enhancers)
	);
}
