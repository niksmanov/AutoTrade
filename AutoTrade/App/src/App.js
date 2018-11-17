import React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout/Layout';
import Home from './components/Home/Home';
import Register from './components/Account/Register';
import Login from './components/Account/Login';
import FetchData from './components/FetchData/FetchData';
import Profile from './components/Profile/Profile';


export default () => (
	<Layout>
		<Route exact path='/' component={Home} />
		<Route path='/register' component={Register} />
		<Route path='/login' component={Login} />
		<Route path='/profile' component={Profile} />
		<Route path='/fetchdata/:startDateIndex?' component={FetchData} />
	</Layout>
);
