import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { vehicleActionCreators } from '../Shared/Vehicle/store/Vehicle';
import * as types from '../Shared/Vehicle/store/types';
import { UserContext } from '../Shared/User/UserContext';
import Navigation from './Navigation';
import VehicleList from '../Shared/Vehicle/List';
import InfiniteScroll from 'react-infinite-scroller';


class Vehicles extends Component {
	state = {
		page: 0,
		size: 10,
		responseCount: 1,
	};

	componentDidMount() {
		this.props[types.CLEAR_STATE]();
		this.props[types.GET_VEHICLES](this.state.page, this.state.size, this.context.id);
	}

	loadMore() {
		if (this.props.vehicles.length === (this.state.page + 1) * this.state.size) {
			this.setState({ page: this.state.page + 1 }, () => {
				this.props[types.GET_VEHICLES](this.state.page, this.state.size, this.context.id);
				this.setState({ responseCount: this.state.responseCount + 1 });
			});
		}
	}

	render() {
		let vehicleList;
		if (this.props.isLoading) {
			vehicleList =
				<React.Fragment>
					<div className="loading-app"></div>
				</React.Fragment>;
		} else {
			vehicleList =
				<VehicleList vehicles={this.props.vehicles} />
		}

		return (<React.Fragment>
			<Navigation />
			<InfiniteScroll
				pageStart={0}
				loadMore={this.loadMore.bind(this)}
				hasMore={this.props.vehicles.length === this.state.responseCount * this.state.size}
				loader={<div key={0} className="loading-app"></div>}>
				{vehicleList}
			</InfiniteScroll>
		</React.Fragment>);
	}
}

Vehicles.contextType = UserContext;
export default connect(
	state => state.vehicle,
	dispatch => bindActionCreators(vehicleActionCreators, dispatch)
)(Vehicles);

