

var HelloWorld = React.createClass({
    
    propTypes: {
        initialComments: React.PropTypes.array.isRequired,
      
        page: React.PropTypes.number
    },

    getInitialState() {
        return {
            comments: this.props.initialComments,
            page: this.props.page,
            hasMore: true,
            loadingMore: false
        };
    },

    render() {
        return (
            <div>Hello World</div>
        );
    }

});