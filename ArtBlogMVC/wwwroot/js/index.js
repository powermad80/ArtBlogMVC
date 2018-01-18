class TestIt extends React.Component {
    render() {
        return (
            <h1>It's Alive!</h1>
        );
    }
}

class Post extends React.Component {

}


ReactDOM.render(
    <TestIt />,
    document.getElementById('content')
);