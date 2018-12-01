import React from 'react';

const Error = (props) => {
	if (!props.errors || props.errors.length === 0)
		return null;

	return (props.errors.map((err, i) => {
		return (<div key={i} className="alert alert-danger">
			<p>{err}</p>
		</div>);
	}));
}

export default Error;