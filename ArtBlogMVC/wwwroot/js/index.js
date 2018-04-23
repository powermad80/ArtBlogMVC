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



//$.ajax({
//    async: true,
//    type: "POST",
//    url: "Home/GetPosts",
//    success: function (data) {
//        const listContent = data.map((data) =>
//            <Post className="post" key={data.id} id={data.id} title={data.title} imgUrl={"http://localhost:55621/" + data.imgUrl} description={data.description} tags={data.tags} />
//        );                                                                              //Replace with window.location.hostname on deployment to actual URL
//        ReactDOM.render(
//            <div className="feed">{listContent}</div>,
//            document.getElementById('content')
//        );
//        document.getElementById("pagenum").innerHTML = pagenum + " ";
//    }
//});
var pagenum = 1;

function LoadPage() {
    $.ajax({
        aync: true,
        type: "POST",
        url: "Home/GetPosts",
        data: { page: pagenum },
        success: function (data) {
            var posts = data.posts;
            const listContent = data.posts.map((posts) =>
                <Post className="post" key={posts.id} id={posts.id} title={posts.title} imgUrl={"http://localhost:55621/" + posts.imgUrl} description={posts.description} tags={posts.tags} />
            );                                                                              //Replace with window.location.hostname on deployment to actual URL
            ReactDOM.render(
                <div className="feed">{listContent}</div>,
                document.getElementById('content')
            );
            document.getElementById("pagenum").innerHTML = pagenum + " ";
            SetArrows(data.arrowState);
        }
    })
}

function SetArrows(state) {
    document.getElementById("nav-left").hidden = state.leftArrow;
    document.getElementById("nav-right").hidden = state.rightArrow;
}

function NextPage() {
    pagenum = pagenum + 1;
    LoadPage();
}

function PreviousPage() {
    if (pagenum > 1) {
        pagenum = pagenum - 1;
        LoadPage();
    }
}

LoadPage();
document.getElementById("nav-left").addEventListener("click", PreviousPage);
document.getElementById("nav-right").addEventListener("click", NextPage);




