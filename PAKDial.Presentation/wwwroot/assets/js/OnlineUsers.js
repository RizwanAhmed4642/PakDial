var connection = new signalR.HubConnectionBuilder().withUrl('/onlineUsersCount').build();
connection.start();
connection.on("updateCount", function (count) {
    var userCountSpan = document.getElementById('ADbOnlineUsers');
    userCountSpan.innerText = count;
});

//document.addEventListener('DOMContentLoaded', function () {
//    function bindConnectionMessage(connection) {
//        var messageCallback = function (message) {
//            console.log('message' + message);
//            if (!message) return;
//            //var userCountSpan = document.getElementById('ADbOnlineUsers');
//            //userCountSpan.innerText = message;
//        };
//        connection.on("updateCount", messageCallback);
//        connection.onclose(onConnectionError);
//    }

//    function onConnected(connection) {
//        console.log('connection started');
//    }
//    function onConnectionError(error) {
//        if (error && error.message) {
//            console.log(error.message);
//        }
//    }

//    var connection = new signalR.HubConnectionBuilder().withUrl('/onlineUsersCount').configureLogging(signalR.LogLevel.Trace).build();
//    bindConnectionMessage(connection);
//    connection.start()
//        .then(function () {
//            onConnected(connection);
//        })
//        .catch(function (error) {
//            console.log(error);
//        });
//});

