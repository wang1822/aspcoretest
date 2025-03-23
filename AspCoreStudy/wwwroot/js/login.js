$(document).ready(function () {
    $('#loginForm').submit(function (event) {
        event.preventDefault();

        const username = $('#username').val();
        const password = $('#password').val();

        sendAjaxRequest(
            '/api/v1/accountapi/login',
            'POST',
            { username: username, passwordHash: password },
            function (response) {
                const token = response.token;
                localStorage.setItem('token', token);
                alert('登录成功');
                window.location.href = '/Home/Index';
            },
            function (xhr) {
                const errors = xhr.responseJSON?.errors;
                if (xhr.status === 500) {
                    alert('服务器错误，请稍后再试');
                } else {
                    let errorMessages = [];
                    $.each(errors, function (field, messages) {
                        errorMessages.push(messages.join(', '));
                    });
                    alert('\n' + errorMessages.join('\n'));
                }
            });
    });
});