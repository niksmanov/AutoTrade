import React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout/Layout';
import Home from './components/Home/Home';
import Counter from './components/Counter/Counter';
import Register from './components/User/Register';
import Login from './components/User/Login';
import FetchData from './components/FetchData/FetchData';
import Profile from './components/UserProfile/Profile';


export default () => (
	<Layout>
		<Route exact path='/' component={Home} />
		<Route path='/counter' component={Counter} />
		<Route path='/register' component={Register} />
		<Route path='/login' component={Login} />
		<Route path='/profile' component={Profile} />
		<Route path='/fetchdata/:startDateIndex?' component={FetchData} />
	</Layout>
);
