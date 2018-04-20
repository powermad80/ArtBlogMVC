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
                    <a href={"Posts/" + this.props.id}>
                        <img className="post-img-tag" src={this.props.imgUrl} />
                    </a>
                </div>

                <div className="post-footer">
                    <span>{this.props.tags}</span>
                    <hr />
                    <p>{this.props.description}</p>
                </div>
            </div>
        );
    }
}

var url = window.location.href.split('=');
var postid = url[url.length - 1];

$.ajax({
    async: true,
    type: "POST",
    url: "GetPostData",
    data: { id: postid },
    success: function (data) {

        

        ReactDOM.render(
            <Post className="post" id={data.id} title={data.title} imgUrl={"http://localhost:55621/" + data.imgUrl} description={data.description} tags={data.tags} />,
            document.getElementById('content')                             //Replace with window.location.hostname on deployment to actual URL
        );

        //const listContent = data.map((data) =>
        //    <Post className="post" key={data.id} id={data.id} title={data.title} imgUrl={data.imgUrl} description={data.description} tags={data.tags} />
        //);
        //ReactDOM.render(
        //    <div className="solofeed">{listContent}</div>,
        //    document.getElementById('content')
        //);
    }
});



