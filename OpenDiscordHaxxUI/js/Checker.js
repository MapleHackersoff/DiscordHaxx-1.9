let socket;

const CheckerOpcode = {
    Started: 0,
    BotChecked: 1,
    Error: 2,
    Done: 3,
    Resume: 4
}


window.onload = function() {

    document.getElementById('checker-results').value = '';

    socket = new WebSocket("ws://localhost:420/checker");
    socket.onmessage = function(args) {

        const payload = JSON.parse(args.data);
        
        switch (payload.op) {
            case CheckerOpcode.Started:
                UpdateProgress(payload.progress);

                ShowToast(ToastType.Info, 'Checker has started');
                break;
            case CheckerOpcode.BotChecked:
                UpdateProgress(payload.progress);
                AppendText(payload.bot.at + ' is ' + (payload.valid ? 'valid!' : 'invalid :/'));
                break;
            case CheckerOpcode.Error:
                switch (payload.error) {
                    case 'ratelimit':
                        FatalError('Server is rate limited.\nInvalid tokens will still be removed');
                        break;
                    case 'notokens':
                        FatalError('No tokens are loaded.')
                        break;
                }
                break;
            case CheckerOpcode.Done:
                ShowToast(ToastType.Success, 'Checker has finished');
                break;
            case CheckerOpcode.Resume:
                UpdateProgress(payload.progress);
                break;
        }
    }
    socket.onerror = function() { ServerUnreachable() };
}


function AppendText(text) {
    const results = document.getElementById('checker-results');

    results.value = results.value + text + '\n';
}


function UpdateProgress(progress) {
    document.getElementById('progress').innerText = 'Valid: ' + progress.valid + ' | Invalid: ' + progress.invalid + ' | ' + (progress.valid + progress.invalid) + '/' + progress.total;
}