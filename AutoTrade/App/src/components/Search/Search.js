import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { vehicleActionCreators } from '../Shared/Vehicle/store/Vehicle';
import * as types from '../Shared/Vehicle/store/types';
import DisplayErrors from '../Shared/Error/Error';
import VehicleList from '../Shared/Vehicle/List';
import SearchForm from './Form';
import InfiniteScroll from 'react-infinite-scroller';


class Search extends Component {
	state = {
		page: 0,
		size: 10,
		responseCount: 1,
		errors: [],
		formData: {},
		showVehicles: false,
		useSearch: false,
	};

	loadMore(useSearch) {
		if (this.props.vehicles.length === (this.state.page + 1) * this.state.size) {
			this.setState({ page: this.state.page + 1 }, () => {
				if (useSearch) {
					let tempForm = this.state.formData;
					tempForm.set('page', this.state.page);
					tempForm.set('size', this.state.size);
					this.props[types.GET_SEARCHED_VEHICLES](tempForm);
				} else {
					this.props[types.GET_VEHICLES](this.state.page, this.state.size);
				}
				this.setState({ responseCount: this.state.responseCount + 1 });
			});
		}
	}

	submitSearch(e) {
		e.preventDefault();
		let formData = new FormData(e.target);
		this.setState({ formData: formData });

		this.setState({ page: 0 }, () => {
			formData.append('page', this.state.page);
			formData.append('size', this.state.size);

			this.props[types.CLEAR_STATE]();
			this.props[types.GET_SEARCHED_VEHICLES](formData);
			this.setState({ responseCount: 1 });
			this.setState({ useSearch: true });
			this.setState({ showVehicles: true });
		});
	}

	toggleForm() {
		this.setState({ showVehicles: !this.state.showVehicles }, () => {
			if (this.state.showVehicles) {
				this.props[types.CLEAR_STATE]();
				this.setState({ page: 0 }, () => {
					this.props[types.GET_VEHICLES](this.state.page, this.state.size);
				});
			} else {
				this.setState({ useSearch: false });
			}
		});
	}

	render() {
		return (<React.Fragment>
			<button onClick={this.toggleForm.bind(this)}
				className="btn btn-default spacer">{this.state.showVehicles ? 'Show form' : 'Show all vehicles'}</button>
			<br />
			<br />
			{!this.state.showVehicles &&
				<SearchForm submitSearch={this.submitSearch.bind(this)} />}
			<br />
			{this.state.showVehicles &&
				<InfiniteScroll
					pageStart={0}
					loadMore={this.loadMore.bind(this, this.state.useSearch)}
					hasMore={this.props.vehicles.length === this.state.responseCount * this.state.size}
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