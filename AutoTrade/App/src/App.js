import React, { Component } from 'react';
import { Route, Redirect, Switch } from 'react-router';
import { UserContext } from './components/Shared/User/UserContext';
import axios from 'axios';

//Public routes
import Layout from './components/Layout/Layout';
import NotFound from './components/NotFound/NotFound';
import Home from './components/Home/Home';
import Register from './components/Account/Register';
import Login from './components/Account/Login';
import ForgotPassword from './components/Account/ForgotPassword';
import SearchVehicle from './components/Shared/Vehicle/Search';

//Private routes
import Profile from './components/Profile/Profile';
import ChangePassword from './components/Account/ChangePassword';
import AddVehicle from './components/Shared/Vehicle/Add';
import ListVehicles from './components/Shared/Vehicle/List';

//Admin routes
import Users from './components/Administration/Users';
import VehicleMakes from './components/Administration/VehicleMakes';
import VehicleModels from './components/Administration/VehicleModels';
import Towns from './components/Administration/Towns';
import Colors from './components/Administration/Colors';


const PrivateRoute = ({ component: Component, isAuth, ...rest }) => {
	return <Route {...rest} render={(props) =>
		isAuth ?
			<Component {...props} /> :
			<Redirect to='/login' />
	} />
};

class App extends Component {
	state = {
		user: null,
		isLoading: true,
	};

	componentDidMount() {
		axios.get('/user/current')
			.then(r => { return r.data })
			.then(response => {
				this.setState({ user: response.data });
				this.setState({ isLoading: false });
			});
	}

	render() {
		let isAuth = this.state.user !== null;
		let isAdmin = isAuth && this.state.user.isAdmin;
		let privateRoutes;
		if (this.state.isLoading) {
			privateRoutes =
				<React.Fragment>
					<div className="loading-app"></div>
				</React.Fragment>;
		} else {
			privateRoutes =
				<React.Fragment>
					<Switch>
						<PrivateRoute isAuth={isAdmin} path="/admin/users" component={Users} />
						<PrivateRoute isAuth={isAdmin} path="/admin/makes" component={VehicleMakes} />
						<PrivateRoute isAuth={isAdmin} path="/admin/models" component={VehicleModels} />
						<PrivateRoute isAuth={isAdmin} path="/admin/towns" component={Towns} />
						<PrivateRoute isAuth={isAdmin} path="/admin/colors" component={Colors} />

						<PrivateRoute isAuth={isAuth} path="/profile/home" component={Profile} />
						<PrivateRoute isAuth={isAuth} path="/profile/changepassword" component={ChangePassword} />
						<PrivateRoute isAuth={isAuth} path="/profile/addvehicle" component={AddVehicle} />
						<PrivateRoute isAuth={isAuth} path="/profile/listvehicles" component={ListVehicles} />

						<Route component={NotFound} />
					</Switch>
				</React.Fragment>;
		}

		return (<UserContext.Provider value={this.state.user}>
			<Layout>
				<Switch>
					<Route exact path='/' component={Home} />
					<Route path='/search' component={SearchVehicle} />
					<Route path='/register' component={Register} />
					<Route path='/login' component={Login} />
					<Route path='/forgotpassword' component={ForgotPassword} />
					{privateRoutes}
				</Switch>
			</Layout >
		</UserContext.Provider>);
	}
}

export default App;