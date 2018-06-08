class Tag extends React.Component {
    constructor(props) {
        super(props);
    }

    viewTag(props) {
        document.getElementById("search").value = props.tag;
        LoadPage();
    }

    render() {
        return (
            <span className="tag">
                <a href="#" onClick={() => { this.viewTag(this.props) }} >{this.props.tag}</a>
            </span>)
    }

}


class Post extends React.Component {
    constructor(props) {
        super(props);
    }

    renderTags(tags) {
        var taglist = tags.split(" ");
        if (tags == "") {
            return;
        }
        const listContent = taglist.map((taglist) =>
            <Tag tag={taglist} />
        );

        return listContent;
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
                    {this.renderTags(this.props.tags)}
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

    }
});



