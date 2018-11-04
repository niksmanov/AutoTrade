import React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout/Layout';
import Home from './components/Home/Home';
import Counter from './components/Counter/Counter';
import Register from './components/UserActions/Register';
import FetchData from './components/FetchData/FetchData';


export default () => (
	<Layout>
		<Route exact path='/' component={Home} />
		<Route path='/counter' component={Counter} />
		<Route path='/register' component={Register} />
		<Route path='/fetchdata/:startDateIndex?' component={FetchData} />
	</Layout>
);
