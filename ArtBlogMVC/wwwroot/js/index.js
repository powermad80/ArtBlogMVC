class TestIt extends React.Component {
    render() {
        return (
            <h1>It's Alive!</h1>
        );
    }
}

class Post extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            title: props.title,
            imgUrl: props.imgUrl,
            description: props.description,
            tags: props.tags
        };
    }
    render() {
        return (
            <div className="post">
                <div className="post-header">
                    <h6>{this.state.title}</h6>
                </div>
                
                <div className="post-img">
                    <img src={this.state.imgUrl} height="1000" width="600" />
                </div>

                <div className="post-footer">
                    <p>{this.props.description}</p>
                </div>
            </div>
            )
    }
}


ReactDOM.render(
    <TestIt />,
    document.getElementById('content')
);