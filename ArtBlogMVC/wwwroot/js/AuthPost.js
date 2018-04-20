class Post extends React.Component {
    constructor(props) {
        super(props);
    }
    render() {
        return (
            <form action="SaveChanges" encType="multipart/form-data" method="post" className="post">
                <div className="post-header">
                    <h3><input type="text" name="title" id="title" defaultValue={this.props.title}></input></h3>
                </div>

                <div className="post-img">
                    <img className="post-img-tag" src={this.props.imgUrl} />
                </div>

                <div className="post-footer">
                    <span><input type="text" name="tags" id="tags" defaultValue={this.props.tags}></input></span>
                    <hr />
                    <p><input type="text" name="description" id="description" defaultValue={this.props.description}></input></p>
                </div>
                <input type="checkbox" name="delete" id="delete" value={1} ></input>
                <label>Delete Post</label>
                <p><button type="submit">Save Changes</button></p>
                <input type="hidden" name="postId" id="postId" />
            </form>
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

        document.getElementById('postId').value = postid;

        //const listContent = data.map((data) =>
        //    <Post className="post" key={data.id} id={data.id} title={data.title} imgUrl={data.imgUrl} description={data.description} tags={data.tags} />
        //);
        //ReactDOM.render(
        //    <div className="solofeed">{listContent}</div>,
        //    document.getElementById('content')
        //);
    }
});



