import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { vehicleActionCreators } from '../Shared/Vehicle/store/Vehicle';
import * as types from '../Shared/Vehicle/store/types';
import DisplayErrors from '../Shared/Error/Error';
import VehicleList from '../Shared/Vehicle/List';
import SearchForm from './Form';
import axios from 'axios';
import InfiniteScroll from 'react-infinite-scroller';


class Search extends Component {
	state = {
		page: 0,
		size: 10,
		hasMore: true,
		errors: [],
		showVehicles: false,
	};

	componentDidMount() {
		this.props[types.CLEAR_STATE]();
		this.props[types.GET_VEHICLES](this.state.page, this.state.size);
	}


	loadMore() {
		if (this.props.vehicles.length === (this.state.page + 1) * this.state.size) {
			this.setState({ page: this.state.page + 1 }, () => {
				this.props[types.GET_VEHICLES](this.state.page, this.state.size);
			});
		}
	}

	submitSearch(e) {
		e.preventDefault();
		axios.post('/vehicles/searchvehicles', new FormData(e.target))
			.then(r => { return r.data })
			.then(response => {
				if (response.succeeded) {
				} else {
					this.setState({ errors: response.errors });
				}
			});
	}

	render() {
		return (<React.Fragment>
			<button onClick={() => this.setState({ showVehicles: !this.state.showVehicles })}
				className="btn btn-default spacer">{this.state.showVehicles ? 'Show form' : 'Show all vehicles'}</button>
			<br />
			<br />
			{!this.state.showVehicles &&
				<SearchForm submitSearch={this.submitSearch.bind(this)} />}
			<br />
			{this.state.showVehicles &&
				<InfiniteScroll
					pageStart={0}
					loadMore={this.loadMore.bind(this)}
					hasMore={this.state.hasMore}
					loader={<div key={0} className="loading-app"></div>}>
					<VehicleList vehicles={this.props.vehicles} />
				</InfiniteScroll>}
			<br />
			<DisplayErrors errors={this.state.errors} />
		</React.Fragment >);
	}
}

export default connect(
	state => state.vehicle,
	dispatch => bindActionCreators(vehicleActionCreators, dispatch)
)(Search);