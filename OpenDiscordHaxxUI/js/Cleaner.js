let socket;

const CleanerOpcode = {
    StartCleaner: 0,
    CleanerStarted: 1,
    AccountCleaned: 2,
    CleanerResume: 3,
    CleanerFinished: 4
}


window.onload = function() {
    const options = document.getElementById('clean-options');
    const logs = document.getElementById('cleaner-logs');
    logs.value = '';

    socket = new WebSocket("ws://localhost:420/cleaner");
    socket.onmessage = function(args) {
        
        const payload = JSON.parse(args.data);

        switch (payload.op) {
            case CleanerOpcode.AccountCleaned:
                logs.value = logs.value + payload.at + ' has been cleaned!\n';
                break;
            case CleanerOpcode.CleanerStarted:
                options.disabled = true;
                break;
            case CleanerOpcode.CleanerResume:
                options.disabled = true;
                break;
            case CleanerOpcode.CleanerFinished:
                options.disabled = false;

                ShowToast(ToastType.Success, 'Cleaner has finished!');
                break;
        }
    }
    socket.onerror = function() { ServerUnreachable() };
}


function StartCleaner() {
    socket.send(JSON.stringify(
            { 
                op: CleanerOpcode.StartCleaner, 
                remove_guilds: document.getElementById('remove-guilds').checked == true,
                remove_relationships: document.getElementById('remove-relationships').checked == true,
                remove_private_channels: document.getElementById('remove-privates').checked == true,
                reset_profile: document.getElementById('reset-profile').checked == true
            }));
}