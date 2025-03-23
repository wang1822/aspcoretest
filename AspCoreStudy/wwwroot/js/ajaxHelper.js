function sendAjaxRequest(url, method, data, onSuccess, onError) {
    const token = localStorage.getItem("token");

    $.ajax({
        url: url,
        method: method,
        contentType: 'application/json',
        data: data ? JSON.stringify(data) : null,
        headers: token ? { "Authorization": `Bearer ${token}` } : {},
        success: onSuccess,
        error: onError
    });
}