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
                    <a href={"Posts/img?id=" + this.props.id}>
                        <img className="post-img-tag" src={this.props.imgUrl} />
                    </a>
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
    type: "POST",
    url: "Home/GetPosts",
    success: function (data) {
        const listContent = data.map((data) =>
            <Post className="post" key={data.id} id={data.id} title={data.title} imgUrl={"http://localhost:55621/" + data.imgUrl} description={data.description} tags={data.tags} />
        );                                                                              //Replace with window.location.hostname on deployment to actual URL
        ReactDOM.render(
            <div className="feed">{listContent}</div>,
            document.getElementById('content')
        );
    }
});



