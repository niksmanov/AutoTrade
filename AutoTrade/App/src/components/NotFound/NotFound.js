import React from 'react';

const NotFound = () => {
	return (
		<div align="center">
			<h1> Page not found! </h1>
			<br />
			<img src="/images/404.png" alt='not-found' className="spacer" style={{ maxWidth: '100%' }} />
			<br />
			<a href="/" className="btn btn-primary spacer" > Go to Home page</a>
		</div>
	);
};

export default NotFound;