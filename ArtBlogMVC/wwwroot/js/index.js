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
                    <img className="post-img-tag" src={this.props.imgUrl} />
                </div>

                <div className="post-footer">
                    <span>{this.props.tags}</span>
                    <hr />
                    <p>{this.props.description}</p>
                </div>
            </div>
            )
    }
}


$.ajax({
    async: true,
    url: "Home/GetPosts",
    success: function (data) {
        const listContent = data.map((data) =>
            <Post className="post" key={data.id} title={data.title} imgUrl={data.imgUrl} description={data.description} tags={data.tags} />
        );
        ReactDOM.render(
            <div className="feed">{listContent}</div>,
            document.getElementById('content')
        );
    }
});



