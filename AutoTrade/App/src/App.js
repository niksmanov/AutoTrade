﻿import React, { Component } from 'react';
import { Route, Redirect, Switch } from 'react-router';
import axios from 'axios';

//Public routes
import Layout from './components/Layout/Layout';
import NotFound from './components/NotFound/NotFound';
import Home from './components/Home/Home';
import Register from './components/Account/Register';
import Login from './components/Account/Login';
import ForgotPassword from './components/Account/ForgotPassword';

//Private routes
import Profile from './components/Profile/Profile';
import ChangePassword from './components/Account/ChangePassword';


const PrivateRoute = ({ component: Component, isAuth, ...rest }) => {
	return <Route {...rest} render={(props) =>
		isAuth ?
			<Component {...props} /> :
			<Redirect to='/login' />
	} />
};


class App extends Component {
	constructor() {
		super();
		this.state = {
			isUserAuth: false,
		};
	}

	componentWillMount() {
		axios.get('/user/current')
			.then(r => { return r.data })
			.then(response => {
				this.setState({ isUserAuth: response.succeeded });
			});
	}

	render() {
		let isAuth = this.state.isUserAuth;
		return (
			<Layout>
				<Switch>
					<Route exact path='/' component={Home} />
					<Route path='/register' component={Register} />
					<Route path='/login' component={Login} />
					<Route path='/forgotpassword' component={ForgotPassword} />

					<PrivateRoute isAuth={isAuth} path="/profile" component={Profile} />
					<PrivateRoute isAuth={isAuth} path="/changepassword" component={ChangePassword} />


					<Route component={NotFound} />
				</Switch>
			</Layout >
		);
	}
}

export default App;