import React from 'react';
import { Link } from 'react-router-dom';

const NotFound = () => {
	return (<div align="center">
		<h1> Page not found! </h1>
		<br />
		<img src="/images/404.png" alt='not-found' className="spacer" style={{ maxWidth: '100%' }} />
		<br />
		<Link to={'/'} className="btn btn-primary spacer" > Go to Home page </Link>
	</div>);
};

export default NotFound;