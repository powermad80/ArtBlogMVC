
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
                    <a href={"Posts/img?id=" + this.props.id}>
                        <img className="post-img-tag" src={this.props.imgUrl} />
                    </a>
                </div> 

                <div className="post-footer">
                    {this.renderTags(this.props.tags)}
                    <hr />
                    <p>{this.props.description}</p>
                </div>
            </div>
            )
    }
}

var pagenum = 1;

function LoadPage() {
    var search = document.getElementById("search").value;
    $.ajax({
        aync: true,
        type: "POST",
        url: "Home/GetPosts",
        data: {
            page: pagenum,
            search: search
        },
        success: function (data) {
            var posts = data.posts;
            const listContent = data.posts.map((posts) =>
                <Post className="post" key={posts.id} id={posts.id} title={posts.title} imgUrl={/*"http://localhost:55621/" + */posts.imgUrl} description={posts.description} tags={posts.tags} />
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

$('#search').keyup(function (e) {
    if (e.keyCode === 13) {
        LoadPage();
    }
});

LoadPage();
document.getElementById("nav-left").addEventListener("click", PreviousPage);
document.getElementById("nav-right").addEventListener("click", NextPage);




