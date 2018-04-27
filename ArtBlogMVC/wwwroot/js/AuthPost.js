class Post extends React.Component {
    constructor(props) {
        super(props);
    }
    render() {
        return (
            <div /*action="SaveChanges" encType="multipart/form-data" method="post"*/ className="post">
                <div className="post-header">
                    <h3><input style={{ minWidth: 565 }} type="text" name="title" id="title" defaultValue={this.props.title}></input></h3>
                </div>

                <div className="post-img">
                    <img className="post-img-tag" src={this.props.imgUrl} />
                </div>

                <div className="post-footer">
                    <span><input style={{ minWidth: 555, marginLeft: 5 }} type="text" name="tags" id="tags" defaultValue={this.props.tags}></input></span>
                    <hr />
                    <p><textarea style={{ minWidth: 555, marginLeft: 0 }} name="description" id="description" defaultValue={this.props.description}></textarea></p>
                </div>
                <input style={{ marginLeft: 23 }} type="checkbox" name="delete" id="delete" value={1} ></input>
                <label>Delete Post</label>
                <input type="hidden" name="postId" id="postId" />
                <p><button style={{ marginLeft: 18 }} onClick={UpdatePost} >Save Changes</button></p>
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

        document.getElementById('postId').value = postid;

    }
});

function UpdatePost() {
    var checked = 0;
    if (document.getElementById("delete").checked) {
        checked = 1;
    }

    var editedPost = {
        Id: postid,
        Title: document.getElementById("title").value,
        Description: document.getElementById("description").value,
        Tags: document.getElementById("tags").value,
        Deleted: checked
    };

    $.ajax({
        async: true,
        type: "POST",
        url: "SaveChanges",
        data: { editedPost: editedPost },
        success: function (data) {
            if (data === "403") {
                window.alert("Authentication failed, please log in");
            }

            if (data == "success") {
                if (editedPost.Deleted == 1) {
                    window.location.href = "Index";
                }
                else {
                    window.location.reload();
                }
            }
        }
    });
}



