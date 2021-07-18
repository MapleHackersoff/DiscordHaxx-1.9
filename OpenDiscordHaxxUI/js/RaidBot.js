let socket;


window.onload = function() {
    socket = new WebSocket("ws://localhost:420/raid");
    socket.onmessage = function(args) {
        
        const payload = JSON.parse(args.data);

        if (payload.succeeded)
            payload.message = '<strong>Success!</strong> ' + payload.message;

        ShowToast(payload.succeeded ? ToastType.Success : ToastType.Error, payload.message);
    }
    socket.onerror = function() { ServerUnreachable() };
}

function StartBot(data) {
    socket.send(JSON.stringify(data));
}