function sendAjaxRequest(url, method, data, onSuccess, onError) {
    $.ajax({
        url: url,
        method: method,
        contentType: 'application/json',
        data: JSON.stringify(data),
        success: onSuccess,
        error: onError
    });
}