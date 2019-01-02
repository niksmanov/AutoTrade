import React from 'react';

export default props => {
	if (!props.errors || props.errors.length === 0)
		return null;

	return (<div className="row">
		{props.errors.map((err, i) => {
			return (<div key={i} className="col-sm-6 col-xs-12 alert alert-info">
				<p>{err}</p>
			</div>);
		})}
	</div>);
};