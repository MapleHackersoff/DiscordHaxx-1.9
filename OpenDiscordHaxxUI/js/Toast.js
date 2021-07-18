const ToastType = {
    Info: 'info',
    Success: 'success',
    Warning: 'warning',
    Error: 'danger'
}


function ShowToast(type, htmlContent) {
    const toast = document.createElement('alert');
    toast.style = 'position: fixed; bottom: 0; margin-left: 14px;';

    toast.classList = 'alert alert-' + type;
    toast.innerHTML = htmlContent;
    
    document.body.appendChild(toast);

    setTimeout(RemoveToast, 3500, toast);
}


function RemoveToast(alertElement) {
    alertElement.style.opacity = 1;

    setInterval(function() 
    {
        if (alertElement.style.opacity == 0) {
            clearInterval();
            alertElement.remove();
            return;
        }

        alertElement.style.opacity -= 0.01;
    }, 7);
}