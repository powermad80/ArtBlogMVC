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
    }
    render() {
        return (
            <div className="post">
                <div className="post-header">
                    <h3>{this.props.title}</h3>
                </div>
                
                <div className="post-img">
                    <img src={this.props.imgUrl} height="600" width="400" />
                </div>

                <div className="post-footer">
                    <p>{this.props.description}</p>
                </div>
            </div>
            )
    }
}


var content = Array();
content[0] = {
    Id: 1,
    title: "test",
    imgUrl: "https://pbs.twimg.com/media/DTp8sPpX4AAmb4L.jpg",
    description: "test description"
};

content[1] = {
    Id: 2,
    title: "second test",
    imgUrl: "https://puu.sh/yXnYu.png",
    description: "the second test description"
};

const listContent = content.map((content) =>
    <Post title={content.title} imgUrl={content.imgUrl} description={content.description} />
);
    ReactDOM.render(
        <div className="feed">{listContent}</div>,
        document.getElementById('content')
    );

