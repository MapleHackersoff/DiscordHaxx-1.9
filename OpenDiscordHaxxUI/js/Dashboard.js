let socket;

const DashboardOpcode = {
    StatusUpdate: 0,
    OverlookUpdate: 1,
    AttacksUpdate: 2,
    KillAttack: 3
}


window.onload = function() {
    socket = new WebSocket("ws://localhost:420/dashboard");
    socket.onmessage = function(args) {
        
        const payload = JSON.parse(args.data);

        switch (payload.op) {
            case DashboardOpcode.StatusUpdate: //this does not account for the server dying
                OnStatusUpdate(payload.data);
                break;
            case DashboardOpcode.OverlookUpdate:
                OnOverlookUpdate(payload.data);
                break;
            case DashboardOpcode.AttacksUpdate:
                OnAttacksUpdate(payload.data.attacks);
                break;
        }
    }
    //this probably means the server is down or we don't have a connection to the internet
    socket.onerror = function(error) {
        OnStatusUpdate({ status: "Unreachable" });
    }
}


//sets serverStatus's text to the new status, as well as changing it's color depending on the status
function OnStatusUpdate(data) {
    const statusLabel = document.getElementById('serverStatus');

    statusLabel.innerText = data.status.toUpperCase();

    switch (statusLabel.innerText) {
        case "READY":
            statusLabel.style.color = "rgb(50,205,50)";
            break;
        case "LOADING BOTS":
            statusLabel.style.color = "rgb(255,255,0)";
            break;
        case "UNREACHABLE":
            statusLabel.style.color = "rgb(130,0,0)";
            OnOverlookUpdate({ accounts: 0, attacks: 0 });
            OnAttacksUpdate({});
            break;
        default:
            statusLabel.style.color = "rgb(170,192,195)";
    }
}


function OnOverlookUpdate(data) {
    if (typeof data.accounts !== "undefined")
        document.getElementById('account-amount').innerHTML = 'Loaded accounts: <span style="color: rgb(230,252,255)">' + data.accounts + '</span>';
    document.getElementById('attack-amount').innerHTML = 'Ongoing attacks: <span style="color: rgb(230,252,255)">' + data.attacks + '</span>';
}


function OnAttacksUpdate(attackList) {

    if (typeof attackList.length !== "undefined")
        OnOverlookUpdate({ attacks: attackList.length });

    const list = document.getElementById('attack-list');
    let html = '';

    for (let i = 0; i < attackList.length; i++) {
        const attack = attackList[i];

        html += '<tr id="row-' + i + '" style="letter-spacing: 0.7px; font-size: 17.5px;">\n'
              + '<td style="font-family: Roboto; font-weight: 100; letter-spacing: 0px">' + attack.type + '</td>\n'
              + '<td>' + attack.bots + '</td>\n'
              + '<td>' + attack.threads + '</td>\n'
              + '<td style="display: none">' + attack.id + '</td>\n'
              + '</tr>';
    }

    list.innerHTML = html;

    list.childNodes.forEach(row => {
        $('#' + row.id).contextMenu({
            menuSelector: "#attack-list-context-menu",
            menuSelected: OnContextMenuUsed
        });
    });
}


function OnContextMenuUsed(invokedOn, selectedMenu) {
    const info = GetRowInformation(invokedOn);

    switch (selectedMenu.text()) {
        case 'Kill':
            OnKill(info);
            break;
    }
}


function OnKill(info) {
    socket.send(JSON.stringify({ op: DashboardOpcode.KillAttack, id: info.id }));
}


function GetRowInformation(invokedOn) {
    const row = document.getElementById(invokedOn[0].parentNode.id);


    return { type: row.childNodes[1].innerText, 
             bots: row.childNodes[3].innerText,
             threads: row.childNodes[5].innerText,
             id: row.childNodes[7].innerText };
}