import React from 'react';

export default props => {
	if (!props.errors || props.errors.length === 0)
		return null;

	return (props.errors.map((err, i) => {
		return (<div key={i} className="alert alert-info">
			<p>{err}</p>
		</div>);
	}));
}